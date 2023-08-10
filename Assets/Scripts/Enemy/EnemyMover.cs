using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] [Range(0f, 5f)] private float moveSpeed = 1f;
    private List<Node> path = new List<Node>();

    private GridManager gridManager;
    private PathFinder pathFinder;

    public EventHandler OnReachingDestinationEvent;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<PathFinder>();
    }

    private void OnEnable()
    {        
        ReturnToStart();
        RecalculatePath(true);       
    }

    private void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates;
        if (resetPath)
            coordinates = pathFinder.StartCoordinates;
        else coordinates = gridManager.GetCoordintesFromPosition(transform.position);

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++) 
        {
            Vector3 startingPosition = transform.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * moveSpeed;
                transform.position = Vector3.Lerp(startingPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
        OnReachingDestination();
    }

    private void OnReachingDestination()
    {
        OnReachingDestinationEvent?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }
}
