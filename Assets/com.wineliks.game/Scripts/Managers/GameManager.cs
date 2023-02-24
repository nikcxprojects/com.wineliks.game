using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => FindObjectOfType<GameManager>(); }

    public static bool GamePaused { get; set; }

    private GameObject LevelRef { get; set; }

    [Space(10)]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject game;
    [SerializeField] GameObject leaders;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject pause;
    [SerializeField] GameObject result;


    private void Start()
    {
        OpenMenu();
    }

    public void OpenLeaders(bool IsOpen)
    {
        menu.SetActive(!IsOpen);
        leaders.SetActive(IsOpen);
    }

    public void OpenSettings(bool IsOpen)
    {
        menu.SetActive(!IsOpen);
        settings.SetActive(IsOpen);
    }

    public void OpenGame()
    {
        menu.SetActive(false);
        result.SetActive(false);

        game.SetActive(true);

        if(LevelRef)
        {
            Destroy(LevelRef);
        }

        LevelRef = Instantiate(Resources.Load<GameObject>("level"), GameObject.Find("Environment").transform);
    }

    public void OpenMenu()
    {
        GamePaused = false;

        if(LevelRef)
        {
            Destroy(LevelRef);
        }

        game.SetActive(false);
        pause.SetActive(false);
        result.SetActive(false);

        menu.SetActive(true);
    }

    public void SetPause(bool IsPause)
    {
        GamePaused = IsPause;
        pause.SetActive(IsPause);
    }

    public void OpenResult()
    {
        Destroy(LevelRef);

        game.SetActive(false);
        result.SetActive(true);
    }
}
