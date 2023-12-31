using UnityEngine;

//! Class responsible for editor camera controls
public class EditorCameraMovement : ICameraMovement
{
    private CameraMoveScript cms;

    /**
     * Intializes internal state
     * 
     * \param cms parent CameraMoveScript
     */
    public EditorCameraMovement(CameraMoveScript cms)
    {
        this.cms = cms;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        cms.transform.LookAt(Vector3.zero);
    }

    //! Checks for mouse input and updates camera accordingly
    public void OnUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            cms.rotationX += -Input.GetAxis("Mouse Y") * cms.sensitivityX;
            cms.rotationY += Input.GetAxis("Mouse X") * cms.sensitivityY;
        }

        cms.rotationX = Mathf.Clamp(cms.rotationX, -90, 90);
        
        cms.holderTransform.position += cms.scrollMultiplier * Input.mouseScrollDelta.y * cms.transform.forward;
        cms.cameraDistanceFromOrigin = Vector3.Distance(Vector3.zero, cms.transform.position);

        Vector3 dir = new(0, 0, -cms.cameraDistanceFromOrigin);
        Quaternion newQ = Quaternion.Euler(cms.rotationX, cms.rotationY, 0);
        Vector3 newHolderPos = cms.cameraRot * dir;

        cms.cameraRot = Quaternion.Slerp(cms.cameraRot, newQ, cms.editorCameraSlerpValue * Time.deltaTime);
        cms.holderTransform.position = Vector3.Lerp(cms.holderTransform.position, newHolderPos, cms.editorCameraLerpValue * Time.deltaTime);
        cms.transform.LookAt(Vector3.zero);
    }
}
