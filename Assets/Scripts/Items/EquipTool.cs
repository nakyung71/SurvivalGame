using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera camera;
    private AttackSound attackSound;

    private void Awake()
    {
        camera = Camera.main;
        animator = GetComponent<Animator>();
        attackSound = GetComponentInParent<AttackSound>();
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            attacking = true;
            animator.SetTrigger("Attack");
            Invoke("OnCanAttack", attackRate);
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    public void OnHit()
    {
        if (camera == null) camera = Camera.main;

        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * attackDistance, Color.red, 0.5f);

        if (Physics.Raycast(ray, out hit, attackDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            Debug.Log($"[공격 판정] {hit.collider.name} 에 맞음 (좌표: {hit.point})");

            if (doesDealDamage)
            {
                // 자식 콜라이더를 맞아도 부모의 Enemy를 찾도록 InParent 사용
                var damageable = hit.collider.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakePhysicalDamage(damage);
                    Debug.Log($"[전투] {hit.collider.name} 에 {damage} 피해");
                    return; // 한 번의 스윙에 자원채집까지 함께하지 않으려면 바로 종료
                }
            }

            if (doesGatherResources)
            {
                var resource = hit.collider.GetComponentInParent<Resource>();
                if (resource != null)
                {
                    Debug.Log($"[공격 판정] {resource.name} 에서 자원 채집 시도");
                    resource.Gather(hit.point, hit.normal);
                    attackSound.Hit();
                }
                else
                {
                    Debug.LogWarning($"[공격 판정] {hit.collider.name} 오브젝트에서 Resource 스크립트를 찾을 수 없음");
                }
            }
        }
        else
        {
            Debug.Log("[공격 판정] 레이캐스트가 아무것도 맞추지 못함 (범위 부족 또는 콜라이더 없음)");
        }
    }
}
