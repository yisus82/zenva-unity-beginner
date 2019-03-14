using UnityEngine;

public class BalloonController : MonoBehaviour
{
  public float scaleFactor = 1.2f;
  public float maxScale = 3f;

  private void OnMouseDown()
  {
    transform.localScale *= scaleFactor;
    if (transform.localScale.x >= maxScale
    || transform.localScale.y >= maxScale
    || transform.localScale.z >= maxScale)
    {
      Destroy(gameObject);
    }
  }

  private void OnValidate()
  {
    scaleFactor = Mathf.Max(scaleFactor, 1.01f);
  }
}
