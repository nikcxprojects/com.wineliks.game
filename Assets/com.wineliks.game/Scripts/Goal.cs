using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.CheckResult(true);

        collision.GetComponent<BallAI>().StopTravelling();
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
}
