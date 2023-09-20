using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private void Update()
    {
        // Get the mouse movement.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Rotate the camera around the Y axis.
        transform.Rotate(Vector3.up * mouseX * mouseSensitivity * Time.deltaTime);

        // Rotate the camera around the X axis.
      //  transform.Rotate(Vector3.right * mouseY * mouseSensitivity * Time.deltaTime);
    }
}