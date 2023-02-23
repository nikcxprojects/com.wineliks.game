using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteBtn : MonoBehaviour, IPointerExitHandler
{
    public bool SetActive
    {
        set => gameObject.SetActive(value);
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.DeleteWorldElement();
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        FindObjectOfType<RotateBtn>().SetActive = false;
        SetActive = false;
    }
}
