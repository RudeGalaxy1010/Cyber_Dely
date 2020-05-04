using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (RouteManager.RouteCreationMode)
        {
            FindObjectOfType<RouteManager>().EditRoutePoint(transform.position);
        }
    }
}
