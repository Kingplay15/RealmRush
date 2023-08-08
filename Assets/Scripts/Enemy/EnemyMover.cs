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
        RecalculatePath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void RecalculatePath()
    {
        path.Clear();
        path = pathFinder.GetNewPath();
    }

    private void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathFinder.StartCoordinates);
    }

    private IEnumerator FollowPath()
    {
        for (int i = 0; i < path.Count; i++) 
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
