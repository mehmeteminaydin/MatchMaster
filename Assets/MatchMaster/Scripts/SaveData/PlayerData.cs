using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SNG.Save
{
    

    [System.Serializable]
    public class PlayerData
    {
        public long TotalStar;
        public long HintCounter;
        public long MagnetCounter;
        public int SelectedThemeIndex;
        public long Experience;
        public int PlayerLevel;

        public PlayerData()
        {
            Experience = 0;
            PlayerLevel = 1;
            SelectedThemeIndex = 0;
            TotalStar = 100;
            HintCounter = 3;
            MagnetCounter = 3;
        }

    }
}