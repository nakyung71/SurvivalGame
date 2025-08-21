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
    private PlayerInput playerInput;
    private PlayerController playerController;
    public LayerMask coldTempLayerMask;
    public LayerMask hotTempLayerMask;

    Condition health { get { return gameUI.health; } }
    Condition hunger { get { return gameUI.hunger; } }
    Condition thirst { get { return gameUI.thirst; } }
    Condition temperature { get { return gameUI.temperature; } }
    Condition stamina { get { return gameUI.stamina; } }

    public float noHungerHealthDecay;
    public float noThirstHealthDecay;
    public float temperatureHealthDecay;
    public event Action onTakeDamage;

    private bool isDead = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        hunger.TakeDamage(hunger.passiveValue * Time.deltaTime);
        thirst.TakeDamage(thirst.passiveValue * Time.deltaTime);

        if (hunger.curValue <= 0f)
        {
            health.TakeDamage(noHungerHealthDecay * Time.deltaTime);
        }

        if (temperature.curValue >= temperature.maxValue)
        {
            thirst.TempDamage(thirst.passiveValue * Time.deltaTime);
        }

        if (thirst.curValue <= 0f)
        {
            health.TakeDamage(noThirstHealthDecay * Time.deltaTime);
        }

        if (temperature.curValue <= 0f)
        {
            health.TakeDamage(temperatureHealthDecay * Time.deltaTime);
        }

        if (health.curValue <= 0f && !isDead)
        {
            Die();
            isDead = true;//죽었을 때 애니메이터 한 번 켜기 위해
            playerInput.enabled = false;//죽었을 때 안 움직이게 하기 위해 재시작시 true로 만들어야 함
        }

        if(IsColdPlace())
        {
            temperature.TakeDamage(temperature.passiveValue * Time.deltaTime);
        }
        else if(IsHotPlace())
        {
            temperature.Add(temperature.passiveValue * Time.deltaTime);
        }
        else
        {
            if(temperature.curValue > 55)
            {
                temperature.TakeDamage(temperature.passiveValue* 0.1f * Time.deltaTime);
            }

            else if(temperature.curValue < 45)
            {
                temperature.Add(temperature.passiveValue * 0.1f * Time.deltaTime);
            }
        }


        if (stamina.curValue <= 1)
        {
            playerController.runSpeed = 1;
            playerController.runbool = false;
            animator.SetBool("isRun", playerController.runbool);
        }

        if (animator.GetBool("isRun") == false)
        {
            stamina.Add(stamina.passiveValue * Time.deltaTime);
        }

        if(animator.GetBool("isRun"))
        {
            stamina.TakeDamage(stamina.passiveValue * Time.deltaTime);
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

    public void Die()
    {
        animator.SetTrigger("isDie");
    }

    public void TakePhsicalDamage(int damage)
    {
        health.TakeDamage(damage);
        onTakeDamage?.Invoke();
    }

    bool IsColdPlace()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down)
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.3f, coldTempLayerMask))
            {
                return true;
            }
        }
        return false;
    }

    bool IsHotPlace()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.1f), Vector3.down)
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.3f, hotTempLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
