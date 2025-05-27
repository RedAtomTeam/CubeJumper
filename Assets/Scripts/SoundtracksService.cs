using System.Collections.Generic;
using UnityEngine;
using YG;

public class SoundtracksService : MonoBehaviour
{
    public static SoundtracksService Instance { get; private set; }

    [Header("Target Character Life System")]
    [SerializeField] private PlayerLifeChecker _playerLifeChecker;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _soundtracks = new List<AudioClip>();
    [SerializeField] private bool isSoundtracksPlaying = false;
    [SerializeField] private float defaultVolume = 1.0f;
    [SerializeField] private float volumeChangeSpeed = 0.6f;

    private int targetClip = 0;

    private void OnEnable()
    {
        YandexGame.onHideWindowGame += OnTabLostFocus;
        YandexGame.onShowWindowGame += OnTabRegainedFocus;
    }

    private void OnDisable()
    {
        YandexGame.onHideWindowGame -= OnTabLostFocus;
        YandexGame.onShowWindowGame -= OnTabRegainedFocus;
    }

    private void OnTabLostFocus()
    {
        StopImmediatelySoundtracks();
    }

    private void OnTabRegainedFocus()
    {
        StartSoundtracks();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartSoundtracks();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void StartSoundtracks()
    {
        isSoundtracksPlaying = true;
    }

    public void StopSoundtracks()
    {
        isSoundtracksPlaying = false;
    }

    public void StartImmediatelySoundtracks()
    {
        isSoundtracksPlaying = true;
        _audioSource.UnPause();
        _audioSource.volume = defaultVolume;
    }

    public void StopImmediatelySoundtracks()
    {
        isSoundtracksPlaying = false;
        _audioSource.Pause();
        _audioSource.volume = 0f;
    }

    void Update()
    {
        if (isSoundtracksPlaying)
        {
            if (!_audioSource.isPlaying)
            {
                targetClip += 1;
                if (targetClip >= _soundtracks.Count)
                    targetClip = 0;
                _audioSource.clip = _soundtracks[targetClip];
                _audioSource.Play();
            }
            if (_audioSource.volume < defaultVolume)
                _audioSource.volume += volumeChangeSpeed * Time.deltaTime;
            if (_audioSource.volume > defaultVolume)
                _audioSource.volume = defaultVolume;
        }   
        else
        {
            if (_audioSource.volume > 0f)
                _audioSource.volume -= volumeChangeSpeed * Time.deltaTime;
            if (_audioSource.volume < 0f)
                _audioSource.volume = 0f;
            if (_audioSource.volume == 0f)
                _audioSource.UnPause();
        }
    }
}
