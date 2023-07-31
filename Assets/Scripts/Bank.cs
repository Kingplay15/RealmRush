using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;
    [SerializeField] private int currentBalance = 0;
    public int CurrentBalance { get => currentBalance; }

    
    void Start()
    {
        currentBalance = startingBalance;
    }

    public void DepositGold(int amount)
    {
        currentBalance += amount;
    }

    public void WithdrawGold(int amount)
    {
        currentBalance -= amount;
    }
}
