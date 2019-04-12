using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float rotationSpeed = 100f;

    private void Update()
    {
        float angleRotation = rotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * angleRotation, Space.World);
    }
}
