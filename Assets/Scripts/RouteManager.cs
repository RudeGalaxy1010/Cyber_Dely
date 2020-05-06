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
    private List<GameObject> Route = new List<GameObject>();
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

    public void EditRoutePoint(GameObject point)
    {
        if (Route.Count < 1)
        {
            Route.Add(point);
            Markers.Add(Instantiate(RouteMarkerPrefab, point.transform.position + Vector3.up * MarkersHeight, Quaternion.identity, transform));
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
                if (point.transform.position.x != Route[Route.Count - 1].transform.position.x && point.transform.position.z != Route[Route.Count - 1].transform.position.z)
                    return;

                Route.Add(point);
                Markers.Add(Instantiate(RouteMarkerPrefab, point.transform.position + Vector3.up * MarkersHeight, Quaternion.identity, transform));
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
        Transform spawnPosition;
        if (Route[1].transform.position.x - Route[0].transform.position.x > 0 || Route[1].transform.position.z - Route[0].transform.position.z > 0)
            spawnPosition = Route[0].GetComponent<Tile>().RightTarget;
        else
            spawnPosition = Route[0].GetComponent<Tile>().LeftTarget;
        Car car = Instantiate(CarPrefab, spawnPosition.position, Quaternion.identity, transform).GetComponent<Car>();
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
