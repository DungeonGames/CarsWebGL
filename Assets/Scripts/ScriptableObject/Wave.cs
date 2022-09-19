using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave/Create new Wave", order = 51)]
public class Wave : ScriptableObject
{
    [SerializeField] private Enemy _greenEnemyTemplate;
    [SerializeField] private int _greenCount;
    [SerializeField] private Enemy _blueEnemyTemplate;
    [SerializeField] private int _blueCount;
    [SerializeField] private Enemy _orangeEnemyTemplate;
    [SerializeField] private int _orangeCount;

    public Enemy GreenEnemyTemplate => _greenEnemyTemplate;
    public int GreenCount => _greenCount;
    public Enemy BlueEnemyTemplate => _blueEnemyTemplate;
    public int BlueCount => _blueCount;
    public Enemy OrangeEnemyTemplate => _orangeEnemyTemplate;
    public int OrangeCount => _orangeCount;
}
