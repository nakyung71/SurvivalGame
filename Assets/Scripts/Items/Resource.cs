using UnityEngine;

public class Resource : MonoBehaviour
{
    // ===== ▼▼▼ 추가: 자원 종류 ▼▼▼ =====
    public enum ResourceKind { Tree, Rock }           // ★추가
    [Header("Resource Type")]                         // ★추가
    public ResourceKind resourceKind = ResourceKind.Tree; // ★추가
    // ===== ▲▲▲ 추가: 자원 종류 ▲▲▲ =====

    public ItemData itemToGive;
    public int quantityPerHit = 1;

    [SerializeField] private int capacity;        // (변경) private + SerializeField (원본과 동일 유지)
    [SerializeField] private int maxCapacity = 5; // (추가) 리젠 시 채우는 최대치 (원본과 동일 유지)

    private ResourceSpawner spawner;              // (추가) 부모 Spawner 참조 (원본과 동일 유지)

    private void Awake()
    {
        spawner = GetComponentInParent<ResourceSpawner>();

        // (추가) 콜라이더 확인 로그 (원본과 동일 유지)
        if (GetComponent<Collider>() == null && GetComponentInChildren<Collider>() == null)
        {
            Debug.LogWarning($"{name}: 콜라이더가 없음 → 레이캐스트로 맞출 수 없음");
        }
    }

    private void OnEnable()
    {
        if (capacity <= 0) capacity = maxCapacity;
    }

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        if (capacity <= 0)
        {
            Debug.Log($"{name}: 이미 고갈된 자원, 채집 불가");
            return;
        }

        Debug.Log($"{name}: 채집 시도 (현재 용량={capacity}, 1회 채집량={quantityPerHit})");

        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0) break;

            capacity--;


            // (추가) null 체크
            if (itemToGive != null && itemToGive.dropPrefab != null)
            {
                Instantiate(itemToGive.dropPrefab,
                            hitPoint + Vector3.up,
                            Quaternion.LookRotation(hitNormal, Vector3.up));

                Debug.Log($"{name}: {itemToGive.name} 아이템 드랍, 남은 용량={capacity}");
            }
            else
            {
                Debug.LogWarning($"{name}: itemToGive 또는 dropPrefab이 지정되지 않음 → 아이템 생성 실패");
            }
        }

        if (capacity <= 0 && spawner != null)
        {
            Debug.Log($"{name}: 자원 고갈됨, 리스폰 요청");
            spawner.StartRespawn(this); // (추가) Spawner에 리스폰 요청 (원본과 동일 위치/동작)
            gameObject.SetActive(false); // (변경) 파괴 대신 비활성화 (원본과 동일 유지)
        }
    }

    public void ResetCapacity()
    {
        capacity = maxCapacity; // (추가)
        Debug.Log($"{name}: 용량이 {capacity} 으로 초기화됨");
    }
}
