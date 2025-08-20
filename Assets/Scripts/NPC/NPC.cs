using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Idle,
    Walk,
    Run
}

public class NPC : MonoBehaviour, IDamageable, ITalkable, IInteractable, IQuest
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDeath;

    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance;
    private AIState aiState;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    private float playerDistance;

    public float fieldOfView = 120f;

    private Animator animator;
    private SkinnedMeshRenderer[] meshRenderers;

    private int safeDistance = 7;

    [SerializeField] DialogueData data;
    public DialogueData DialogueData => data;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        SetState(AIState.Walk);
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, CharacterManager.Instance.Player.transform.position);

        animator.SetBool("IsWalk", aiState == AIState.Walk);
        animator.SetBool("IsRun", aiState == AIState.Run);
        animator.SetFloat("Speed", agent.velocity.magnitude);

        switch (aiState)
        {
            case AIState.Idle:
                PassiveUpdate();
                break;
            case AIState.Walk:
                PassiveUpdate();
                break;
            case AIState.Run:
                RunUpdate();
                break;
        }
    }

    private void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Walk:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            case AIState.Run:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

    }

    void PassiveUpdate()
    {
        if (aiState == AIState.Walk && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        if (playerDistance < detectDistance)
        {
            SetState(AIState.Run);
        }
    }

    void RunUpdate()
    {
        if (agent.remainingDistance < 0.1f)
        {
            agent.SetDestination(GetFleeLocation());
        }
        else
        {
            SetState(AIState.Run);
        }
    }

    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle) return;

        SetState(AIState.Walk);
        agent.SetDestination(GetWanderLocation());
    }

    bool IsPlayerInFieldOfView()
    {
        Vector3 directionToPlayer = CharacterManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < fieldOfView * 0.5f;
    }

    Vector3 GetFleeLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (GetDestinationAngle(hit.position) > 90 || playerDistance < safeDistance)
        {

            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * safeDistance), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30)
                break;
        }

        return hit.position;
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }

        return hit.position;
    }

    float GetDestinationAngle(Vector3 targetPos)
    {
        return Vector3.Angle(transform.position - CharacterManager.Instance.Player.transform.position, transform.position + targetPos);
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
            Die();

        StartCoroutine(DamageFlash());
    }

    void Die()
    {
        for (int x = 0; x < dropOnDeath.Length; x++)
        {
            Instantiate(dropOnDeath[x].dropPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    IEnumerator DamageFlash()
    {
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = new Color(1.0f, 0.6f, 0.6f);

        yield return new WaitForSeconds(0.1f);
        for (int x = 0; x < meshRenderers.Length; x++)
            meshRenderers[x].material.color = Color.white;
    }

    public void AcceptQuest()
    {
        Debug.Log("Äù½ºÆ®");
    }

    public string GetInteractPrompt()
    {
        return "Å¥ºê";
    }

    public void OnInteract()
    {
        DialogueManager.Instance.SetTalk(data, this);
    }

    public void Talk()
    {
        DialogueManager.Instance.SetTalk(data);
    }
}
