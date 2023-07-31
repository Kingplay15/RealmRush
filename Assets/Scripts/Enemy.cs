using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int goldReward = 25;
    [SerializeField] private int goldPenalty = 25;
    private EnemyHealth health;
    private EnemyMover mover;
    private Bank bank;

    private void Awake()
    {
        health = GetComponent<EnemyHealth>();
        mover = GetComponent<EnemyMover>();
        bank = FindObjectOfType<Bank>();
    }

    private void Start()
    {
        health.OnDeathEvent += OnDeath_RewardGold;
        mover.OnReachingDestinationEvent += OnReachingDestination_StealGold;
    }

    private void OnDeath_RewardGold(object sender, EventArgs e)
    {
        bank.DepositGold(goldReward);
    }

    private void OnReachingDestination_StealGold(object sender, EventArgs e)
    {
        bank.WithdrawGold(goldPenalty);
    }
}
