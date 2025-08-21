using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedUnit : MonoBehaviour
{
    [HideInInspector] public NPCSpawner spawner;
    [HideInInspector] public GameObject prefabKey;

    bool _quitting;
    void OnApplicationQuit() => _quitting = true;

    void OnDestroy()
    {
        if (_quitting || spawner == null) return;
        spawner.OnUnitDestroyed(prefabKey, this); // 죽은만큼 보충 신호
    }
}
