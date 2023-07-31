using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;
    private Bank bank;

    public bool CreateTower(Vector3 position)
    {
        bank = FindObjectOfType<Bank>();

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(gameObject, position, Quaternion.identity);
            bank.WithdrawGold(cost);
            return true;
        }

        else return false;
    }
}
