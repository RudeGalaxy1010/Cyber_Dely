using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public List<GameObject> Cities = new List<GameObject>();

    public Delivery delivery;
    private RouteManager RM;    

    private void Start()
    {
        RM = FindObjectOfType<RouteManager>();
        GenerateDelivery();
    }

    public void GenerateDelivery()
    {
        delivery = new Delivery {  };
        Cities[Random.Range(0, Cities.Count)].GetComponent<City>().SpawnDelivery();
    }

    public void OnDeliveryAccept()
    {
        RM.CreateRoute();
    }

    public void OnDeliveryDeny()
    {
        Debug.Log("Denied");
    }
}

[System.Serializable]
public class Delivery
{
    public string Name;
    public float Weight;
}
