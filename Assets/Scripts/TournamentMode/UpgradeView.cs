using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _upgradeText;

    [SerializeField] private GameObject _minorUpdateSymbol;
    [SerializeField] private GameObject _majorUpdateSymbol;

    [SerializeField] private UpgradeType _upgradeType;

    public void Setup(int upgradePercent, bool isMajor)
    {
        string upgradeName = String.Empty;

        switch (_upgradeType)
        {
            case UpgradeType.Speed:
                upgradeName = "Скорость:";
                break;
            case UpgradeType.Damage:
                upgradeName = "Урон:";
                break;
            case UpgradeType.ReloadSpeed:
                upgradeName = "Перезарядка:";
                break;
        }

        if (isMajor)
        {
            _majorUpdateSymbol.SetActive(true);
            _minorUpdateSymbol.SetActive(false);
        }
        else
        {
            _majorUpdateSymbol.SetActive(false);
            _minorUpdateSymbol.SetActive(true);
        }
        
        _upgradeText.text = upgradeName + "\n" + $"{upgradePercent}%";
    }
    
    public enum UpgradeType
    {
        Speed,
        Damage,
        ReloadSpeed
    }
}
