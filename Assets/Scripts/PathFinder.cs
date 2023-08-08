using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] private Vector2Int startCoordinates;
    public Vector2Int StartCoordinates { get => startCoordinates; }
    [SerializeField] private Vector2Int endCoordinates;
    public Vector2Int EndCoordinates { get => endCoordinates; }

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
        SetUpPath();
    }

    private void Start()
    {        
        GetNewPath();
    }

    private void SetUpPath()
    {
        startNode = grid[startCoordinates];
        startNode.isWalkable = true;
        endNode = grid[endCoordinates];
        endNode.isWalkable = true;
    }

    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreathFirstSearch();
        return BuildPath();
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
        frontier.Clear();
        exploreds.Clear();

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

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            bool previousState = grid[coordinates].isWalkable;
            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousState;
            if (newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", SendMessageOptions.DontRequireReceiver);
    }
}
