using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNG.Save;
using UnityEngine.UI;
using TMPro;
using SNG.Configs;

public class ShopController : MonoBehaviour
{
    public List<Button> ButtonList;
    // create list of textmeshpro text
    
    public List<TextMeshProUGUI> PriceTextList;

    // Start is called before the first frame update
    void Start()
    {
        UpdateButtonUi();
    }

    
    public void OnBuyButtonClick(int index)
    {
        AudioManager.instance.PlaySoundEffect("click");
        if (index == SaveGame.Instance.PlayerData.SelectedThemeIndex)
        {
            return;
        }
        if (SaveGame.Instance.PlayerData.TotalStar >= Configs.ThemeConfig.ThemeList[index].ThemePrice && !SaveGame.Instance.GeneralData.IsThemeUnlocked[index])
        {
            SaveGame.Instance.PlayerData.TotalStar -= Configs.ThemeConfig.ThemeList[index].ThemePrice;
            SaveGame.Instance.GeneralData.IsThemeUnlocked[index] = true;
            SaveGame.Instance.PlayerData.SelectedThemeIndex = index;
            UpdateButtonUi();
            
        }
        if(SaveGame.Instance.PlayerData.TotalStar < Configs.ThemeConfig.ThemeList[index].ThemePrice && !SaveGame.Instance.GeneralData.IsThemeUnlocked[index]){
            Debug.Log("Not enough star");
        }
        if (SaveGame.Instance.GeneralData.IsThemeUnlocked[index]){
            SaveGame.Instance.PlayerData.SelectedThemeIndex = index;
            UpdateButtonUi();
        }
    }

    private void UpdateButtonUi()
    {
        for (int i = 0; i < ButtonList.Count; i++)
        {
            if (i == SaveGame.Instance.PlayerData.SelectedThemeIndex)
            {
                ButtonList[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.75f);
                PriceTextList[i].text = "In Use";
                ButtonList[i].interactable = false;
            }
            else
            {
                ButtonList[i].interactable = true;
                //for bought themes
                if(SaveGame.Instance.GeneralData.IsThemeUnlocked[i]){
                    PriceTextList[i].text = "Select";
                    ButtonList[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.75f);
                }
                else{
                    ButtonList[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                    PriceTextList[i].text = Configs.ThemeConfig.ThemeList[i].ThemePrice.ToString();
                }
                
            }
        }
    }
    
    
}
