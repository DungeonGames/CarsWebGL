using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentMapExpand : MonoBehaviour
{
    [SerializeField] private List<GameObject> _zoneWalls;
    [SerializeField] private WavesManager _wavesManager;

    private int _wallToBreak;
    
    private void Start()
    {
        _wavesManager.MajorWaveEnded += BreakWall;
    }

    private void BreakWall()
    {
        _zoneWalls[_wallToBreak].SetActive(false);
        _wallToBreak++;
    }
}