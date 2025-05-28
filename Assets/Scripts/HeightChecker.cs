using TMPro;
using UnityEngine;
using YG;

public class HeightChecker : MonoBehaviour
{
    [SerializeField] private GameObject? _player;
    
    [SerializeField] private TextMeshProUGUI _maxHeightText;
    [SerializeField] private TextMeshProUGUI _currentHeightText;

    private float _maxHeight;
    private float _currentHeight;

    private void Start()
    {
        _maxHeight = YandexGame.savesData.maxHeight;
        _currentHeight = (int)_player?.transform.position.y;
    }

    void Update()
    {
        HeightUpdate();
        UpdateUI();
    }

    public void HeightUpdate()
    {
        if (_player != null)
        {
            _currentHeight = _player.transform.position.y;
            if (_currentHeight > _maxHeight)
            {
                _maxHeight = _currentHeight;
                SaveService.SaveHeight((int)_maxHeight);
            }
        }
    }

    public void UpdateUI()
    {
        _currentHeightText.text = ((int)_currentHeight).ToString();
        _maxHeightText.text = ((int)_maxHeight).ToString();
    }


}
