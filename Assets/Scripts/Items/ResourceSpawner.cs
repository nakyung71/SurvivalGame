using UnityEngine;
using System.Collections;

public class ResourceSpawner : MonoBehaviour
{
    [Tooltip("리스폰까지 대기 시간(초)")]
    public float respawnTime = 50f;

    [Tooltip("관리할 리소스. 비워두면 자식에서 자동으로 찾습니다.")]
    public Resource resource;

    // 원래 자리/자세/스케일 저장
    private Vector3 originPos;
    private Quaternion originRot;
    private Vector3 originScale;

    private bool isRespawning;

    private void Awake()
    {
        if (resource == null)
            resource = GetComponentInChildren<Resource>(includeInactive: true);

        if (resource == null)
        {
            Debug.LogError("[ResourceSpawner] 자식에 Resource가 없습니다.");
            return;
        }

        // 리스폰 시 정확히 같은 자리에서 나타나도록 최초 상태 저장
        var t = resource.transform;
        originPos = t.position;
        originRot = t.rotation;
        originScale = t.localScale;
    }

    public void StartRespawn(Resource res)
    {
        if (isRespawning) return;
        StartCoroutine(RespawnRoutine(res));
    }

    private IEnumerator RespawnRoutine(Resource res)
    {
        isRespawning = true;
        yield return new WaitForSeconds(respawnTime);

        // 위치/회전/스케일 원복 (같은 자리 리스폰 보장)
        var t = res.transform;
        t.SetPositionAndRotation(originPos, originRot);
        t.localScale = originScale;

        res.ResetCapacity();
        res.gameObject.SetActive(true); // 다시 등장

        isRespawning = false;
    }
}