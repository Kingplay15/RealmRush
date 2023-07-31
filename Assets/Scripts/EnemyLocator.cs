using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocator : MonoBehaviour
{
    [SerializeField] float attackRange = 15f;
    [SerializeField] private Transform weapon;
    [SerializeField] private ParticleSystem projecttileParticles;
    private Enemy target;   

    private void Update()
    {
        FindClosestEnemies();
        AimAtEnemy();
    }

    private void FindClosestEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;

        foreach(Enemy e in enemies)
        {
            float distance = Vector3.Distance(transform.position, e.transform.position);
            if (distance < maxDistance)
            {
                target = e;
                maxDistance = distance;
            }
        }
    }

    private void AimAtEnemy()
    {
        weapon.LookAt(target.transform);

        //If the closest enemy is in attack range, then attack
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= attackRange)
            Attack(true);
        else Attack(false);
    }

    private void Attack(bool isActive)
    {
        var emissionModule = projecttileParticles.emission;
        emissionModule.enabled = isActive;
    }

}
