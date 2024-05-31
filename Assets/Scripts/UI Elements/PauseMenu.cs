using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : ButtonsBehaviour
{
    public RectTransform PauseMenuTransform;
    public Button PauseButtonSprite;
    public Sprite PauseImage, PlayImage;
    public Scoreboard ScoreboardPrefab;
    public RectTransform ScoreSubmissionUI;
    public TMP_InputField NameInputField;
    public Counter ScoreCounter;
    public Button SubmitButton;
    public TextMeshProUGUI ErrorText;

    public void ActivatePauseMenu()
    {
        if(PauseMenuTransform.gameObject.activeSelf == true)
        {
            PauseMenuTransform.gameObject.SetActive(false);
            PauseButtonSprite.image.sprite = PauseImage;
            Time.timeScale = 1f;
        }
        else
        {
            PauseMenuTransform.gameObject.SetActive(true);
            PauseButtonSprite.image.sprite = PlayImage;
            Time.timeScale = 0f;
        }
    }

    public void ShowScoreSubmission()
    {
        ScoreSubmissionUI.gameObject.SetActive(true);
        SubmitButton.gameObject.SetActive(true);
    }

    public void HideScoreSubmission()
    {
        ScoreSubmissionUI.gameObject.SetActive(false);
    }

    public void OnSubmit()
    {
        string name = NameInputField.text;
        ScoreboardEntryData entry = new ScoreboardEntryData
        {
            EntryName = name,
            EntryScoreNumber = ScoreCounter.Minutes + ScoreCounter.Seconds * 100,
            EntryScoreText = string.Format("{0:00}:{1:00}", ScoreCounter.Seconds, ScoreCounter.Minutes),
        };

        ScoreboardPrefab.AddEntry(entry);
        if (ScoreboardPrefab.NameAlreadyExist)
        {
            StartCoroutine(ShowErrorText());
        }
        else
        {
            SubmitButton.gameObject.SetActive(false);
            ScoreSubmissionUI.gameObject.SetActive(false);
        }
    }

    IEnumerator ShowErrorText()
    {
        ErrorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        ErrorText.gameObject.SetActive(false);
    }

}
