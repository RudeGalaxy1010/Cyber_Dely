using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform cameraTransform;

    [Space(10)]
    public float movementTime;
    public bool canMove = false;
    [Space(10)]
    public float movementSpeed;
    public float rotationAmount;
    public float zoomAmount;
    [Space(10)]
    public Vector2 CameraRestrictions;

    private Vector3 newPosition;
    private Quaternion newRotation;
    private Vector3 newZoom;

    private void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.transform.localPosition;
    }

    private void LateUpdate()
    {
        InputHandler();
    }

    private void InputHandler()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.W))
            {
                newPosition += (transform.forward * movementSpeed);
            }
            if (Input.GetKey(KeyCode.S))
            {
                newPosition -= (transform.forward * movementSpeed);
            }
            if (Input.GetKey(KeyCode.D))
            {
                newPosition += transform.right * movementSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {
                newPosition -= transform.right * movementSpeed;
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            newZoom += Vector3.forward * zoomAmount;
        }
        if (Input.GetKeyDown(KeyCode.F) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            newZoom -= Vector3.forward * zoomAmount;
        }


        newZoom = new Vector3(0, 0, Mathf.Clamp(newZoom.z, 0, 10));
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, -CameraRestrictions.x, CameraRestrictions.x), newPosition.y, Mathf.Clamp(newPosition.z, -CameraRestrictions.y, CameraRestrictions.y));


        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * movementTime);
    }
}
