using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Reference to character controller.
    Rigidbody2D rb2D;

    // Initialize input vector.
    Vector2 moveVector;
    Vector2 lastMoveVector;

    // Movement speed of the player.
    [SerializeField] float moveSpeed = 2.0f;

    // Reference to the animator component.
    Animator animator;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        // Subscribe to the move event.
        Actions.MoveEvent += GetInputVector;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        UpdateAnimation();
    }

    void MovePlayer()
    {
        // Move the player using rigidbody position and velocity scaled.
        rb2D.MovePosition(rb2D.position + moveVector * moveSpeed * Time.fixedDeltaTime);
    }

    void GetInputVector(Vector2 inputDirection)
    {
        // If there is movement input, store the new vector. 
        if (inputDirection != Vector2.zero)
        {
            moveVector = inputDirection;
            lastMoveVector = moveVector.normalized;
        }
        else
        {
            // No input, stay idle.
            moveVector = Vector2.zero;
        }
    }

    public void ResetVelocity()
    {
        rb2D.velocity = Vector2.zero;
    }

    public bool SetCanMove(bool canMove)
    {
        if (canMove)
        {
            Actions.MoveEvent += GetInputVector;
        }
        else
        {
            Actions.MoveEvent -= GetInputVector;
        }
        return canMove;
    }
    void UpdateAnimation()
    {
        bool isMoving = moveVector != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            // Set the MoveDirection parameters for the Animator (for movement)
            animator.SetFloat("MoveDirectionX", moveVector.x);
            animator.SetFloat("MoveDirectionY", moveVector.y);
        }
        else
        {
            animator.SetFloat("MoveDirectionX", lastMoveVector.x);
            animator.SetFloat("MoveDirectionY", lastMoveVector.y);
        }
    }
}
