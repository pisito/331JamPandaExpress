using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Modified by Pisit on 03-12-2024
public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f; // percent is better
    public bool isDeath = false;

    public UnityEvent<float> onHealthChange;
    public UnityEvent onDead;

    public float defense = 0f;

    // Start is called before the first frame update
    void Start()
    {
        healthAmount = Mathf.Clamp(healthAmount, 0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0)
        {
            isDeath = true;
        }
        else
        {
            isDeath = false;
        }

        //// Debug Purpose
        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    TakeDamage(20);
        //}

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Heal(5);

        //}
    }

    public void TakeDamage (float damage)
    {
        healthAmount -= Mathf.Clamp((damage - defense),0, 100);
        healthAmount = Mathf.Clamp(healthAmount, 0f, 100f);
        
        if(healthBar!=null) healthBar.fillAmount = healthAmount / 100f;

        onHealthChange?.Invoke(healthAmount);
        if(healthAmount <= 0)
        {
            Dead();
        }
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0f, 100f);

        if(healthBar != null) healthBar.fillAmount = healthAmount / 100f;

        onHealthChange?.Invoke(healthAmount);
    }

    public void Dead()
    {
        isDeath = true;
        onDead?.Invoke();
    }

    public void AddDefense(float value)
    {
        defense += value;
    }
}
