using UnityEngine;
using Lean.Localization;

public class WaveView : MonoBehaviour
{
    [SerializeField] private WavesManager _wavesManager;
    //[SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private LeanToken _currentWaveToken;

    private void OnEnable()
    {
        _wavesManager.MinorWaveEnded += OnCurrentWaveChanged;
        _wavesManager.MajorWaveEnded += OnCurrentWaveChanged;
    }

    private void OnDisable()
    {
        _wavesManager.MinorWaveEnded -= OnCurrentWaveChanged;
        _wavesManager.MajorWaveEnded -= OnCurrentWaveChanged;
    }

    private void OnCurrentWaveChanged()
    {
        _currentWaveToken.SetValue(_wavesManager.CurrentWave);
    }
}
