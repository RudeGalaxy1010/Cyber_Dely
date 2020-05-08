using System.Collections.Generic;
using UnityEngine;

public class RouteManager : MonoBehaviour
{
    public static bool RouteCreationMode = false;
    public GameObject RouteMarkerPrefab, FirstRouteMarkerPrefab, LastRouteMarkerPrefab, MapViewRouteStartMarkerPrefab, MapViewRouteEndMarkerPrefab;
    public float MarkersHeight = 10f;
    [Space(10)]
    public GameObject CarPrefab;

    private List<GameObject> Route = new List<GameObject>();
    private List<GameObject> Markers = new List<GameObject>();
    private GameObject StartMarker, EndMarker;
    private GameObject Destination;

    public void CreateRoute(GameObject StartPoint)
    {
        ClearAll();
        RouteCreationMode = true;
        EditRoutePoint(StartPoint);
    }

    public void EditRoutePoint(GameObject point)
    {
        if (Route.Count < 1)
        {
            Route.Add(point);
            Markers.Add(Instantiate(FirstRouteMarkerPrefab, point.transform.position + Vector3.up * MarkersHeight, Quaternion.identity, transform));
        }
        else
        {
            if (Route.Count > 1 && Route[Route.Count - 1] == point)
            {
                Route.Remove(point);
                DelMarker(Markers.Count - 1);
                if (Markers.Count > 1 && point.transform.position + Vector3.up * MarkersHeight != Markers[1].transform.position)
                {
                    Markers.Add(Instantiate(LastRouteMarkerPrefab, Markers[Markers.Count - 1].transform.position, Quaternion.identity, transform));
                    DelMarker(Markers.Count - 2);
                }
            }
            else if (!Route.Contains(point))
            {
                if (point.transform.position.x != Route[Route.Count - 1].transform.position.x && point.transform.position.z != Route[Route.Count - 1].transform.position.z)
                    return;

                Route.Add(point);
                if (Markers.Count > 1)
                {
                    Markers.Add(Instantiate(RouteMarkerPrefab, Markers[Markers.Count - 1].transform.position, Quaternion.identity, transform));
                    DelMarker(Markers.Count - 2);
                }
                Markers.Add(Instantiate(LastRouteMarkerPrefab, point.transform.position + Vector3.up * MarkersHeight, Quaternion.identity, transform));
            }
        }
    }

    public bool OnRouteEnd()
    {
        if (!RouteCreationMode || Route.Count < 2 || Route[Route.Count -1] != Destination)
            return false;

        Destroy(StartMarker);
        Destroy(EndMarker);

        RouteCreationMode = false;
        Transform spawnPosition;
        if (Route[1].transform.position.x - Route[0].transform.position.x > 0 || Route[1].transform.position.z - Route[0].transform.position.z > 0)
            spawnPosition = Route[0].GetComponent<Tile>().BRightTarget;
        else
            spawnPosition = Route[0].GetComponent<Tile>().ALeftTarget;
        Car car = Instantiate(CarPrefab, spawnPosition.position, Quaternion.identity, transform).GetComponent<Car>();
        car.SetRoute(Route);

        return true;
    }

    public void ShowRoute(GameObject A, GameObject B)
    {
        StartMarker = Instantiate(MapViewRouteStartMarkerPrefab, A.transform.position + Vector3.up * MarkersHeight * 1.5f, Quaternion.identity, transform);
        EndMarker = Instantiate(MapViewRouteEndMarkerPrefab, B.transform.position + Vector3.up * MarkersHeight * 1.5f, Quaternion.identity, transform);
        Destination = B;
    }

    public void OnDeliveryDone()
    {
        ClearAll();
        FindObjectOfType<DeliveryManager>().GenerateDelivery();
    }

    private void DelMarker(int index)
    {
        Destroy(Markers[index]);
        Markers.RemoveAt(index);
    }

    public void ClearAll()
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
