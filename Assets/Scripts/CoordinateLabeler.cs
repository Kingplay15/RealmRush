using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordiantes = new Vector2Int();

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
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
    }

    private void DisplayCoordinates()
    {
        coordiantes.x = Mathf.RoundToInt(-transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordiantes.y = Mathf.RoundToInt(-transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordiantes.x.ToString() + "," + coordiantes.y.ToString();
    }

    private void UpdateName()
    {
        transform.parent.gameObject.name = label.text;
    }
}
