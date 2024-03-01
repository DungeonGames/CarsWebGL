using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathEffects : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private List<GameObject> _toDisable;

    [SerializeField] private Car _playerHealth;

    private const string _explosionSound = "PlayerDeathExplosion";

    private void Start()
    {
        _playerHealth.Died += ShowDeathEffect;
    }

    private void ShowDeathEffect()
    {
        foreach (var toDisable in _toDisable)
        {
            toDisable.SetActive(false);
        }
        
        _explosion.SetActive(true);
        AudioResources.Instance.PlaySound(_explosionSound);
    }
}
