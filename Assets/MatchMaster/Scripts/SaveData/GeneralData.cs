using UnityEngine;
using UnityEngine.UI;
using SNG.Configs;


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

        public GeneralData(){
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

}
