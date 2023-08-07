using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private List<Tile> path = new List<Tile>();
    [SerializeField] [Range(0f, 5f)] private float moveSpeed = 1f;

    public EventHandler OnReachingDestinationEvent;

    private void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
    }

    private void FindPath()
    {
        path.Clear();

        GameObject enemyPath = GameObject.FindGameObjectWithTag("Path");
        foreach(Transform child in enemyPath.transform)
        {
            path.Add(child.GetComponent<Tile>());
        }
    }

    private void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    private IEnumerator FollowPath()
    {
        foreach (Tile wayponint in path)
        {
            Vector3 startingPosition = transform.position;
            Vector3 endPosition = wayponint.transform.position;
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
