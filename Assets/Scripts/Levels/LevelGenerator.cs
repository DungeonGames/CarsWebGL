using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level _firstLevel;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private List<Wave> _waves;

    private int _currentWave;
    private bool _isFirstRun;
    private const string FirsRun = "FirsRun";
    private const string CurrentWave = "CurrentWave";

    public event UnityAction<int> NumberWaveChange;
    public event UnityAction<Wave> WaveChange;

    private void Awake()
    {
        Load();

        if (_isFirstRun)
        {
            _isFirstRun = false;
            Instantiate(_firstLevel);
        }
        else
        {
            int randomIndex = Random.Range(0, _levels.Count);
            Instantiate(_levels[randomIndex]);
        }

        Save();
    }

    public void SetWaveCount()
    {
        _currentWave++;
        Save();
    }

    private void Load()
    {
        var dataFirstRun = SaveSystem.Load<SaveData.PlayerData>(FirsRun);
        var dataCurrentWave = SaveSystem.Load<SaveData.PlayerData>(CurrentWave);

        _isFirstRun = dataFirstRun.IsFirstRun;

        if (dataCurrentWave.CurrentWave > _waves.Count)
        {
            int index = Random.Range(1, _waves.Count);
            WaveChange?.Invoke(_waves[index]);
        }
        else
        {
            WaveChange?.Invoke(_waves[dataCurrentWave.CurrentWave]);
        }

        _currentWave = dataCurrentWave.CurrentWave;
    }

    private void Start()
    {
        NumberWaveChange?.Invoke(_currentWave + 1);
    }

    private void Save()
    {
        SaveSystem.Save(FirsRun, GetSaveSnapshot());
        SaveSystem.Save(CurrentWave, GetSaveSnapshot());
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            IsFirstRun = _isFirstRun,
            CurrentWave = _currentWave,
        };

        return data;
    }
}
