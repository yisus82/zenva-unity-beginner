using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;

    private Rigidbody rb;
    private Collider col;

    private bool pressedJump = false;
    private Vector3 playerSize;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        playerSize = col.bounds.size;
    }

    private void FixedUpdate()
    {
        WalkHandler();
        JumpHandler();
    }

    private void JumpHandler()
    {
        float jumpAxis = Input.GetAxis("Jump");

        if (jumpAxis > 0)
        {
            bool isGrounded = CheckGrounded();

            if (!pressedJump && isGrounded)
            {
                pressedJump = true;
                Vector3 jumpVector = new Vector3(0f, jumpAxis * jumpForce, 0f);
                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }
            
        } else
        {
            pressedJump = false;
        }
    }

    private bool CheckGrounded()
    {
        Vector3 corner1 = transform.position + new Vector3(playerSize.x / 2, -playerSize.y / 2 + 0.01f, playerSize.z / 2);
        Vector3 corner2 = transform.position + new Vector3(-playerSize.x / 2, -playerSize.y / 2 + 0.01f, playerSize.z / 2);
        Vector3 corner3 = transform.position + new Vector3(playerSize.x / 2, -playerSize.y / 2 + 0.01f, -playerSize.z / 2);
        Vector3 corner4 = transform.position + new Vector3(-playerSize.x / 2, -playerSize.y / 2 + 0.01f, -playerSize.z / 2);

        bool grounded1 = Physics.Raycast(corner1, Vector3.down, 0.01f);
        bool grounded2 = Physics.Raycast(corner2, Vector3.down, 0.01f);
        bool grounded3 = Physics.Raycast(corner3, Vector3.down, 0.01f);
        bool grounded4 = Physics.Raycast(corner4, Vector3.down, 0.01f);

        return grounded1 || grounded2 || grounded3 || grounded4;
    }

    private void WalkHandler()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * walkSpeed * Time.fixedDeltaTime;
        float moveVertical = Input.GetAxis("Vertical") * walkSpeed * Time.fixedDeltaTime;
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
        Vector3 newPosition = transform.position + movement;
        rb.MovePosition(newPosition);
    }
}
