using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Transform ARightTarget, BRightTarget, ALeftTarget, BLeftTarget;

    private void OnMouseDown()
    {
        if (RouteManager.RouteCreationMode)
        {
            FindObjectOfType<RouteManager>().EditRoutePoint(gameObject);
        }
    }
}
