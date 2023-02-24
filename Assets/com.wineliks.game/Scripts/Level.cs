using UnityEngine;

public class Level : MonoBehaviour
{
    private bool IsReady { get; set; }

    private Vector2 velocity = Vector2.zero;
    private const float smoothTime = 0.35f;

    private void Start()
    {
        transform.position = new Vector3(0, 13.0f);
    }

    private void Update()
    {
        if (IsReady)
        {
            return;
        }

        transform.position = Vector2.SmoothDamp(transform.position, Vector2.zero, ref velocity, smoothTime);
        if (Vector2.Distance(transform.position, Vector2.zero) < 1.0f)
        {
            IsReady = true;
        }
    }
}
