using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private YandexLeaderboard _leaderboard;
    [SerializeField] private Level _firstLevel;
    [SerializeField] private List<Level> _levels;
    [SerializeField] private List<Wave> _waves;

    private int _currentWave;
    private int _currentLevel;
    private bool _isFirstRun;
    private const string FirsRun = "FirsRun";
    private const string CurrentWave = "CurrentWave";
    private const string CurrentLevel = "CurrentLevel";

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
            if (_currentLevel == -1)
            {
                int randomIndex = Random.Range(0, _levels.Count);
                Instantiate(_levels[randomIndex]);
                _currentLevel = randomIndex;
            }
            else
            {
                Instantiate(_levels[_currentLevel]);
            }           
        }
    }

    private void Start()
    {
        NumberWaveChange?.Invoke(_currentWave + 1);
    }

    public void SaveCurrentLevel()
    {
        SaveSystem.Save(CurrentLevel, SaveLevel());
    }

    public void SetWaveCount()
    {
        _currentWave++;
        _currentLevel = -1;

#if YANDEX_GAMES
        SaveLeaderboard();
#endif

        Save();
    }

    private void Load()
    {
        var dataFirstRun = SaveSystem.Load<SaveData.PlayerData>(FirsRun);
        var dataCurrentWave = SaveSystem.Load<SaveData.PlayerData>(CurrentWave);
        var dataCurrentLevel = SaveSystem.Load<SaveData.PlayerData>(CurrentLevel);

        _isFirstRun = dataFirstRun.IsFirstRun;
        _currentLevel = dataCurrentLevel.CurrentLevel;

        if (dataCurrentWave.CurrentWave >= _waves.Count)
        {
            int index = Random.Range(0, _waves.Count);
            WaveChange?.Invoke(_waves[index]);
        }
        else
        {
            WaveChange?.Invoke(_waves[dataCurrentWave.CurrentWave]);
        }

        _currentWave = dataCurrentWave.CurrentWave;
    }

    private void SaveLeaderboard()
    {
       _leaderboard.AddPlayerToLeaderboard(_currentWave + 1);  
    }

    private void Save()
    {        
        SaveSystem.Save(FirsRun, GetSaveSnapshot());
        SaveSystem.Save(CurrentWave, GetSaveSnapshot());
        SaveSystem.Save(CurrentLevel, GetSaveSnapshot());
    }

    private SaveData.PlayerData SaveLevel()
    {
        var data = new SaveData.PlayerData()
        {
            CurrentLevel = _currentLevel,
        };

        return data;
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            IsFirstRun = _isFirstRun,
            CurrentWave = _currentWave,
            CurrentLevel = _currentLevel,
        };

        return data;
    }
}
