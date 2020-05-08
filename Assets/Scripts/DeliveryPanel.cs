using UnityEngine;
using UnityEngine.UI;

public class DeliveryPanel : MonoBehaviour
{
    public Text Name, Weight, Route;

    public void SetValues(Delivery delivery)
    {
        Name.text = "name: " + delivery.Name;
        Weight.text = "weight: " + delivery.Weight + " kg";
        Route.text = "route: " + delivery.StartPoint.GetComponent<City>().name + " --> " + delivery.DestinationPoint.GetComponent<City>().name;
    }
}
