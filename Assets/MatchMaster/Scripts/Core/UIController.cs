using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNG.Save;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public GameObject GameEndScreen;
    public Image GameEndWin;
    public Image GameEndLose;
    public ObjectController ObjectController;
    public Dragging Dragging;
    public Image TimeBarImage;
    public Image TimeBarBGImage;
    public List<Image> StarImages;
    public List<Image> StarBGImages;
    public TMPro.TextMeshProUGUI TotalStarCount;
    public TMPro.TextMeshProUGUI TimerText;

    public float TotalTime = 60f; // Total time in seconds (1 minute)

    private bool _isGameOver = false;
    private bool _isTimeOver = false;
    private long _starCount = 3;
    private float _currentTime;
    private System.Random _random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        TotalStarCount.text = SaveGame.Instance.PlayerData.Star.ToString();
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
        if(_currentTime/TotalTime >= 0.5f){
            _starCount = 3;
        }
        else if(_currentTime/TotalTime >= 0.3f && _currentTime/TotalTime < 0.5f){
            _starCount = 2;
        }
        else if(_currentTime/TotalTime >= 0.1f && _currentTime/TotalTime < 0.3f){
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
        SaveGame.Instance.PlayerData.Star += _starCount;
        TotalStarCount.text = SaveGame.Instance.PlayerData.Star.ToString();
    }

    public void GameOverWon()
    {
        _isGameOver = true;
        SaveStar();
        Debug.Log(" _starCount: " + _starCount + "You won!"+ "Total Star Count: " + SaveGame.Instance.PlayerData.Star);

        GameEndScreen.SetActive(true);
        GameEndWin.enabled = true;
        GameEndLose.enabled = false;
    }

    public void GameOverLost()
    {
        _isGameOver = true;
        Debug.Log(" _starCount: " + _starCount + "You lost!"+ "Total Star Count: " + SaveGame.Instance.PlayerData.Star);
        Dragging.GameOver();
        
        GameEndScreen.SetActive(true);
        GameEndWin.enabled = false;
        GameEndLose.enabled = true;
    }
}
