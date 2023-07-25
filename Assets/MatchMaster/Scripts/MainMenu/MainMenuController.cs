using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject ShopPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayButtonClicked(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void OnSettingsButtonClicked(){
        // enable the settings panel
        SettingsPanel.SetActive(true);
    }

    public void OnSettingsPanelExit(){
        // disable the settings panel
        SettingsPanel.SetActive(false);
    }

    public void OnShopButtonClicked(){
        // enable the shop panel
        ShopPanel.SetActive(true);
    }

    public void OnShopPanelExit(){
        // disable the shop panel
        ShopPanel.SetActive(false);
    }
}
