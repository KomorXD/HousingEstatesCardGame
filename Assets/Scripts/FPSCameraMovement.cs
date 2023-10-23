using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraMovement : ICameraMovement
{
    private Transform cameraTransform;
    private Transform holderTransform;

    private float rotX;
    private float sensX = 1.0f;
    private float sensY = 1.0f;

    public FPSCameraMovement(Transform cameraTransform, Transform holderTransform)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        this.cameraTransform = cameraTransform;
        this.holderTransform = holderTransform;
    }

    public void OnUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90, 90);

        cameraTransform.localRotation = Quaternion.Euler(rotX, 0, 0);
        holderTransform.Rotate(Vector3.up * mouseX);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 wishMove = holderTransform.right * horizontalInput + holderTransform.forward * verticalInput;
        holderTransform.position += wishMove.normalized * 5.0f * Time.deltaTime;
    }
}
