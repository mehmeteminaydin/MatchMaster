using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNG.Save;
using SNG.Configs;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public PlayerLevelController PlayerLevelController;
    public GameObject GameEndScreen;
    public Image GameEndWin;
    public Image GameEndLose;
    public ObjectController ObjectController;
    public Dragging Dragging;
    public Image TimeBarImage;
    public Image TimeBarBGImage;
    public GameObject TimeBarPosition;
    public List<Image> StarImages;
    public List<Image> StarBGImages;
    public List<Image> GameEndStarImages;
    public TMPro.TextMeshProUGUI TotalStarCount;
    public TMPro.TextMeshProUGUI TimerText;

    public float TotalTime; // Total time in seconds
    
    private bool _isStarAnimationPlaying = false;
    private bool _isGameOver = false;
    private bool _isTimeOver = false;
    private long _starCount = 3;
    private float _currentTime;
    private System.Random _random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        // using TimeBarPosition GameObject, get the left edge of it
        float leftEdgeX = 0 - TimeBarPosition.GetComponent<RectTransform>().rect.width / 2;
        float rightEdgeX = 0 + TimeBarPosition.GetComponent<RectTransform>().rect.width / 2;
        float distance = TimeBarPosition.GetComponent<RectTransform>().rect.width;
        float offset = 0f;
        float star1X = leftEdgeX + (distance * Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].StarEarningRateList[0]) + offset;
        float star2X = leftEdgeX + (distance * Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].StarEarningRateList[1]) + offset;
        float star3X = leftEdgeX + (distance * Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].StarEarningRateList[2]) + offset;

        // change the local position of the stars
        StarImages[0].GetComponent<RectTransform>().localPosition = new Vector3(star1X, StarImages[0].GetComponent<RectTransform>().localPosition.y, StarImages[0].GetComponent<RectTransform>().localPosition.z);
        StarImages[1].GetComponent<RectTransform>().localPosition = new Vector3(star2X, StarImages[1].GetComponent<RectTransform>().localPosition.y, StarImages[1].GetComponent<RectTransform>().localPosition.z);
        StarImages[2].GetComponent<RectTransform>().localPosition = new Vector3(star3X, StarImages[2].GetComponent<RectTransform>().localPosition.y, StarImages[2].GetComponent<RectTransform>().localPosition.z);
        StarBGImages[0].GetComponent<RectTransform>().localPosition = new Vector3(star1X, StarBGImages[0].GetComponent<RectTransform>().localPosition.y, StarBGImages[0].GetComponent<RectTransform>().localPosition.z);
        StarBGImages[1].GetComponent<RectTransform>().localPosition = new Vector3(star2X, StarBGImages[1].GetComponent<RectTransform>().localPosition.y, StarBGImages[1].GetComponent<RectTransform>().localPosition.z);
        StarBGImages[2].GetComponent<RectTransform>().localPosition = new Vector3(star3X, StarBGImages[2].GetComponent<RectTransform>().localPosition.y, StarBGImages[2].GetComponent<RectTransform>().localPosition.z);
        

    

        TotalTime = Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].LevelTimeInSeconds;
        TotalStarCount.text = SaveGame.Instance.PlayerData.TotalStar.ToString();
        _currentTime = TotalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isTimeOver || _isGameOver){
            return;
        }
        _currentTime -= Time.deltaTime;

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);

        //update the UI text with the current time
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        if(_currentTime/TotalTime >= Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].StarEarningRateList[2]){
            _starCount = 3;
        }
        else if(_currentTime/TotalTime >= Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].StarEarningRateList[1] && _currentTime/TotalTime < Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].StarEarningRateList[2]){
            _starCount = 2;
        }
        else if(_currentTime/TotalTime >= Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].StarEarningRateList[0] && _currentTime/TotalTime < Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].StarEarningRateList[1]){
            _starCount = 1;
        }
        else{
            _starCount = 0;
        }
        if(_currentTime <= 10){
            TimerText.color = Color.red;
        }
        if (_currentTime <= 0)
        {
            _currentTime = 0;
            TimerText.text = string.Format("{0:00}:{1:00}", 0, 0);
            _isTimeOver = true;
            TimeBarImage.enabled = false; // Hide the green bar
            GameOverLost();
            return;
        }
        else
        {
            float remainingRatio = _currentTime / TotalTime;
            TimeBarImage.fillAmount = remainingRatio; // Set the fill amount of the green bar
            TimeBarImage.enabled = true; // Make the green bar visible
        }

        for (int i = 0; i < StarImages.Count; i++)
        {
            StarImages[i].enabled = (i < _starCount); // Enable the star image if its index is less than _starCount
        }
    }

    public void OnHintButton()
    {
        if(_isTimeOver || _isGameOver){
            return;
        }
        ObjectController.OnHintButtonPress();
    }

    public void OnMagnetButton()
    {
        if(_isTimeOver || _isGameOver){
            return;
        }
        ObjectController.OnMagnetButtonPress();
    }

    private void SaveStar()
    {
        SaveGame.Instance.PlayerData.TotalStar += _starCount;
        TotalStarCount.text = SaveGame.Instance.PlayerData.TotalStar.ToString();
    }

    public void GameOverWon()
    {
        PlayerLevelController.LevelUpdate(true);

        if(SaveGame.Instance.GeneralData.CurrentLevel == 4){
            SaveGame.Instance.GeneralData.CurrentLevel = 4;
            Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].LastLevelConfig();
        }
        else{
            SaveGame.Instance.GeneralData.CurrentLevel++;
            if(SaveGame.Instance.GeneralData.CurrentLevel == 4){
                Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].LastLevelConfig();
            }
        }
        
        _isGameOver = true;
        SaveStar();
        GameEndScreen.SetActive(true);
        // I want to enable the star images according to the _starCount by using animation
        // in each loop, I will play the audio
        _isStarAnimationPlaying = true;
        StartCoroutine(PlayStarAnimation());
        
        GameEndWin.enabled = true;
        GameEndLose.enabled = false;
    }


    IEnumerator PlayStarAnimation()
    {
        for (int i = 0; i < _starCount; i++)
        {
            if(i == 0){
                GameEndStarImages[2].enabled = false;
                GameEndStarImages[1].enabled = false;
                GameEndStarImages[0].enabled = true;
            }
            else if (i == 1){
                GameEndStarImages[2].enabled = false;
                GameEndStarImages[1].enabled = true;
            }
            else{
                GameEndStarImages[2].enabled = true;
            }
            if(SaveGame.Instance.GeneralData.IsSoundEffectsOn)
            {    
                AudioManager.instance.PlaySoundEffect("star_collect");
            }
            yield return new WaitForSeconds(0.43f);
        }
        // if there is no star
        if(_starCount == 0){
            GameEndStarImages[2].enabled = false;
            GameEndStarImages[1].enabled = false;
            GameEndStarImages[0].enabled = false;
        }
        _isStarAnimationPlaying = false;
    }

    public void GameOverLost()
    {
        PlayerLevelController.LevelUpdate(false);

        if(SaveGame.Instance.GeneralData.CurrentLevel == 4){
            Configs.LevelConfig.LevelList[SaveGame.Instance.GeneralData.CurrentLevel - 1].LastLevelConfig();
        }

        _isGameOver = true;

        Dragging.GameOver();
        
        GameEndScreen.SetActive(true);
        for (int i = 0; i < GameEndStarImages.Count; i++)
        {
            GameEndStarImages[i].enabled = (i < _starCount); // Enable the star image if its index is less than _starCount
        }
        GameEndWin.enabled = false;
        GameEndLose.enabled = true;
    }

    public void OnContinueButtonClicked(){
        if(_isStarAnimationPlaying){
            return;
        }
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.PlaySoundEffect("click");
        }
        // Hide the game end screen
        GameEndScreen.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void OnExitButtonClicked(){
        if(_isStarAnimationPlaying){
            return;
        }
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.PlaySoundEffect("click");
        }
        // Hide the game end screen
        GameEndScreen.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
