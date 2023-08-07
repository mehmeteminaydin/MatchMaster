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
        public Material ThemeMaterial;
        public int ThemePrice;

        public Theme()
        {
            ThemeMaterial = null;
            ThemePrice = 0;
        }
    }
}

