using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using SNG.Save;
using TMPro;


public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI PlayerLevel;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI Experience;
    public GameObject SettingsPanel;
    public GameObject ShopPanel;
    public float FadeTime = 0.05f;
    public CanvasGroup ShopCanvasGroup;
    public RectTransform ShopRectTransform;
    public CanvasGroup SettingsCanvasGroup;
    public RectTransform SettingsRectTransform;
    public Image ShopPanelBackground;
    public Image SettingsPanelBackground;
    public Image LevelBarImage;

    public Button SettingsExitButton;
    public Button ShopExitButton;

    private Color _shopPanelColor;
    private Color _settingsPanelColor;

    // Start is called before the first frame update
    void Start()
    {

        int currentPlayerLevelPlusOne = SaveGame.Instance.PlayerData.PlayerLevel + 1;
        long exp = (20 * (currentPlayerLevelPlusOne * currentPlayerLevelPlusOne)) - (20 * currentPlayerLevelPlusOne);
        long currentExp = SaveGame.Instance.PlayerData.Experience;
        Experience.text = currentExp + " / " + exp.ToString();
        PlayerLevel.text = SaveGame.Instance.PlayerData.PlayerLevel.ToString();
        StartCoroutine(ExperienceBarAnimation((float)currentExp / (float)exp));
        
        LevelText.text = "Level:" + SaveGame.Instance.GeneralData.CurrentLevel.ToString();
        _shopPanelColor  = ShopPanelBackground.color;
        _settingsPanelColor = SettingsPanelBackground.color;
    }

    IEnumerator ExperienceBarAnimation(float ratio)
    {
        if(ratio == 0){
            LevelBarImage.enabled = false;
            yield break;
        }
        else{
            LevelBarImage.enabled = true;
            float duration = 0.5f;
            float time = 0f;
            while(time < duration){
                time += Time.deltaTime;
                float newRatio =  (ratio * (time / duration));
                LevelBarImage.fillAmount = newRatio;
                yield return null;
            }
            LevelBarImage.fillAmount = ratio;
        }
        
        
    }

    public void OnPlayButtonClicked(){
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.PlaySoundEffect("click");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }


    public void ShopPanelFadeIn()
    {
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.PlaySoundEffect("click");
        }        
        ShopExitButton.interactable = false;
        ShopPanelBackground.color = new Color(_shopPanelColor.r, _shopPanelColor.g, _shopPanelColor.b, 0f);
        ShopCanvasGroup.alpha = 0f;
        ShopPanel.SetActive(true);
        ShopRectTransform.transform.localPosition = new Vector3(0f, -2000f, 0f);
        ShopRectTransform.DOAnchorPos(new Vector2(0f, 0f), FadeTime, false).SetEase(Ease.InOutSine);
        ShopCanvasGroup.DOFade(1, FadeTime);
        StartCoroutine(ChangeShopPanelBackground());        
    }

    IEnumerator ChangeShopPanelBackground()
    {
        yield return new WaitForSeconds(1f);
        ShopPanelBackground.DOColor(new Color(_shopPanelColor.r, _shopPanelColor.g, _shopPanelColor.b, 0.8f), 0.2f);
        // wait until the animation is finished
        yield return new WaitForSeconds(0.2f);
        ShopExitButton.interactable = true;
    }

    public void ShopPanelFadeOut()
    {
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.PlaySoundEffect("click");
        }
        ShopPanelBackground.color = new Color(_shopPanelColor.r, _shopPanelColor.g, _shopPanelColor.b, 0f);

        StartCoroutine(AnimateShopPanelFadeOut());

    }

    IEnumerator AnimateShopPanelFadeOut()
    {
        ShopCanvasGroup.alpha = 1f;
        ShopRectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        ShopRectTransform.DOAnchorPos(new Vector2(0f, -2000f), FadeTime, false).SetEase(Ease.InOutSine);
        ShopCanvasGroup.DOFade(1, FadeTime);
        // wait for the animation to finish
        yield return new WaitForSeconds(0.8f);
        ShopPanel.SetActive(false);
    }


    public void SettingsPanelFadeIn()
    {
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.PlaySoundEffect("click");
        }
        SettingsExitButton.interactable = false;
        SettingsPanelBackground.color = new Color(_settingsPanelColor.r, _settingsPanelColor.g, _settingsPanelColor.b, 0f);
        SettingsCanvasGroup.alpha = 0f;
        SettingsPanel.SetActive(true);
        SettingsCanvasGroup.alpha = 0f;
        SettingsRectTransform.transform.localPosition = new Vector3(0f, -2000f, 0f);
        SettingsRectTransform.DOAnchorPos(new Vector2(0f, 0f), FadeTime, false).SetEase(Ease.InOutSine);
        SettingsCanvasGroup.DOFade(1, FadeTime);
        StartCoroutine(ChangeSettingsPanelBackground());
    }

    IEnumerator ChangeSettingsPanelBackground()
    {
        yield return new WaitForSeconds(1f);
        SettingsPanelBackground.DOColor(new Color(_settingsPanelColor.r, _settingsPanelColor.g, _settingsPanelColor.b, 0.8f), 0.2f);
        // wait until the animation is finished
        yield return new WaitForSeconds(0.2f);
        SettingsExitButton.interactable = true;
    }


    public void SettingsPanelFadeOut()
    {
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.PlaySoundEffect("click");
        }
        SettingsPanelBackground.color = new Color(_settingsPanelColor.r, _settingsPanelColor.g, _settingsPanelColor.b, 0f);
        StartCoroutine(AnimateSettingsPanelFadeOut());

    }

    IEnumerator AnimateSettingsPanelFadeOut()
    {
        SettingsCanvasGroup.alpha = 1f;
        SettingsRectTransform.transform.localPosition = new Vector3(0f, 0f, 0f);
        SettingsRectTransform.DOAnchorPos(new Vector2(0f, -2000f), FadeTime, false).SetEase(Ease.InOutSine);
        SettingsCanvasGroup.DOFade(1, FadeTime);
        // wait for the animation to finish
        yield return new WaitForSeconds(0.8f);
        SettingsPanel.SetActive(false);
    }
}
