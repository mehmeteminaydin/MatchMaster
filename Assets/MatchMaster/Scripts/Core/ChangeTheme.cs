using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNG.Save;
using UnityEngine.UI;
using SNG.Configs;


public class ChangeTheme : MonoBehaviour
{
    public Image ThemeImageToUpdate;
    // Start is called before the first frame update
    void Start()
    {
        ThemeImageToUpdate.sprite = Configs.ThemeConfig.ThemeList[SaveGame.Instance.PlayerData.SelectedThemeIndex].ThemeImage;
    }

}
