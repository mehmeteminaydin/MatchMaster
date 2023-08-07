using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNG.Save;
using UnityEngine.UI;
using SNG.Configs;


public class ChangeTheme : MonoBehaviour
{
    public Renderer[] PlaneRendererList;
    // Start is called before the first frame update
    void Start()
    {
        // change all the planes' material to the selected theme material
        for (int i = 0; i < PlaneRendererList.Length; i++)
        {
            PlaneRendererList[i].material = Configs.ThemeConfig.ThemeList[SaveGame.Instance.PlayerData.SelectedThemeIndex].ThemeMaterial;
        }
        //ThemeImageToUpdate.sprite = Configs.ThemeConfig.ThemeList[SaveGame.Instance.PlayerData.SelectedThemeIndex].ThemeImage;
    }

}
