using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardEntryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _entryNameText = null;
    [SerializeField] private TextMeshProUGUI _entryScoreText = null;

    public void Initialize(ScoreboardEntryData scoreboardEntryData)
    {
        _entryNameText.text = scoreboardEntryData.EntryName;
        _entryScoreText.text = scoreboardEntryData.EntryScoreText;
    }

    private void Update()
    {
        if(transform.parent==null)
        {
            Destroy(gameObject);
        }
    }

}
