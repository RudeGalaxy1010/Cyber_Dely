using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float Speed;
    public List<Vector3> Route = new List<Vector3>();
    private Vector3 NextPoint;

    public void SetRoute(List<Vector3> route)
    {
        Route.AddRange(route);
        NextPoint = Route[1];
    }

    private void LateUpdate()
    {
        if (NextPoint != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPoint, Time.deltaTime * Speed);
            transform.LookAt(NextPoint);
        }
        else if (Route.IndexOf(NextPoint) != Route.Count - 1)
        {
            NextPoint = Route[Route.IndexOf(NextPoint) + 1];
        }
        else
        {
            Debug.Log("Done");
            FindObjectOfType<RouteManager>().OnDeliveryDone();
            Destroy(gameObject);
        }
    }
}
