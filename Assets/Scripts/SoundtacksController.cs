using System.Collections.Generic;
using UnityEngine;

public class SoundtacksController : MonoBehaviour
{
    [Header("Target Character Life System")]
    [SerializeField] private PlayerLifeChecker _playerLifeChecker;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _soundtracks = new List<AudioClip>();
    [SerializeField] private bool isSoundtracksPlaying = false;
    [SerializeField] private float defaultVolume = 1.0f;
    [SerializeField] private float volumeChangeSpeed = 0.6f;

    private int targetClip = 0;

    private void Start()
    {
        StartSoundtracks();
        if (_playerLifeChecker is not null)
            _playerLifeChecker.dieEvent += StopSoundtracks;
    }

    private void StartSoundtracks()
    {
        _audioSource.clip = _soundtracks[targetClip];
        _audioSource.Play();
        isSoundtracksPlaying = true;
    }

    private void StopSoundtracks()
    {
        isSoundtracksPlaying = false;
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
                _audioSource.Stop();
        }
    }
}
