using UnityEngine;
using UnityEngine.UI;
using SNG.Configs;
using System.Collections.Generic;



namespace SNG.Save
{
    [System.Serializable]
    public class GeneralData
    {
        public int CurrentLevel;
        public bool IsSoundEffectsOn;
        public bool IsMusicOn;
        public bool[] IsThemeUnlocked;
        public int NumberOfThemes;
        public bool IsGameOver;
        


        public GeneralData(){
            IsGameOver = false;
            CurrentLevel = 1;
            IsSoundEffectsOn = true;
            IsMusicOn = true;
            NumberOfThemes = 4;
            IsThemeUnlocked = new bool[NumberOfThemes];
            for(int i = 0; i < IsThemeUnlocked.Length; i++){
                if(i == 0){
                    IsThemeUnlocked[i] = true;
                }
                else{
                    IsThemeUnlocked[i] = false;
                }
            }
            
        }
    }

    // keep the Game State
    [System.Serializable]
    public class GameState
    {
        public bool ShouldBeLoaded;
        public bool LoadGameScene;
        public float RemainingTime;
        public int RemainingHint;
        public int RemainingMagnet;
        public List<int> ObjectIDList;
        public List<int> ObjectIndexList;
        


        public GameState(){
            ShouldBeLoaded = false;
            LoadGameScene = false;
            RemainingTime = 0;
            RemainingHint = 0;
            RemainingMagnet = 0;    
            ObjectIDList = new List<int>();
        }
    }

}
