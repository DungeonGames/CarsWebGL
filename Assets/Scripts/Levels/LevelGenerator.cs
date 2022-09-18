using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Level _firstLevel;
    [SerializeField] private List<Level> _levels;

    private Level _currentLevel;
    private bool _isFirstRun;
    private const string FirsRun = "FirsRun";

    private void Awake()
    {
        Load();

        if (_isFirstRun)
        {
            _isFirstRun = false;
            Instantiate(_firstLevel);
            Save();
        }
        else
        {
            int randomIndex = Random.Range(0, _levels.Count);
            Instantiate(_levels[randomIndex]);
        }
    }

    private void Load()
    {
        var data = SaveSystem.Load<SaveData.PlayerData>(FirsRun);
        _isFirstRun = data.IsFirstRun;
    }

    private void Save()
    {
        SaveSystem.Save(FirsRun, GetSaveSnapshot());
    }

    private SaveData.PlayerData GetSaveSnapshot()
    {
        var data = new SaveData.PlayerData()
        {
            IsFirstRun = _isFirstRun,
        };

        return data;
    }
}
