using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float mouseSensitivity = 100f;

    private float verticalRotation = 0f;
    private Transform cameraTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Get the camera's transform
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Handle player movement
        float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Translate(xValue, 0, zValue);

        // Handle camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        // Limit vertical camera rotation to prevent over-rotation
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
