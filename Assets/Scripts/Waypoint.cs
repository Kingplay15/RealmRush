using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool isPlaceable = false;
    public bool IsPlaceable { get => isPlaceable; }

    [SerializeField] private Tower towerPrefab;
    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
