using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public float rangeY = 2f;

    private Vector3 initialPosition;
    private int direction = 1;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        float factor = direction == -1 ? 2f : 1f;
        float movementY = speed * factor * direction * Time.deltaTime;
        float newY = transform.position.y + movementY;
        if (Mathf.Abs(newY - initialPosition.y) > rangeY)
        {
            direction *= -1;
        }
        else
        {
            transform.position += new Vector3(0f, movementY, 0f);
        }
    }
}
