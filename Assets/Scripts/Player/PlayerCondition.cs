using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakePhsicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public GameUI gameUI;

    Condition health { get { return gameUI.health; } }
    Condition hunger { get { return gameUI.hunger; } }
    Condition stamina { get { return gameUI.stamina; } }

    public float noHungerHealthDecay;
    public event Action onTakeDamage;

   
    private void Update()
    {
        hunger.TakeDamage(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue < 0f)
        {
            health.TakeDamage(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue < 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    public void TakePhsicalDamage(int damage)
    {
        health.TakeDamage(damage);
        onTakeDamage?.Invoke();
    }
}
