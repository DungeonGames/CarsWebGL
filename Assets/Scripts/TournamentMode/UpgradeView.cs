using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Lean.Localization;


public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _upgradeText;

    [SerializeField] private GameObject _minorUpdateSymbol;
    [SerializeField] private GameObject _majorUpdateSymbol;

    [SerializeField] private UpgradeType _upgradeType;

    private const string _reloadSpeed = "ReloadUpgrade";
    private const string _damage = "DamageUpgrade";
    private const string _speed = "SpeedUpgrade";
    
    public void Setup(int upgradePercent, bool isMajor)
    {
        string upgradeName = String.Empty;

        switch (_upgradeType)
        {
            case UpgradeType.Speed:
                upgradeName = LeanLocalization.GetTranslationText(_speed);
                break;
            case UpgradeType.Damage:
                upgradeName = LeanLocalization.GetTranslationText(_damage);
                break;
            case UpgradeType.ReloadSpeed:
                upgradeName = LeanLocalization.GetTranslationText(_reloadSpeed);
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
