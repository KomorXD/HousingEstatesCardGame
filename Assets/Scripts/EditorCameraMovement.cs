using UnityEngine;

//! Class responsible for editor camera controls
public class EditorCameraMovement : ICameraMovement
{
    private CameraMoveScript cms;

    //! Initializes internal state
    public EditorCameraMovement(CameraMoveScript cms)
    {
        this.cms = cms;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        cms.cameraDistanceFromOrigin = Vector3.Distance(Vector3.zero, cms.transform.position);
        cms.transform.LookAt(Vector3.zero);
    }

    //! Checks for mouse input and updates camera ccordingly
    public void OnUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            cms.rotationX += -Input.GetAxis("Mouse Y") * cms.sensitivityX;
            cms.rotationY += Input.GetAxis("Mouse X") * cms.sensitivityY;
        }

        cms.rotationX = Mathf.Clamp(cms.rotationX, -90, 90);
        
        cms.holderTransform.position += 1.0f * Input.mouseScrollDelta.y * cms.transform.forward;
        cms.cameraDistanceFromOrigin = Vector3.Distance(Vector3.zero, cms.transform.position);

        Vector3 dir = new Vector3(0, 0, -cms.cameraDistanceFromOrigin);
        Quaternion newQ = Quaternion.Euler(cms.rotationX, cms.rotationY, 0);
        Vector3 newHolderPos = cms.cameraRot * dir;

        cms.cameraRot = Quaternion.Slerp(cms.cameraRot, newQ, cms.editorCameraSlerpValue * Time.deltaTime);
        cms.holderTransform.position = Vector3.Lerp(cms.holderTransform.position, newHolderPos, cms.editorCameraLerpValue * Time.deltaTime);
        cms.transform.LookAt(Vector3.zero);
    }
}
