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
        AudioManager.instance.Play("click");
        if (index == SaveGame.Instance.PlayerData.SelectedThemeIndex)
        {
            return;
        }
        if (SaveGame.Instance.PlayerData.TotalStar >= Configs.ThemeConfig.ThemeList[index].ThemePrice && !Configs.ThemeConfig.ThemeList[index].IsThemeUnlocked)
        {
            SaveGame.Instance.PlayerData.TotalStar -= Configs.ThemeConfig.ThemeList[index].ThemePrice;
            Configs.ThemeConfig.ThemeList[index].IsThemeUnlocked = true;
            SaveGame.Instance.PlayerData.SelectedThemeIndex = index;
            UpdateButtonUi();
            
        }
        if(SaveGame.Instance.PlayerData.TotalStar < Configs.ThemeConfig.ThemeList[index].ThemePrice && !Configs.ThemeConfig.ThemeList[index].IsThemeUnlocked){
            Debug.Log("Not enough star");
        }
        if (Configs.ThemeConfig.ThemeList[index].IsThemeUnlocked){
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
                if(Configs.ThemeConfig.ThemeList[i].IsThemeUnlocked){
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
