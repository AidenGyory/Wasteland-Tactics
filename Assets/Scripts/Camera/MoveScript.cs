using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float lerpSpeed = 5f;

    public Vector3 lerpPosition;
    public bool isLerping; 

    private bool _isDragging = false;
    private Vector3 _dragStartPosition;
    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleKeyboardInput();
        HandleMouseInput();

        if (!isLerping) return;
        
        var currentPosition = Vector3.Lerp(transform.position, lerpPosition, lerpSpeed * Time.deltaTime);

        transform.position = currentPosition;

        if (transform.position == lerpPosition )
        {
            isLerping = false;
        }
    }

    private void HandleKeyboardInput()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        var movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();
        var movementSpeed = movement * (moveSpeed * Time.deltaTime);
        
        _characterController.Move(movementSpeed);
    }

    
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isDragging = true;
            _dragStartPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _isDragging = false;
        }

        if (!_isDragging) return;
        
        var currentMousePosition = Input.mousePosition;
        var mouseMovement = currentMousePosition - _dragStartPosition;

        var mouseX = mouseMovement.x;
        var mouseY = mouseMovement.y;

        var movement = new Vector3(mouseX, 0f, mouseY);
        movement.Normalize();
        
        transform.Translate(-movement * (moveSpeed * Time.deltaTime));
    }


}
