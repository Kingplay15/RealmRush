using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int startingBalance = 150;
    [SerializeField] private int currentBalance = 0;
    public int CurrentBalance { get => currentBalance; }

    [SerializeField] private TextMeshProUGUI goldDisplay;
    
    
    void Start()
    {
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void DepositGold(int amount)
    {
        currentBalance += amount;
        UpdateDisplay();
    }

    public void WithdrawGold(int amount)
    {
        currentBalance -= amount;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        goldDisplay.text = "GOLD: " + currentBalance.ToString();
    }
}
