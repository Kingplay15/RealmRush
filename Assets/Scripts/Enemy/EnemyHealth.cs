using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currHealth = 0;

    public EventHandler OnDeathEvent;

    private void OnEnable()
    {
        currHealth = maxHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        GetHit();
    }

    private void GetHit()
    {
        currHealth--;
        if (currHealth <= 0)
        {
            OnDeathEvent?.Invoke(this, EventArgs.Empty);
            gameObject.SetActive(false);
        }
            
    }
}
