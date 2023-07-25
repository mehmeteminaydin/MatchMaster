using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject ShopPanel;
    public float FadeTime = 0.05f;
    public CanvasGroup ShopCanvasGroup;
    public RectTransform ShopRectTransform;
    public CanvasGroup SettingsCanvasGroup;
    public RectTransform SettingsRectTransform;
    public Image ShopPanelBackground;
    public Image SettingsPanelBackground;

    private Color _shopPanelColor;
    private Color _settingsPanelColor;

    // Start is called before the first frame update
    void Start()
    {
        _shopPanelColor  = ShopPanelBackground.color;
        _settingsPanelColor = SettingsPanelBackground.color;
    }


    public void OnPlayButtonClicked(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }


    public void ShopPanelFadeIn()
    {
        
        ShopPanelBackground.color = new Color(_shopPanelColor.r, _shopPanelColor.g, _shopPanelColor.b, 0f);
        ShopCanvasGroup.alpha = 0f;
        ShopPanel.SetActive(true);
        ShopRectTransform.transform.localPosition = new Vector3(0f, -2000f, 0f);
        ShopRectTransform.DOAnchorPos(new Vector2(0f, 0f), FadeTime, false).SetEase(Ease.InOutSine);
        ShopCanvasGroup.DOFade(1, FadeTime);
        ShopPanelBackground.DOColor(new Color(_shopPanelColor.r, _shopPanelColor.g, _shopPanelColor.b, 0.8f), 1.5f);
        
    }

    public void ShopPanelFadeOut()
    {
        ShopPanelBackground.DOColor(new Color(_shopPanelColor.r, _shopPanelColor.g, _shopPanelColor.b, 0f), 1f);
        ShopCanvasGroup.alpha = 1f;
        ShopRectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        ShopRectTransform.DOAnchorPos(new Vector2(0f, -2000), FadeTime, false).SetEase(Ease.InOutSine);
        ShopCanvasGroup.DOFade(1, FadeTime);
        // wait for the animation to finish
        StartCoroutine(DisableShopPanel());

    }

    IEnumerator DisableShopPanel()
    {
        yield return new WaitForSeconds(0.8f);
        ShopPanel.SetActive(false);
    }

    public void SettingsPanelFadeIn()
    {
        SettingsPanelBackground.color = new Color(_settingsPanelColor.r, _settingsPanelColor.g, _settingsPanelColor.b, 0f);
        SettingsCanvasGroup.alpha = 0f;
        SettingsPanel.SetActive(true);
        SettingsCanvasGroup.alpha = 0f;
        SettingsRectTransform.transform.localPosition = new Vector3(0f, -2000f, 0f);
        SettingsRectTransform.DOAnchorPos(new Vector2(0f, 0f), FadeTime, false).SetEase(Ease.InOutSine);
        SettingsCanvasGroup.DOFade(1, FadeTime);
        SettingsPanelBackground.DOColor(new Color(_settingsPanelColor.r, _settingsPanelColor.g, _settingsPanelColor.b, 0.8f), 1.5f);
    }


    public void SettingsPanelFadeOut()
    {
        SettingsPanelBackground.DOColor(new Color(_settingsPanelColor.r, _settingsPanelColor.g, _settingsPanelColor.b, 0f), 1f);
        SettingsCanvasGroup.alpha = 1f;
        SettingsRectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        SettingsRectTransform.DOAnchorPos(new Vector2(0f, -2000f), FadeTime, false).SetEase(Ease.InOutSine);
        SettingsCanvasGroup.DOFade(1, FadeTime);
        // wait for the animation to finish
        StartCoroutine(DisableSettingsPanel());

    }

    IEnumerator DisableSettingsPanel()
    {
        yield return new WaitForSeconds(0.8f);
        SettingsPanel.SetActive(false);
    }
}
