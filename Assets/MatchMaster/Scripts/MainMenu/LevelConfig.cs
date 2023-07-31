using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "SNG/ConfigurationObjects/LevelConfig")]

    public class LevelConfig : ScriptableObject
    {
        public Level[] LevelList;
    }

    [System.Serializable]
    public class Level
    {   
        // apple x 4 , pear x 4, banana x 4
        public int LevelNumber;
        public int TotalObjectType;  // TotalObjectType = 3
        public int EachObjectCount;  // EachObjectCount = 4
        public int TotalObjectCount; // TotalObjectCount = 12,
        public float LevelTimeInSeconds;
        public List<float> StarEarningRateList; // 0-1;

        private System.Random _random = new System.Random();


        public void LastLevelConfig()
        {
            TotalObjectType = _random.Next(TotalObjectType-2, TotalObjectType+2);
            TotalObjectCount = TotalObjectType * EachObjectCount;
            LevelTimeInSeconds = _random.Next((int)(LevelTimeInSeconds-20), (int)(LevelTimeInSeconds+20));
        }
        
    }

}
