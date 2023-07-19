using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public ObjectController ObjectController;

    public TMPro.TextMeshProUGUI TimerText;

    public float TotalTime = 60f; // Total time in seconds (1 minute)

    private float _currentTime;
    private System.Random _random = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = TotalTime;
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime -= Time.deltaTime;

        // Calculate minutes and seconds
        int minutes = Mathf.FloorToInt(_currentTime / 60f);
        int seconds = Mathf.FloorToInt(_currentTime % 60f);

        //update the UI text with the current time
        TimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (_currentTime <= 0)
        {
            Debug.Log("Game Over");
            //ObjectController.GameOver();
        }
    }

    public void OnHintButton(){
        ObjectController.OnHintButtonPress();
    }

    public void OnMagnetButton(){
        ObjectController.OnMagnetButtonPress();
    }
}
