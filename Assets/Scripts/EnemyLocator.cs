using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    private EnemyMover enemy;

    private void Awake()
    {
        enemy = FindObjectOfType<EnemyMover>();
    }

    private void Update()
    {
        AimAtEnemy();
    }

    private void AimAtEnemy()
    {
        weapon.LookAt(enemy.transform);
    }

}
