using System;
using TMPro;
using UnityEngine;
using YG;

public class Timer : MonoBehaviour 
{
    private bool _isTimerEnable = false;

    [SerializeField] private PlayerLifeChecker playerLifeChecker;

    [SerializeField] private TextMeshProUGUI _maxTimeText;
    [SerializeField] private TextMeshProUGUI _currentTimeText;

    private float _maxTime;
    private float _currentTime;

    private void Start()
    {
        _maxTime = YandexGame.savesData.maxTimeInSeconds;
        playerLifeChecker.dieEvent += StopTimer;
        StartTimer();
    }

    public void StartTimer()
    {
        _isTimerEnable = true;
    }

    private void Update()
    {
        TimeUpdate();
        UpdateUI();
    }

    public void TimeUpdate()
    {
        if (_isTimerEnable)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime > _maxTime)
            {
                _maxTime = _currentTime;
                SaveService.SaveTime((int)_maxTime);
            }
        }
    }

    public void UpdateUI()
    {
        var time = TimeSpan.FromSeconds(_currentTime);
        _currentTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                time.Hours,
                time.Minutes,
                time.Seconds);
        time = TimeSpan.FromSeconds(_maxTime);
        _maxTimeText.text = string.Format("{0:D2}:{1:D2}:{2:D2}",
                time.Hours,
                time.Minutes,
                time.Seconds); ;
    }


    public void StopTimer()
    {
        _isTimerEnable = false;
    }
}
