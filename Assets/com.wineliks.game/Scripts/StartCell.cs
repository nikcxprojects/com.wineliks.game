using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class StartCell : MonoBehaviour
{
    private bool IsStarted { get; set; }

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnMouseDown()
    {
        if (GameManager.GamePaused || IsStarted)
        {
            return;
        }

        IsStarted = true;
        gameObject.layer = 2;

        Elements.IsActive = false;
        GameManager.Instance.DisableControlls();

        FindObjectOfType<BallAI>().StartTravelling();
    }
}
