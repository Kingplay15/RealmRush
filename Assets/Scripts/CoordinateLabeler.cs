using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color blockedColor = Color.grey;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, 0.5f, 0f); //Orange

    private GridManager gridManager;

    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        gridManager = FindObjectOfType<GridManager>();
        label.enabled = false;
        DisplayCoordinates();
    }

    private void Update()
    {
        if (!Application.isPlaying) //Executes in editor mode only
        {
            DisplayCoordinates();
            UpdateName();
        }
        SetLabelsColor();
        ToggleLabels();
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
            label.enabled = !label.IsActive();
    }

    private void SetLabelsColor()
    {
        if (gridManager == null)
            return;
        Node node = gridManager.GetNode(coordinates);
        if (node == null)
            return;

        if (!node.isWalkable)
            label.color = blockedColor;
        else if (node.isPath)
            label.color = pathColor;
        else if (node.isExplored)
            label.color = exploredColor;
        else label.color = defaultColor;
    }

    private void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(-transform.parent.position.x / gridManager.UnityGridSize);
        coordinates.y = Mathf.RoundToInt(-transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x.ToString() + "," + coordinates.y.ToString();
    }

    private void UpdateName()
    {
        transform.parent.gameObject.name = label.text;
    }
}
