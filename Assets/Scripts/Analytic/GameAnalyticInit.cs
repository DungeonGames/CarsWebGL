using System;
using GameAnalyticsSDK;
using UnityEngine;

namespace Analytic
{
    public class GameAnalyticInit : MonoBehaviour
    {
        private void Awake()
        {
            GameAnalytics.Initialize();
        }
    }
}