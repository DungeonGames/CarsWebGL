namespace SaveData
{
    [System.Serializable]
    public class PlayerData
    {
        public int CurrentCarIndex;
        public int CurrentGunIndex;
        public bool IsFirstRun = true;
        public bool IsKeyboardInput = false;
        public bool[] IsUnlockableItemFlags = new bool[2];
        public int CurrentUnlockableQuanity = 0;
        public int CurrentWave = 0;
        public int Coins;
        public int Gems;
        public int SedanLevel;
        public int SubaruLevel;
        public int MinigunLevel;
        public int RocketLauncherLevel;
        public bool SubaruIsBuyed = false;
        public bool RocketLauncherIsBuyed = false;
    }
}