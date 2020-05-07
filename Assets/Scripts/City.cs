using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tile))]
public class City : MonoBehaviour
{
    private DeliveryManager DM;

    public GameObject Panel;

    private void Start()
    {
        DM = FindObjectOfType<DeliveryManager>();
        Panel.SetActive(false);
    }

    public void SpawnDelivery()
    {
        Panel.SetActive(true);
    }

    public void ViewDelivery()
    {
        Panel.SetActive(false);
        DM.OnDeliveryAccept();
    }
}
