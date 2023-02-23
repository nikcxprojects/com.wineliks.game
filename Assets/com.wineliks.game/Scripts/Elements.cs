using UnityEngine;

public class Elements : MonoBehaviour
{
    private static GameObject thisObject;

    private void Awake()
    {
        thisObject = gameObject;
    }

    public static bool IsActive
    {
        set => thisObject.SetActive(value);
    }
}
