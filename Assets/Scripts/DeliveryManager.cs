using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public Delivery delivery;

    private RouteManager RM;

    private void Start()
    {
        RM = FindObjectOfType<RouteManager>();
        OnDeliveryAccept();
    }

    public void GetDelivery()
    {
        delivery = new Delivery { };
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
