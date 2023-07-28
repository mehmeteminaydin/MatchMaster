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
        
    }

}
