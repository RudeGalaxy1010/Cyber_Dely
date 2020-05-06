using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform RightTarget, LeftTarget;

    private void OnMouseDown()
    {
        if (RouteManager.RouteCreationMode)
        {
            FindObjectOfType<RouteManager>().EditRoutePoint(gameObject);
        }
    }
}
