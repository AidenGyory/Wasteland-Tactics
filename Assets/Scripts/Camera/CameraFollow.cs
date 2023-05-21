using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CameraController))]
public class CameraFollow : MonoBehaviour
{
    
    public Transform followTarget;
    public Vector3 targetPosition; 
    public bool follow;

    public float smoothSpeed;
    
    public Vector3 cameraOffset;

    public float topOffsetClamp; 
    public float bottomOffsetClamp;

    public bool canScroll; 

    private Vector3 _smoothVel;

    void Start()
    {
        GetComponent<CameraController>().SetOffset(0);
    }

    void LateUpdate()
    {
        Vector3 _targetPosition = followTarget.position + cameraOffset; 

        // Y MOVEMENT
        if (canScroll)
        {
            if(Input.mouseScrollDelta.magnitude > 0)
            {
                cameraOffset.y += Input.mouseScrollDelta.y;

                if (cameraOffset.y > topOffsetClamp)
                {
                    cameraOffset.y = topOffsetClamp;
                }
                if (cameraOffset.y < bottomOffsetClamp)
                {
                    cameraOffset.y = bottomOffsetClamp;
                }
            }
        }

        // X and Z MOVEMENT 
        if (follow)
        {
            if(transform.position !=  _targetPosition)
            {
                transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _smoothVel, smoothSpeed);
            }

            float distance = Mathf.Abs(transform.position.y - _targetPosition.y);

            if (distance > 0.05f)
            {
                transform.LookAt(followTarget.position);
            }
        }
    }

    public void StartShake(float duration, float magnitude)
    {
        CameraShake cameraShake = GetComponent<CameraShake>();
        StartCoroutine(cameraShake.Shake(magnitude, duration));
    }

    public void LerpToPosition(Vector3 _target)
    {
        followTarget.GetComponent<MoveScript>().lerpPosition = _target;
        followTarget.GetComponent<MoveScript>().isLerping = true;
        GetComponent<CameraController>().SetCameraMode(CameraController.CameraMode.Focused);
        
    }
}
