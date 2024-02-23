using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerUpgrade : MonoBehaviour
{
    [SerializeField] private PlayerMover _movement;
    [SerializeField] private Gun _currentGun;
    [SerializeField] private float _minorUpdateCoef = 1.01f;
    [SerializeField] private float _majorUpgradeCoef = 1.1f;

    [SerializeField] private WavesManager _wavesManager;

    private void Start()
    {
        _wavesManager.MinorWaveEnded += MinorUpdate;
        _wavesManager.MajorWaveEnded += MajorUpdate;
    }

    private void MinorUpdate()
    {
        switch (GetRandomUpgradeType())
        {
            case UpgradeType.Damage:
                _currentGun.UpgradeDamage(_minorUpdateCoef);
                break;
            case UpgradeType.FireRate:
                _currentGun.UpgradeFireRate(_minorUpdateCoef);
                break;
            case UpgradeType.MoveSpeed:
                _movement.UpgradeSpeed(_minorUpdateCoef);
                break;
        }
    }

    private void MajorUpdate()
    {
        _currentGun.UpgradeDamage(_majorUpgradeCoef);
        _currentGun.UpgradeFireRate(_majorUpgradeCoef);
        _movement.UpgradeSpeed(_majorUpgradeCoef);
    }

    /// <summary>
    /// For car or weapon model change
    /// </summary>
    private void UpdateModule()
    {
    }

    private UpgradeType GetRandomUpgradeType()
    {
        var arr = Enum.GetValues(typeof(UpgradeType));

        int test = Random.Range(0, arr.Length);
        return (UpgradeType)arr.GetValue(test);
    }

    enum UpgradeType
    {
        Damage,
        FireRate,
        MoveSpeed
    }
}