using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelUIManager : MonoBehaviour
{
    private LevelManager levelManager;

    [SerializeField] private Button nextLevelButton;

    #region Text Fields

    [SerializeField] private TextMeshProUGUI coinsCollected;
    [SerializeField] private TextMeshProUGUI perfectFlips;
    [SerializeField] private TextMeshProUGUI coinsTotalCollected;

    #endregion

    #region Buttons panels

    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject endgameButtonPanel;

    #endregion

    #region Stars

    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;

    #endregion

    private void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    public void ShowEndLevelMenu(int currentLevel, int collected, int total, int perfect, int totalCollected)
    {
        ResetStars();
        float percent = CalculateStars(collected, total, perfect);
        FillStars(percent);

        gameObject.SetActive(true);
        coinsCollected.text = collected.ToString();
        perfectFlips.text = perfect.ToString();
        coinsTotalCollected.text = (totalCollected + collected).ToString();

        if (currentLevel == 5)
        {
            endgameButtonPanel.SetActive(true);
            buttonsPanel.SetActive(false);
        }
        else
        {
            endgameButtonPanel.SetActive(false);
            buttonsPanel.SetActive(true);

            nextLevelButton.interactable = !(collected == 0);
        }
    }

    private void ResetStars()
    {
        star1.fillAmount = 0f;
        star2.fillAmount = 0f;
        star3.fillAmount = 0f;
    }

    private float CalculateStars(int collected, int total, int perfect)
    {
        if(collected == 0) 
        {
            return 0f;        
        }

        float collectedPercent = (float)collected / total;
        float perfectPercent = (float)perfect / collected;

        float totalPercent = collectedPercent + perfectPercent / 2;

        Debug.Log(totalPercent);
        return totalPercent;
    }

    private void FillStars(float totalPercent)
    {
        if (totalPercent < 0.33f)
        {
            star1.fillAmount = totalPercent / 0.33f;
        }
        else if (totalPercent >= 0.33f && totalPercent < 0.66)
        {
            star1.fillAmount = 1f;
            star2.fillAmount = (totalPercent - 0.33f) / 0.33f;
        }
        else if(totalPercent >= 0.66 && totalPercent < 1f)
        {
            star1.fillAmount = 1f;
            star2.fillAmount = 1f;
            star3.fillAmount = (totalPercent - 0.66f) / 0.33f;
        }
        else
        {
            star1.fillAmount = 1f;
            star2.fillAmount = 1f;
            star3.fillAmount = 1f;
        }
    }

    #region Button Handlers

    public void NextLevel_BtnClick()
    {
        gameObject.SetActive(false);
        levelManager.NextLevel();
    }

    public void Replay_BtnClick()
    {
        gameObject.SetActive(false);
        levelManager.Replay();
    }

    public void ReplayAll_BtnClick()
    {
        gameObject.SetActive(false);
        levelManager.ReplayAll();
    }

    public void MainMenu_BtnClick()
    {
        SceneManager.LoadScene(0);
    }

    #endregion
}
