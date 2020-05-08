using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public List<GameObject> Cities = new List<GameObject>();

    public Delivery delivery;
    public GameObject deliveryPanel, mapViewPanel, routeAcceptionPanel;

    private GameObject CurrentCity;
    private RouteManager RM;

    private GameObject lastCity, city;

    private void Start()
    {
        RM = FindObjectOfType<RouteManager>();
        GenerateDelivery();

        deliveryPanel.SetActive(false);
        mapViewPanel.SetActive(false);
        routeAcceptionPanel.SetActive(false);
    }

    public void GenerateDelivery()
    {
        if (Cities.Count < 2)
            return;

        CurrentCity = GetRandomCity();
        delivery = new Delivery { Name = "Test", Weight = 0.1f, StartPoint = CurrentCity, DestinationPoint = GetRandomCity() };
        CurrentCity.GetComponent<City>().SpawnDelivery();
    }

    public void OnViewDelivery()
    {
        deliveryPanel.GetComponent<DeliveryPanel>().SetValues(delivery);
        deliveryPanel.SetActive(true);
    }

    public void OnDeliveryMapView()
    {
        mapViewPanel.SetActive(true);
        RM.ShowRoute(delivery.StartPoint, delivery.DestinationPoint);
    }

    public void OnDeliveryAccept()
    {
        RM.CreateRoute(CurrentCity);
        routeAcceptionPanel.SetActive(true);
    }

    public void OnDeliveryDeny()
    {
        RM.ClearAll();
        GenerateDelivery();
    }

    public void OnRouteCheck()
    {
        if (RM.OnRouteEnd())
        {
            routeAcceptionPanel.SetActive(false);
        }
    }

    private GameObject GetRandomCity()
    {
        while (city == lastCity)
        {
            city = Cities[Random.Range(0, Cities.Count)];
        }
        lastCity = city;
        return city;
    }
}

[System.Serializable]
public class Delivery
{
    public string Name;
    public float Weight;
    public GameObject StartPoint, DestinationPoint;
}
