using UnityEngine;

public class Resource : MonoBehaviour
{
    public ItemData itemToGive;
    public int quantityPerHit = 1;

    [SerializeField] private int capacity;        // (변경) private + SerializeField
    [SerializeField] private int maxCapacity = 5; // (추가) 리젠 시 채우는 최대치

    private ResourceSpawner spawner;              // (추가) 부모 Spawner 참조

    private void Awake()
    {
        spawner = GetComponentInParent<ResourceSpawner>();
    }

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        if (capacity <= 0) return; // (추가) 고갈시 즉시 종료

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
            }
        }

        if (capacity <= 0 && spawner != null)
        {
            spawner.StartRespawn(this); // (추가) Spawner에 리스폰 요청
            gameObject.SetActive(false); // (변경) 파괴 대신 비활성화
        }
    }

    public void ResetCapacity() => capacity = maxCapacity; // (추가)
}
