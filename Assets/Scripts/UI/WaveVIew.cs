using UnityEngine;
using TMPro;
using Lean.Localization;

public class WaveView : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private LeanToken _currentWaveToken;

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
        _currentWaveToken.SetValue(waveNumber);
    }
}
