using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableAdapter : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerCondition player;

    private void Awake()
    {
        if (player == null) player = GetComponent<PlayerCondition>();
    }

    public void TakePhysicalDamage(int damage)
    {
        if (player != null)
        {
            player.TakePhsicalDamage(damage); // 기존 메서드 그대로 호출
        }
    }

}
