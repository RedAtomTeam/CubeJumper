using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour 
{
    private float _timeInSeconds = 0;
    private bool _isTimerEnable = false;
    private TimeSpan _time;

    [SerializeField] private PlayerLifeChecker playerLifeChecker; 

    [SerializeField] private TextMeshProUGUI _textForTimer;

    private void Start()
    {
        playerLifeChecker.dieEvent += StopTimer;
        StartTimer();
    }

    public void StartTimer()
    {
        _isTimerEnable = true;
    }

    private void Update()
    {
        if (_isTimerEnable)
        {
            _timeInSeconds += Time.deltaTime;
            _time = TimeSpan.FromSeconds(_timeInSeconds);
            _textForTimer.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                _time.Hours,
                _time.Minutes,
                _time.Seconds); 
        }
    }

    public void StopTimer()
    {
        _isTimerEnable = false;
    }
}
