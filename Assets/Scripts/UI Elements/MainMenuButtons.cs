using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : ButtonsBehaviour
{
    public RectTransform ScoreBoardTransform;
    public RectTransform MenuButtons;
    public GameObject CloseButton;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }
    public void ShowScoreBoard()
    {
        CloseButton.SetActive(true);
        MenuButtons.gameObject.SetActive(false);
        ScoreBoardTransform.gameObject.SetActive(true);
    }

    public void HideScoreBoard()
    {
        MenuButtons.gameObject.SetActive(true);
        ScoreBoardTransform.gameObject.SetActive(false);
        CloseButton.SetActive(false);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}
