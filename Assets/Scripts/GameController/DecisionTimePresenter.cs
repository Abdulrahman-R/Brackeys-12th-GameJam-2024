using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DecisionTimePresenter : MonoBehaviour
{
    [SerializeField] DecisionTimeCounter _decisionTimeCounter;
    [SerializeField] TextMeshProUGUI _timerText;
    // Start is called before the first frame update
    void Start()
    {
        _decisionTimeCounter.onTimeChange += UpdateTimerText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTimerText()
    {
        _timerText.text = _decisionTimeCounter._currentTime.ToString("F1");
    }
}
