using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerCondition : MonoBehaviour , IDamagable
{
    public UICondition uiCondition;
    
    Condition health { get {return uiCondition.health;}}

    public event Action onTakeDamage;

    private void Update()
    {
        // if (health.curValue <= 0.0f)
        // {
        //     Die();
        // }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    private void Die()
    {
        Debug.Log("dead");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }
}
