using UnityEngine;

public class WorldElement : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager.Instance.SetWorldElement(gameObject);
    }
}
