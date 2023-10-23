using UnityEngine;

public class CameraMoveScript : MonoBehaviour
{
    public Transform holderTransform;
    private ICameraMovement iCamera;

    void Start()
    {
        iCamera = new EditorCameraMovement(transform);
        //iCamera = new FPSCameraMovement(transform, holderTransform);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            iCamera = new FPSCameraMovement(transform, holderTransform);
        }
        else if (Input.GetMouseButtonUp(2))
        {
            iCamera = new EditorCameraMovement(transform);
        }
        iCamera.OnUpdate();
    }

}
