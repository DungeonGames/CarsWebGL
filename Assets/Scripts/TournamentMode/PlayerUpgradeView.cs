using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeView : MonoBehaviour
{
    [SerializeField] private GameObject _damageUpgrade;
    [SerializeField] private GameObject _moveSpeedUpgrade;
    [SerializeField] private GameObject _reloadSpeedUpgrade;

    [SerializeField] private PlayerUpgrade _playerUpgrade;

    private float _upgradeShowTime = 3f;
    
    private void Start()
    {
        _playerUpgrade.OneStatUpgrade += OneStatUpgrade;
        _playerUpgrade.BigUpgrade += AllStatsUpgrade;
    }

    private void AllStatsUpgrade(float percent)
    {
        List<GameObject> toShow = new List<GameObject>();
        
        toShow.Add(_damageUpgrade);
        toShow.Add(_moveSpeedUpgrade);
        toShow.Add(_reloadSpeedUpgrade);

        int updatePercent = Mathf.CeilToInt(100 * (percent - 1));
        
        foreach (var updateView in toShow)
        {
            updateView.GetComponent<UpgradeView>().Setup(updatePercent, true);
        }

        StartCoroutine(ShowUpgrade(toShow));
    }

    private void OneStatUpgrade(PlayerUpgrade.UpgradeType obj, float percent)
    {
        int updatePercent = Mathf.CeilToInt(100 * (percent - 1));
        List<GameObject> toUpgrade = new List<GameObject>();
        
        switch (obj)
        {
            case PlayerUpgrade.UpgradeType.FireRate:
                _reloadSpeedUpgrade.GetComponent<UpgradeView>().Setup(updatePercent, false);
                toUpgrade.Add(_reloadSpeedUpgrade);
                break;
            case PlayerUpgrade.UpgradeType.Damage:
                _damageUpgrade.GetComponent<UpgradeView>().Setup(updatePercent, false);
                toUpgrade.Add(_damageUpgrade);
                break;
            case PlayerUpgrade.UpgradeType.MoveSpeed:
                _moveSpeedUpgrade.GetComponent<UpgradeView>().Setup(updatePercent, false);
                toUpgrade.Add(_moveSpeedUpgrade);
                break;
        }
        
        StartCoroutine(ShowUpgrade(toUpgrade));
    }

    private IEnumerator ShowUpgrade(List<GameObject> upgrades)
    {
        foreach (var element in upgrades)
        {
            element.SetActive(true);
        }

        yield return new WaitForSeconds(_upgradeShowTime);

        foreach (var element in upgrades)
        {
            element.SetActive(false);
        }
    }
}
