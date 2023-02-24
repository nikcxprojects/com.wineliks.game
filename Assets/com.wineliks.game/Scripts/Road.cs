using UnityEngine;

public class Road : MonoBehaviour
{
    private const float minX = -18.0f;
    private const float offset = 23.1f;

    [SerializeField] float speed;

    private void Update()
    {
        if (GameManager.GamePaused)
        {
            return;
        }

        foreach (Transform t in transform)
        {
            if (t.position.x < minX)
            {
                t.position = GetLastPosition + Vector2.right * offset;
                t.SetAsLastSibling();
            }

            t.Translate(speed * Time.deltaTime * Vector2.left);
        }
    }

    private Vector2 GetLastPosition
    {
        get => (Vector2)transform.GetChild(transform.childCount - 1).position;
    }
}
