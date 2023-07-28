using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private int currHealth = 0;

    private void Start()
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
            Destroy(gameObject);
    }
}
