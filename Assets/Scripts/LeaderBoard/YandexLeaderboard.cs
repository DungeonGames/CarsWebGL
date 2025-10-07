using UnityEngine;

public class YandexLeaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardView _leaderboardView;

    private const string _leaderboardName = "BestOfTheBest";

    private void Awake()
    {
        FormListOfTopPlayers();
    }

    private void FormListOfTopPlayers(bool test = false)
    {
#if YANDEX_GAMES
        List<PlayerInfoLeaderboard> top5Players = new List<PlayerInfoLeaderboard>();

        if (test)
        {
            for (int i = 0; i < 5; i++)
            {
                top5Players.Add(new PlayerInfoLeaderboard("name", i));
            }

            _leaderboardView.ConstructLeaderboard(top5Players);

            return;
        }

        if (PlayerAccount.IsAuthorized)
        {
            Leaderboard.GetEntries(_leaderboardName, (result) =>
            {
                Debug.Log($"My rank = {result.userRank}");

                int resultsAmount = result.entries.Length;

                resultsAmount = Mathf.Clamp(resultsAmount, 1, 5);

                for (int i = 0; i < resultsAmount; i++)
                {
                    string name = result.entries[i].player.publicName;

                    if (string.IsNullOrEmpty(name))
                        name = "Anonymos";

                    int score = result.entries[i].score;

                    top5Players.Add(new PlayerInfoLeaderboard(name, score));
                }

                _leaderboardView.ConstructLeaderboard(top5Players);
            });
        }
#endif
    }

    public void Authorized()
    {
#if YANDEX_GAMES
        PlayerAccount.Authorize();

        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
        }

        FormListOfTopPlayers();
#endif
    }

    public void AddPlayerToLeaderboard(int score)
    {
#if YANDEX_GAMES

        if (!PlayerAccount.IsAuthorized)
            return;
        Leaderboard.GetPlayerEntry(_leaderboardName, (result) =>
        {
            Leaderboard.SetScore(_leaderboardName, score);
        });
#endif
    }
}

public class PlayerInfoLeaderboard
{
    public string Name { get; private set; }
    public int Score { get; private set; }

    public PlayerInfoLeaderboard(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
