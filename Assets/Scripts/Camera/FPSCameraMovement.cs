using UnityEngine;

//! Class responsible for FPS camera controls
public class FPSCameraMovement : ICameraMovement
{
    private CameraMoveScript cms;
    private CharacterController cc;

    private float velocityY = 0.0f;

    /**
     * Intializes internal state, adds character controller component
     * 
     * \param cms parent CameraMoveScript
     */
    public FPSCameraMovement(CameraMoveScript cms)
    {
        this.cms = cms;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        cc = player.GetComponent<CharacterController>();
        cms.holderTransform.position = new(-0.5f, 10.0f, -1.0f);

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

        if(wishMove.magnitude > 0.0f)
        {
            wishMove = wishMove.normalized * cms.speed;
        }

        // Jumping and gravity
        if (!cc.isGrounded)
        {
            velocityY += cms.gravityForce * Time.deltaTime;
        }
        else
        {
            velocityY = Input.GetKeyDown(KeyCode.Space) ? Mathf.Sqrt(-2.0f * cms.gravityForce * cms.jumpHeight) : 0.0f;
        }

        wishMove += velocityY * Vector3.up;
        cc.Move(wishMove * Time.deltaTime);
    }
}
