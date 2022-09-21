namespace SaveData
{
    [System.Serializable]
    public class PlayerData
    {
        public bool IsFirstRun = true;
        public bool IsKeyboardInput = false;
        public int CurrentWave;
        public int Coins;
        public int Gems;
        public int QuanityUpgradeMinigun;
        public int QuanityUpgradeCar;
    }
}