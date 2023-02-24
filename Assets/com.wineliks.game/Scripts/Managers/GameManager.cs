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

    private float score;
    [Space(10)]
    [SerializeField] Text scoreText;


    private void Start()
    {
        OpenMenu();
    }

    private void Update()
    {
        if(GamePaused || !game.activeSelf)
        {
            return;
        }

        score += Time.deltaTime * 2.5f;
        scoreText.text = $"{score:N} m";
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
        score = 0;

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

        if(SettingsManager.VibraEnbled)
        {
            Handheld.Vibrate();
        }

        SFXManager.Instance.PlayEffect(0);

        game.SetActive(false);
        result.SetActive(true);
    }
}
