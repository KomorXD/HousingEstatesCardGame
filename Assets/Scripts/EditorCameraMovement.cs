using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorCameraMovement : ICameraMovement
{
    private Transform cameraTransform;
    private float rotX;
    private float rotY;
    private float sensX = 1.0f;
    private float sensY = 1.0f;
    private float slerpVal = 0.25f;
    private float cameraDistance = 10.0f;

    private Quaternion _cameraRot;

    public EditorCameraMovement(Transform cameraTransform)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        this.cameraTransform = cameraTransform;
        cameraDistance = Vector3.Distance(Vector3.zero, cameraTransform.position);
    }

    public void OnUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            rotX += -Input.GetAxis("Mouse Y") * sensX;
            rotY += Input.GetAxis("Mouse X") * sensY;
        }

        cameraTransform.position += cameraTransform.forward * 1.0f * Input.mouseScrollDelta.y;
        cameraDistance = Vector3.Distance(Vector3.zero, cameraTransform.position);

        Vector3 dir = new Vector3(0, 0, -cameraDistance);
        Quaternion newQ = Quaternion.Euler(rotX, rotY, 0);

        _cameraRot = Quaternion.Slerp(_cameraRot, newQ, slerpVal);
        cameraTransform.position = _cameraRot * dir;
        cameraTransform.LookAt(Vector3.zero);
    }

}
