using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeartBeatPresenter : MonoBehaviour
{
    [SerializeField] DecisionTimeCounter _decisionTimeCounter;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] int _avgHB;
    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.modeSwitcher.OnSwitchMode += UpdateHeartBeatText;
    }

    private void UpdateHeartBeatText()
    {
        int hBIncrement = (int)_decisionTimeCounter._currentTime;
        int overAllHB = _avgHB + hBIncrement;
        _timerText.text =overAllHB.ToString("F0");
    }
}
