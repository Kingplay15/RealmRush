using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.red;
    [SerializeField] private Color blockedColor = Color.grey;

    private TextMeshPro label;
    private Vector2Int coordiantes = new Vector2Int();
    private Waypoint waypoint;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Waypoint>();
        label.enabled = false;
        DisplayCoordinates();
    }

    // Update is called once per frame
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
        if (waypoint.IsPlaceable)
            label.color = defaultColor;
        else label.color = blockedColor;
    }

    private void DisplayCoordinates()
    {
        //Todo: Move this script into an Editor folder when building the game
        coordiantes.x = Mathf.RoundToInt(-transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordiantes.y = Mathf.RoundToInt(-transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordiantes.x.ToString() + "," + coordiantes.y.ToString();
    }

    private void UpdateName()
    {
        transform.parent.gameObject.name = label.text;
    }
}
