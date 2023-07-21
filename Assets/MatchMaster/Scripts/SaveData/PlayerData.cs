namespace SNG.Save
{
    [System.Serializable]
    public class PlayerData
    {

        public long Star;
        public long HintCounter;
        public long MagnetCounter;

        public PlayerData()
        {
            Star = 0;
            HintCounter = 3;
            MagnetCounter = 3;
        }

    }
}