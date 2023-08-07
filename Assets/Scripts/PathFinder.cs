using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    [SerializeField] private Vector2Int endCoordinates;

    private Node startNode;
    private Node currentSearchNode;
    private Node endNode;

    Dictionary<Vector2Int, Node> exploreds = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontier = new Queue<Node>();

    private Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    private GridManager gridManager;
    private Dictionary<Vector2Int, Node> grid;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        grid = gridManager.Grid;        
    }

    private void Start()
    {
        startNode = grid[startCoordinates];
        endNode = grid[endCoordinates];
        BreathFirstSearch();
        BuildPath();
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoors = currentSearchNode.coordinates + direction;
            if (grid.ContainsKey(neighborCoors))
            {
                neighbors.Add(grid[neighborCoors]);
            }
        }
        foreach (Node neighbor in neighbors)
        {
            if (!exploreds.ContainsKey(neighbor.coordinates) && neighbor.isWalkable) 
            {
                neighbor.connectedTo = currentSearchNode;
                frontier.Enqueue(neighbor);
                exploreds.Add(neighbor.coordinates, neighbor);
            }            
        }
    }

    private void BreathFirstSearch()
    {
        frontier.Enqueue(startNode);
        exploreds.Add(startCoordinates, startNode);

        while (frontier.Count > 0)
        {
            currentSearchNode = frontier.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();
            if (currentSearchNode.coordinates == endCoordinates)
                break;
        }
    }

    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != null)
        {
            currentNode.isPath = true;
            path.Add(currentNode);
            currentNode = currentNode.connectedTo;
        }
        path.Reverse();
        return path;
    }
}
