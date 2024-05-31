using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private int _maxScoreboardEntry = 5;
    [SerializeField] private Transform _highScoreHolderTransform;
    [SerializeField] private GameObject _scoreboardEntryObject;

    public bool NameAlreadyExist = false;

    [Header("Test")]
    [SerializeField] private string Name ;
    [SerializeField] private int Score;

    private string _savePath => $"{Application.persistentDataPath}/HighScore.json";

    public static Scoreboard Instance;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ScoreboardSaveData savedSores = GetSavedScores();

        SaveScores(savedSores);

        UpdateUI(savedSores);
    }

    [ContextMenu("Add test Entry")]
    public void AddTestEntry()
    {
        AddEntry(new ScoreboardEntryData()
        {
            EntryName = "Mark",
            EntryScoreText = "00:02",
            EntryScoreNumber = 1
        });
    }

    public void AddEntry(ScoreboardEntryData scoreboardEntryData)
    {
        ScoreboardSaveData savedScores = GetSavedScores();
        NameAlreadyExist = false;
        if (savedScores.HighScores.Count != 0)
        {
            for (int i = 0; i < savedScores.HighScores.Count; i++)
            {
                if (scoreboardEntryData.EntryName == savedScores.HighScores[i].EntryName)
                {
                    NameAlreadyExist = true;
                    Debug.Log("NameAlreadyExist = true");
                }
            }
        }
        else
            NameAlreadyExist = false;

        bool scoreAdded = false;
        if (!NameAlreadyExist)
        {

            //Check if the score is high enough to be added.
            for (int i = 0; i < savedScores.HighScores.Count; i++)
            {
                if (scoreboardEntryData.EntryScoreNumber > savedScores.HighScores[i].EntryScoreNumber)
                {
                    savedScores.HighScores.Insert(i, scoreboardEntryData);
                    scoreAdded = true;
                    break;
                }
            }

            //Check if the score can be added to the end of the list.
            if (!scoreAdded && savedScores.HighScores.Count < _maxScoreboardEntry)
            {
                savedScores.HighScores.Add(scoreboardEntryData);
            }

            //Remove any scores past the limit.
            if (savedScores.HighScores.Count > _maxScoreboardEntry)
            {
                savedScores.HighScores.RemoveRange(_maxScoreboardEntry, savedScores.HighScores.Count - _maxScoreboardEntry);
            }

            // Remove the Biggest Time and add in it's time the new small one
            if(savedScores.HighScores.Count == _maxScoreboardEntry && 
                savedScores.HighScores[savedScores.HighScores.Count-1].EntryScoreNumber > scoreboardEntryData.EntryScoreNumber)
            {
                savedScores.HighScores[savedScores.HighScores.Count - 1] = scoreboardEntryData;
            }

            // Organize the list 
            for (int i = 0; i < savedScores.HighScores.Count; i++)
            {
                for (int j = i+1; j < savedScores.HighScores.Count; j++)
                {
                    if (savedScores.HighScores[j].EntryScoreNumber < savedScores.HighScores[i].EntryScoreNumber)
                    {
                        ScoreboardEntryData tmp = savedScores.HighScores[i];
                        savedScores.HighScores[i] = savedScores.HighScores[j];
                        savedScores.HighScores[j] = tmp;
                    }
                }
            }

            UpdateUI(savedScores);

            SaveScores(savedScores);

        }
    }

    private void UpdateUI(ScoreboardSaveData savedScores)
    {
        foreach (Transform child in _highScoreHolderTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (ScoreboardEntryData highscore in savedScores.HighScores)
        {
            Instantiate(_scoreboardEntryObject, _highScoreHolderTransform).GetComponent<ScoreboardEntryUI>().Initialize(highscore);
        }
    }

    private ScoreboardSaveData GetSavedScores()
    {
        if (!File.Exists(_savePath))
        {
            File.Create(_savePath).Dispose();
            return new ScoreboardSaveData();
        }

        using (StreamReader stream = new StreamReader(_savePath))
        {
            string json = stream.ReadToEnd();

            return JsonUtility.FromJson<ScoreboardSaveData>(json);
        }
    }

    private void SaveScores(ScoreboardSaveData scoreboardSaveData)
    {
        using (StreamWriter stream = new StreamWriter(_savePath))
        {
            string json = JsonUtility.ToJson(scoreboardSaveData, true);
            stream.Write(json);
        }
    }
}

