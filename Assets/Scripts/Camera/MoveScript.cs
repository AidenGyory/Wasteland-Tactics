using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lerpSpeed = 5f;

    public Vector3 lerpPosition;
    public bool isLerping; 

    private void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();

        if(isLerping)
        {
            Vector3 _currentPosition = Vector3.Lerp(transform.position, lerpPosition, lerpSpeed * Time.deltaTime);

            transform.position = _currentPosition;

            if(transform.position == lerpPosition )
            {
                isLerping = false;
            }
        }
    }

    private void HandleKeyboardInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();

        transform.Translate(movement * moveSpeed * Time.deltaTime);

        //isLerping = false;
    }

    private bool isDragging = false;
    private Vector3 dragStartPosition;

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isDragging = true;
            dragStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 currentMousePosition = Input.mousePosition;
            Vector3 mouseMovement = currentMousePosition - dragStartPosition;

            float mouseX = mouseMovement.x;
            float mouseY = mouseMovement.y;

            Vector3 movement = new Vector3(mouseX, 0f, mouseY);
            movement.Normalize();

            transform.Translate(-movement * moveSpeed * Time.deltaTime);
        }
    }


}
