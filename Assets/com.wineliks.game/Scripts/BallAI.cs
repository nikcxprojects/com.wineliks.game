using System.Collections;
using UnityEngine;

public class BallAI : MonoBehaviour
{
    private readonly Vector2[] Directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    private Vector2 Target { get; set; }
    private const float force = 4;

    private static Rigidbody2D Rigidbody { get; set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetBallPosition(Vector2 position)
    {
        transform.position = position;
        Target = position;
    }

    public void StartTravelling()
    {
        StartCoroutine(nameof(Travelling));
    }

    public static bool Sleep
    {
        set => Rigidbody.bodyType = value ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }

    public void StopTravelling()
    {
        StopCoroutine(nameof(Travelling));
    }

    private IEnumerator Travelling()
    {
        float et = 0.0f;
        float waitTime = 3.0f;

        while(true)
        {
            if (GameManager.GamePaused)
            {
                yield return null;
            }

            bool find = false;

            for (int i = 0; i < Directions.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Directions[i], 0.7f);
                if (hit.collider != null)
                {
                    if (hit.collider.GetType() == typeof(EdgeCollider2D))
                    {
                        continue;
                    }

                    if (hit.collider.isTrigger)
                    {
                        hit.collider.gameObject.layer = 2;
                        Target = hit.transform.position;

                        et = 0.0f;
                        find = true;
                    }
                }

                if (find)
                {
                    break;
                }
            }

            if (!find && !GameManager.GamePaused)
            {
                et += Time.deltaTime;
                if (et >= waitTime)
                {
                    GameManager.Instance.CheckResult(false);
                    yield break;
                }
            }

            Vector2 direction = (Target - (Vector2)transform.position).normalized;
            Rigidbody.AddForce(direction * force, ForceMode2D.Force);

            yield return null;
        }
    }
}
