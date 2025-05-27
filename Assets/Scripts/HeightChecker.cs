using TMPro;
using UnityEngine;

public class HeightChecker : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    
    [SerializeField] private TextMeshProUGUI _maxHeightText;
    [SerializeField] private TextMeshProUGUI _currentHeightText;

    private float _maxHeight;
    private float _currentHeight;

    private void Start()
    {
        _maxHeight = (int)_player?.transform.position.y;
        _currentHeight = (int)_player?.transform.position.y;
    }

    void Update()
    {
        HeightUpdate();
        UpdateUI();
    }

    public void HeightUpdate()
    {
        if (_player is not null)
        {
            _currentHeight = (int)_player?.transform.position.y;
            if (_currentHeight > _maxHeight)
                _maxHeight = (int)_currentHeight;
        }
    }

    public void UpdateUI()
    {
        _currentHeightText.text = _currentHeight.ToString();
        _maxHeightText.text = _maxHeight.ToString();
    }


}
