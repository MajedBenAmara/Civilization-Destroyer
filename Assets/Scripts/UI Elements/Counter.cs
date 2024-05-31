using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI CounterText;
    private float _elapsedTime;
    [HideInInspector] public int Seconds, Minutes;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        Seconds = Mathf.FloorToInt(_elapsedTime / 60);
        Minutes = Mathf.FloorToInt(_elapsedTime % 60);

        CounterText.text = string.Format("{0:00}:{1:00}", Seconds, Minutes);
    }
}
