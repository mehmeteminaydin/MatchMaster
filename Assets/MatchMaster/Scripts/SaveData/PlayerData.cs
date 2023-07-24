namespace SNG.Save
{
    [System.Serializable]
    public class PlayerData
    {

        public long TotalStar;
        public long HintCounter;
        public long MagnetCounter;

        public PlayerData()
        {
            TotalStar = 0;
            HintCounter = 3;
            MagnetCounter = 3;
        }

    }
}