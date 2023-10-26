using UnityEngine;

//! Class responsible for FPS camera controls
public class FPSCameraMovement : ICameraMovement
{
    private CameraMoveScript cms;

    //! Initializes an object
    public FPSCameraMovement(CameraMoveScript cms)
    {
        this.cms = cms;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //! Checks for mouse and keyboard input and updates camera accordingly
    public void OnUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * cms.sensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * cms.sensitivityY;

        cms.rotationX -= mouseY;
        cms.rotationX = Mathf.Clamp(cms.rotationX, -90, 90);
        cms.rotationY += mouseX;
        cms.cameraRot = Quaternion.Euler(cms.rotationX, cms.rotationY, 0);

        cms.transform.localRotation = Quaternion.Euler(cms.rotationX, 0, 0);
        cms.holderTransform.Rotate(Vector3.up * mouseX);

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 wishMove = cms.holderTransform.right * horizontalInput + cms.holderTransform.forward * verticalInput;

        if (Input.GetKey(KeyCode.Space))
        {
            wishMove += Vector3.up;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            wishMove -= Vector3.up;
        }

        cms.holderTransform.position += wishMove.normalized * 15.0f * Time.deltaTime;
    }
}
