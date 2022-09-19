using UnityEngine;
using TMPro;

public class WaveVIew : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private TMP_Text _currentWaveText;
    [SerializeField] private TMP_Text _currentWaveOnRewardText;

    private void OnEnable()
    {
        _levelGenerator.NumberWaveChange += OnCurrentWaveChanged;
    }

    private void OnDisable()
    {
        _levelGenerator.NumberWaveChange -= OnCurrentWaveChanged;
    }

    private void OnCurrentWaveChanged(int waveNumber)
    {
        _currentWaveText.text = $"Wave {waveNumber}";
        _currentWaveOnRewardText.text = $"WAVE {waveNumber}";
    }
}
