using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public Camera camera;
    public float sensX = 1.0f;
    public float sensY = 1.0f;
    public float cameraDistance = 10.0f;
    public float slerpVal = 0.25f;

    private float _rotX;
    private float _rotY;
    private Quaternion _cameraRot;

    private void Awake()
    {
        if(camera == null)
        {
            camera = Camera.main;
        }
    }

    void Start()
    {
        cameraDistance = Vector3.Distance(Vector3.zero, camera.transform.position);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _rotX += -Input.GetAxis("Mouse Y") * sensX;
            _rotY += Input.GetAxis("Mouse X") * sensY;
        }

        camera.transform.position += camera.transform.forward * 1.0f * Input.mouseScrollDelta.y;
        cameraDistance = Vector3.Distance(Vector3.zero, camera.transform.position);
    }
    
    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -cameraDistance);
        Quaternion newQ = Quaternion.Euler(_rotX, _rotY, 0);

        _cameraRot = Quaternion.Slerp(_cameraRot, newQ, slerpVal);
        camera.transform.position = _cameraRot * dir;
        camera.transform.LookAt(Vector3.zero);
    }
}
