using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float Speed;
    public List<GameObject> Route = new List<GameObject>();
    private Vector3 NextPoint;

    private int index = 0;
    private float xDelta = 0, zDelta = 0;

    public void SetRoute(List<GameObject> route)
    {
        Route.AddRange(route);
        NextPoint = transform.position;
    }

    private void LateUpdate()
    {
        if (NextPoint != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, NextPoint, Time.deltaTime * Speed);
            transform.LookAt(NextPoint);
        }
        else if (index != Route.Count - 1)
        {
            xDelta = Route[index].GetComponent<Tile>().RightTarget.position.x - transform.position.x;
            zDelta = Route[index].GetComponent<Tile>().RightTarget.position.z - transform.position.z;
            index++;
            if (xDelta > 0 || zDelta > 0)
                NextPoint = Route[index].GetComponent<Tile>().LeftTarget.position;
            else
                NextPoint = Route[index].GetComponent<Tile>().RightTarget.position;
        }
        else
        {
            FindObjectOfType<RouteManager>().OnDeliveryDone();
            Destroy(gameObject);
        }
    }
}
