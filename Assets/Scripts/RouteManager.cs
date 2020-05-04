using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public static bool RouteCreationMode = false;
    public GameObject RouteMarkerPrefab;
    public float MarkersHeight = 5f;
    [Space(10)]
    public float CarYOffset;
    public GameObject CarPrefab;

    [SerializeField]
    private List<Vector3> Route = new List<Vector3>();
    [SerializeField]
    private List<GameObject> Markers = new List<GameObject>();

    private void Start()
    {
        CreateRoute();
    }

    public void CreateRoute()
    {
        ClearAll();
        RouteCreationMode = true;
    }

    public void EditRoutePoint(Vector3 point)
    {
        if (Route.Count < 1)
        {
            Route.Add(point);
            Markers.Add(Instantiate(RouteMarkerPrefab, point + Vector3.up * MarkersHeight, Quaternion.identity, transform));
        }
        else
        {
            if (Route.Count != 1 && Route[Route.Count - 1] == point)
            {
                Route.Remove(point);
                DelMarker(Markers.Count - 1);
            }
            else if (!Route.Contains(point))
            {
                if (point.x != Route[Route.Count - 1].x && point.z != Route[Route.Count - 1].z)
                    return;

                Route.Add(point);
                Markers.Add(Instantiate(RouteMarkerPrefab, point + Vector3.up * MarkersHeight, Quaternion.identity, transform));
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && RouteCreationMode)
        {
            OnRouteEnd();
        }
        if (Input.GetKeyDown(KeyCode.R) && !RouteCreationMode)
        {
            CreateRoute();
        }
    }

    public void OnRouteEnd()
    {
        if (!RouteCreationMode)
            return;

        RouteCreationMode = false;
        Car car = Instantiate(CarPrefab, Route[0] + Vector3.up * CarYOffset, Quaternion.identity, transform).GetComponent<Car>();
        for (int i = 0; i < Route.Count; i++)
        {
            Route[i] += Vector3.up * CarYOffset;
        }
        car.SetRoute(Route);
    }

    public void OnDeliveryDone()
    {
        ClearAll();
    }

    private void DelMarker(int index)
    {
        Destroy(Markers[index]);
        Markers.RemoveAt(index);
    }

    private void ClearAll()
    {
        Route.Clear();
        if (Markers.Count > 0)
        {
            foreach (GameObject marker in Markers)
            {
                Destroy(marker);
            }
            Markers.Clear();
        }
    }
}
