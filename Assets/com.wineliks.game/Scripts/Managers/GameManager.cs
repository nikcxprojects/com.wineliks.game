using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => FindObjectOfType<GameManager>(); }

    public static bool GamePaused { get; set; }

    private GameObject LevelRef { get; set; }
    private GameObject worldElementRef;

    [SerializeField] GameObject background;

    [Space(10)]
    [SerializeField] GameObject menu;
    [SerializeField] GameObject game;
    [SerializeField] GameObject leaders;
    [SerializeField] GameObject settings;

    [Space(10)]
    [SerializeField] GameObject pause;
    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;

    [Space(10)]
    [SerializeField] RotateBtn rotateBtn;
    [SerializeField] DeleteBtn deleteBtn;

    private float timer;
    [Space(10)]
    [SerializeField] Text timerText;

    private void Start()
    {
        OpenMenu();
    }

    private void Update()
    {
        if(game.activeSelf && !win.activeSelf && !lose.activeSelf && !GamePaused)
        {
            timer += Time.deltaTime;

            float min = Mathf.RoundToInt(timer / 60);
            float sec = Mathf.Round(timer % 60);

            timerText.text = string.Format("{0:00}:{1:00}", min,sec);
        }
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
        timer = 0;
        background.SetActive(false);

        foreach(LevelContainer lc in FindObjectsOfType<LevelContainer>())
        {
            lc.ForceUpdate();
        }

        win.SetActive(false);
        lose.SetActive(false);
        menu.SetActive(false);

        game.SetActive(true);

        if(LevelRef)
        {
            Destroy(LevelRef);
        }

        LevelRef = Instantiate(Resources.Load<GameObject>("level"), GameObject.Find("Environment").transform);
        Elements.IsActive = true;
    }

    public void OpenMenu()
    {
        GamePaused = false;

        if(LevelRef)
        {
            Destroy(LevelRef);
        }

        background.SetActive(true);

        win.SetActive(false);
        lose.SetActive(false);
        game.SetActive(false);
        pause.SetActive(false);

        menu.SetActive(true);
    }

    public void SetPause(bool IsPause)
    {
        GamePaused = IsPause;
        pause.SetActive(IsPause);
        BallAI.Sleep = IsPause;
    }

    public void SetWorldElement(GameObject _worldElementRef)
    {
        worldElementRef = _worldElementRef;
        rotateBtn.SetActive = true;
        deleteBtn.SetActive = true;
    }

    public void RotateWorldElement()
    {
        if(!worldElementRef)
        {
            return;
        }

        worldElementRef.transform.Rotate(0, 0, 90);
    }

    public void DisableControlls()
    {
        rotateBtn.SetActive = false;
        deleteBtn.SetActive = false;
    }

    public void CheckResult(bool IsWin)
    {
        if(SettingsManager.VibraEnbled)
        {
            Handheld.Vibrate();
        }

        if(IsWin)
        {
            SFXManager.Instance.PlayEffect(1);
            StatsUtility.Level++;
            win.SetActive(true);
        }
        else
        {
            SFXManager.Instance.PlayEffect(0);
            lose.SetActive(true);
        }
    }

    public void DeleteWorldElement()
    {
        if(!worldElementRef)
        {
            return;
        }

        FindObjectOfType<RotateBtn>().SetActive = false;
        FindObjectOfType<DeleteBtn>().SetActive = false;

        Destroy(worldElementRef);
        worldElementRef = null;
    }
}
