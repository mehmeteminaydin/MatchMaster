using UnityEngine;
using UnityEngine.UI;


namespace Configuration{

    [CreateAssetMenu(fileName = "ThemeConfig", menuName = "SNG/ConfigurationObjects/ThemeConfig")]
    public class ThemeConfig : ScriptableObject
    {
        public Theme[] ThemeList;
    }


    [System.Serializable]
    public class Theme
    {
        public Sprite ThemeImage;
        public int ThemePrice;
        public bool IsThemeUnlocked;

        public Theme()
        {
            ThemeImage = null;
            ThemePrice = 0;
            IsThemeUnlocked = false;
        }
    }
}

