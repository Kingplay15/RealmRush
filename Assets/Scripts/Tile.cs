using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool isPlaceable = false;
    public bool IsPlaceable { get => isPlaceable; }

    [SerializeField] private Tower towerPrefab;

    private GridManager gridManager;
    private Vector2Int coordinates;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Start()
    {
        coordinates = gridManager.GetCoordintesFromPosition(transform.position);
        if (!isPlaceable)
            gridManager.BlockNode(coordinates);
    }

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(transform.position);
            isPlaceable = !isPlaced;
        }
    }
}
