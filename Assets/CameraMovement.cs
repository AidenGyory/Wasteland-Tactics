using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float dragSpeed = 2f;
    public float smoothTime = 0.2f;
    public float deceleration = 10f;
    public float stopThreshold = 0.01f;
    public float zoomSpeed = 5f;
    public float minZoomY = 1f;
    public float maxZoomY = 10f;
    public float zoomSmoothTime = 0.2f;

    private bool isDragging;
    private Vector3 dragOrigin;
    private Vector3 currentVelocity;
    private bool isSliding;
    [SerializeField] private float targetZoomY;
    private float currentZoomVelocity;

    private void Update()
    {
        

        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;
            isSliding = false;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
            isSliding = true;
        }

        if (isDragging)
        {
            Vector3 mouseDelta = (Input.mousePosition - dragOrigin) * -dragSpeed * Time.deltaTime * transform.position.y;
            Vector3 newPosition = transform.position + new Vector3(mouseDelta.x, 0f, mouseDelta.y);
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(newPosition.x, transform.position.y, newPosition.z), ref currentVelocity, smoothTime);
            dragOrigin = Input.mousePosition;
        }
        else if (isSliding)
        {
            transform.position += currentVelocity * Time.deltaTime;

            if (currentVelocity.magnitude < stopThreshold)
            {
                isSliding = false;
                currentVelocity = Vector3.zero;
            }
            else
            {
                currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
            }
        }
        else
        {
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        float scrollDelta = Input.mouseScrollDelta.y;
        if (scrollDelta != 0f)
        {
            targetZoomY = Mathf.Clamp(transform.position.y - scrollDelta * zoomSpeed, minZoomY, maxZoomY);
        }

        float newZoomY = Mathf.SmoothDamp(transform.position.y, targetZoomY, ref currentZoomVelocity, zoomSmoothTime);
        transform.position = new Vector3(transform.position.x, newZoomY, transform.position.z);
    }
}
