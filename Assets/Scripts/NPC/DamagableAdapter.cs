using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableAdapter : MonoBehaviour
{
    [SerializeField] private Enemy enemy;

    private void Awake()
    {
        if (enemy == null) enemy = GetComponent<Enemy>();
    }

    public void TakePhysicalDamage(int damage)
    {
        if (enemy != null)
        {
            enemy.TakePhysicalDamage(damage); // 기존 메서드 그대로 호출
        }
    }
}
