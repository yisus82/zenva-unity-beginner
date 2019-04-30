using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;
    public float minY;
    public AudioClip coinSound;

    private Rigidbody rb;
    private Collider col;
    private AudioSource audioSource;
    private bool pressedJump = false;
    private Vector3 playerSize;
    private Vector3 cameraOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
        playerSize = col.bounds.size;
        cameraOffset = transform.position - Camera.main.transform.position;
    }

    private void FixedUpdate()
    {
        CheckFall();
        WalkHandler();
        JumpHandler();
        SetCameraPosition();
    }

    private void CheckFall()
    {
        if (transform.position.y <= minY)
        {
            GameManager.instance.GameOver();
        }
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

        }
        else
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
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(hAxis * walkSpeed * Time.fixedDeltaTime, 0f, vAxis * walkSpeed * Time.fixedDeltaTime);
        Vector3 newPosition = transform.position + movement;
        rb.MovePosition(newPosition);

        if (hAxis != 0 || vAxis != 0)
        {
            Vector3 direction = new Vector3(hAxis, 0, vAxis);
            rb.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void SetCameraPosition()
    {
        Camera.main.transform.position = transform.position - cameraOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            audioSource.clip = coinSound;
            audioSource.Play();
            GameManager.instance.IncreaseScore(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {

            GameManager.instance.GameOver();
        }
        else if (other.CompareTag("Goal"))
        {
            GameManager.instance.NextLevel();
        }
    }
}
