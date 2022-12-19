using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsInitializer : MonoBehaviour
{
    private void Awake()
    {
        GameAnalytics.Initialize();
    }
}
