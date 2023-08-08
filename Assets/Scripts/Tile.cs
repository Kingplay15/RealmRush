using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private bool isPlaceable = false;
    public bool IsPlaceable { get => isPlaceable; }

    [SerializeField] private Tower towerPrefab;

    private GridManager gridManager;
    private PathFinder pathFinder;
    private Vector2Int coordinates;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void Start()
    {
        coordinates = gridManager.GetCoordintesFromPosition(transform.position);
        if (!isPlaceable)
            gridManager.BlockNode(coordinates);
    }

    private void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathFinder.WillBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(transform.position);
            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathFinder.NotifyReceivers();
            }
                
        }
    }
}
