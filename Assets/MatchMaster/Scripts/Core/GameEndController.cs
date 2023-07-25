using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnContinueButtonClicked(){
        // Hide the game end screen
        this.gameObject.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void OnExitButtonClicked(){
        // Hide the game end screen
        this.gameObject.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
