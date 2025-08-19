using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IDamagable
{
    void TakePhsicalDamage(int damage);
}
public class PlayerCondition : MonoBehaviour, IDamagable
{
    public GameUI gameUI;
    private Animator animator;
    private PlayerInput PlayerInput;

    Condition health { get { return gameUI.health; } }
    Condition hunger { get { return gameUI.hunger; } }
    Condition thirst { get { return gameUI.thirst; } }
    Condition temperature { get { return gameUI.temperature; } }
    Condition stamina { get { return gameUI.stamina; } }

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;
    public float noTemperatureHealthDecay;
    public event Action onTakeDamage;

    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        hunger.TakeDamage(hunger.passiveValue * Time.deltaTime);
        thirst.TakeDamage(thirst.passiveValue * Time.deltaTime);
        temperature.TakeDamage(temperature.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0f)
        {
            health.TakeDamage(noHungerHealthDecay * Time.deltaTime);
        }

        if (thirst.curValue <= 0f)
        {
            health.TakeDamage(noThirstHealthDecay * Time.deltaTime);
        }

        if (temperature.curValue <= 0f)
        {
            health.TakeDamage(noTemperatureHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0f && !isDead)
        {
            Die();
            isDead = true;//죽었을 때 애니메이터 한 번 켜기 위해
            PlayerInput.enabled = false;//죽었을 때 안 움직이게 하기 위해 재시작시 true로 만들어야 함
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

    public void Drink(float amount)
    {
        thirst.Add(amount);
    }

    public void WarmUp(float amount)
    {
        temperature.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
        animator.SetTrigger("isDie");
    }

    public void TakePhsicalDamage(int damage)
    {
        health.TakeDamage(damage);
        onTakeDamage?.Invoke();
    }
}
