using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Data")]
    public float Score;
    public TextMeshProUGUI ScoreText;

    public RectTransform HP_bg;
    public RectTransform HP_fg;
    public RectTransform EXP;

    public GameObject GameoverPanel;
    public TextMeshProUGUI GameoverScore;

    static GameManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        instance.ScoreText.text = "0";
    }

    public static void UpdateScore(float val)
    {
        instance.Score += val;
        int intScore = (int)instance.Score;
        instance.ScoreText.text = intScore.ToString();
    }

    public static void UpdateHPUI(float currentHP, float maxHP)
    {
        instance.HP_bg.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxHP / 20f);
        instance.HP_fg.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentHP / 20f);
    }

    public static void UpdateEXPUI(float width)
    {
        instance.EXP.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * 15f);
    }

    public static void GameOver()
    {
        Time.timeScale = 0f;
        instance.GameoverPanel.SetActive(true);
        instance.GameoverScore.text = ((int)instance.Score).ToString();
    }

    public void onButtonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }
    public void onButtonQuick()
    {
        Application.Quit();
    }
}
