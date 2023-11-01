using UnityEngine;

//! Class responsible for camera controls and properties
public class CameraMoveScript : MonoBehaviour
{
    [Header("Settings")]
    //! Parent body's transform
    public Transform holderTransform;

    //! Current camera rotation in Quaternion form
    public Quaternion cameraRot;

    //! Player speed
    public float speed = 15.0f;

    //! Gravity force
    public float gravityForce = -60.0f;

    //! Force with which player jumps
    public float jumpHeight = 3.0f;

    //! Multiplier by which scroll input gets magnified
    public float scrollMultiplier = 10.0f;

    //! Current X rotation
    public float rotationX;

    //! Current Y rotation
    public float rotationY;

    //! Sensitivity over X axis
    public float sensitivityX = 1.0f;

    //! Sensitivity over Y axis
    public float sensitivityY = 1.0f;

    //! Camera's distance from Vector3.zero
    public float cameraDistanceFromOrigin = 30.0f;

    //! Camera's rotation Slerp value
    public float editorCameraSlerpValue = 15.0f;

    //! Camera's position Lerp value
    public float editorCameraLerpValue = 15.0f;

    [SerializeField] private ICameraMovement cameraMoveMode;
    public ICameraMovement CameraMode { get { return cameraMoveMode; } set { cameraMoveMode = value; } }

    //! Reads set rotation and assigns it, default to EditorCameraMovement
    void Start()
    {
        rotationX = transform.localRotation.eulerAngles.x;
        rotationY = holderTransform.localRotation.eulerAngles.y;
        
        cameraMoveMode = new EditorCameraMovement(this);
    }

    //! Check for change of camera mode, update current one
    void Update()
    {
        cameraMoveMode.OnUpdate();
    }

}
