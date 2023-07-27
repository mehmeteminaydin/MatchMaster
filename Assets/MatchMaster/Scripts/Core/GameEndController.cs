using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNG.Save;


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
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.Play("click");
        }
        // Hide the game end screen
        this.gameObject.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    public void OnExitButtonClicked(){
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.Play("click");
        }
        // Hide the game end screen
        this.gameObject.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
