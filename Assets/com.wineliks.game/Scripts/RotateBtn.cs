using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotateBtn : MonoBehaviour, IPointerExitHandler
{
    public bool SetActive
    {
        set => gameObject.SetActive(value);
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.RotateWorldElement();
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<DeleteBtn>().SetActive = false;
        SetActive = false;
    }
}
