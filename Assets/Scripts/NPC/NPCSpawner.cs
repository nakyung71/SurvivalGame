using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform player;          // 플레이어 Transform 할당
    [SerializeField] private MeshCollider[] fieldAreas;  // 여러 개 할당 가능, 필드 범위로 쓸 바닥(Ground의 자식 오브젝트로 지정)

    [Header("Enemy Prefabs")]
    public GameObject Chicken;
    public GameObject Deer;
    public GameObject Dog;
    public GameObject Tiger;

    [Header("Spawn Settings")]
    [SerializeField] private float npcSpawnDistance = 5f;
    [SerializeField] private LayerMask groundMask = ~0;   // 지면 레이어(필요시 설정)
    [SerializeField] private float navMeshMaxSampleDist = 8f;
    [SerializeField] private float minSeparation = 1.0f;  // 서로 겹치지 않도록 간단한 거리 제한

    private readonly List<Vector3> _spawned = new();

    private void Start()
    {

        // 적들 필드 전체에서 지정 수량만큼
        SpawnInField(Chicken, 25);
        SpawnInField(Deer, 10);
        SpawnInField(Dog, 10);
        SpawnInField(Tiger, 1);
    }

    private void SpawnNearPlayer(GameObject prefab)
    {
        Vector3 ring = RandomPointOnRing(player.position, npcSpawnDistance);
        Vector3 ground = ProjectToGround(ring);

        if (!TryGetNavmeshPos(ground, navMeshMaxSampleDist, out var navPos)) return;
        if (!IsFarEnough(navPos)) return;

        var go = Instantiate(prefab, navPos, Quaternion.identity);

        // 프리팹에서 NavMeshAgent는 반드시 Disabled 상태여야 함!
        var ag = go.GetComponent<NavMeshAgent>();
        if (ag != null)
        {
            ag.enabled = false;               // 안전
            go.transform.position = navPos;   // 보정 위치
            ag.enabled = true;                // 이제 켬
            ag.Warp(navPos);                  // 확실히 NavMesh 위로 배치
        }
    }

    private void SpawnInField(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            const int maxTries = 25;
            for (int t = 0; t < maxTries; t++)
            {
                Vector3 probe = ProjectToGround(RandomPointInArea());

                // 필드가 넓으면 샘플 거리 조금 늘리세요(예: 8~12)
                if (!TryGetNavmeshPos(probe, navMeshMaxSampleDist, out var navPos)) continue;
                if (!IsFarEnough(navPos)) continue;

                var go = Instantiate(prefab, navPos, Quaternion.identity);

                var ag = go.GetComponent<NavMeshAgent>();
                if (ag != null)
                {
                    ag.enabled = false;
                    go.transform.position = navPos;
                    ag.enabled = true;
                    ag.Warp(navPos);
                }
                break;
            }
        }
    }


    private Vector3 RandomPointOnRing(Vector3 center, float radius)
    {
        float ang = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 p = center + new Vector3(Mathf.Cos(ang), 0f, Mathf.Sin(ang)) * radius;
        return p;
    }

    private Vector3 RandomPointInArea()
    {
        if (fieldAreas == null || fieldAreas.Length == 0)
            return FallbackArea();

        // 1) 랜덤으로 하나 선택
        MeshCollider chosen = fieldAreas[Random.Range(0, fieldAreas.Length)];
        if (chosen == null) return FallbackArea();

        // 2) 선택된 콜라이더의 bounds 사용
        Bounds b = chosen.bounds;
        Vector3 half = b.extents;

        float x = Random.Range(b.center.x - half.x, b.center.x + half.x);
        float z = Random.Range(b.center.z - half.z, b.center.z + half.z);

        float y = b.max.y + 50f; // 위에서 쏘기
        return new Vector3(x, y, z);
    }

    private Vector3 FallbackArea()
    {
        Vector3 half = new(25f, 0f, 25f);
        Vector3 c = transform.position;
        return new Vector3(
            Random.Range(c.x - half.x, c.x + half.x),
            c.y + 50f,
            Random.Range(c.z - half.z, c.z + half.z)
        );
    }

    private Vector3 ProjectToGround(Vector3 fromAbove)
    {
        if (Physics.Raycast(fromAbove, Vector3.down, out RaycastHit hit, 200f, groundMask))
            return hit.point;
        return fromAbove; // 실패 시 원위치
    }

    private bool TryGetNavmeshPos(Vector3 from, float maxDist, out Vector3 navPos)
    {
        if (NavMesh.SamplePosition(from, out var hit, maxDist, NavMesh.AllAreas))
        {
            navPos = hit.position;
            return true;
        }
        navPos = default;
        return false;
    }

    private bool IsFarEnough(Vector3 pos)
    {
        foreach (var p in _spawned)
        {
            if (Vector3.SqrMagnitude(p - pos) < minSeparation * minSeparation) return false;
        }
        _spawned.Add(pos);
        return true;
    }

}
