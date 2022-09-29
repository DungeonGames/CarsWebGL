using System.Collections.Generic;
using UnityEngine;

public class LeaderboardView : MonoBehaviour
{
    [SerializeField] private Transform _parentObject;
    [SerializeField] private GameObject _leaderboardElementPrefab;

    private List<GameObject> _spawnedElements = new List<GameObject>();

    public void ConstructLeaderboard(List<PlayerInfoLeaderboard> playersInfo)
    {
        ClearLeaderboard();

        foreach(PlayerInfoLeaderboard info in playersInfo)
        {
            GameObject leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _parentObject);

            LeaderboardElement leaderboardElement = leaderboardElementInstance.GetComponent<LeaderboardElement>();
            leaderboardElement.Initialize(info.Name, info.Score, false);

            _spawnedElements.Add(leaderboardElementInstance);
        }
    }

    private void ClearLeaderboard()
    {
        foreach (var element in _spawnedElements)
            Destroy(element);

        _spawnedElements = new List<GameObject>();
    }
}
