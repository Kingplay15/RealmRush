using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;
    [SerializeField] private float buildDelay = 1f;
    private Bank bank;

    private void Start()
    {
        StartCoroutine(Build());
    }

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

    private IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child.transform)
                grandchild.gameObject.SetActive(false);
        }
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            foreach (Transform grandchild in child.transform)
                grandchild.gameObject.SetActive(true);
        }
    }
}
