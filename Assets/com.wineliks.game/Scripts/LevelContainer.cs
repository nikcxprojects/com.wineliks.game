using UnityEngine;
using UnityEngine.UI;

public class LevelContainer : MonoBehaviour
{
    private Text Text { get; set; }

    private void Awake()
    {
        Text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        Text.text = $"LVL {StatsUtility.Level}";
    }

    public void ForceUpdate()
    {
        Text.text = $"LVL {StatsUtility.Level}";
    }
}
