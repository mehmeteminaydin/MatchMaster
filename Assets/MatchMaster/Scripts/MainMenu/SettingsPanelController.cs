using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SNG.Save;


public class SettingsPanelController : MonoBehaviour
{
    public Image SoundButton;
    public Image MusicButton;
    public Sprite TurnedOff;
    public Sprite TurnedOn;
    public Image SoundButtonCircleImage;
    public Image MusicButtonCircleImage;
    public GameObject SoundLeftSight;
    public GameObject SoundRightSight;
    public GameObject MusicLeftSight;
    public GameObject MusicRightSight;

    
    void Start(){
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            SoundButton.sprite = TurnedOn;
            SoundButtonCircleImage.transform.position = SoundRightSight.transform.position;
        }else{
            SoundButton.sprite = TurnedOff;
            SoundButtonCircleImage.transform.position = SoundLeftSight.transform.position;
        }

        if(SaveGame.Instance.GeneralData.IsMusicOn){
            MusicButton.sprite = TurnedOn;
            MusicButtonCircleImage.transform.position = MusicRightSight.transform.position;
        }else{
            MusicButton.sprite = TurnedOff;
            MusicButtonCircleImage.transform.position = MusicLeftSight.transform.position;
        }
    }


    public void OnMusicButtonClicked(){
    
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.Play("click");
        }
        // Change the sound button background image when pressed
        if(MusicButton.sprite == TurnedOn){
            SaveGame.Instance.GeneralData.IsMusicOn = false;
            AudioManager.instance.Stop("bg_music");
            // set the position of the ButtonCircleImage as LeftSight position
            MusicButtonCircleImage.transform.position = MusicLeftSight.transform.position;
            MusicButton.sprite = TurnedOff;
        }else{
            SaveGame.Instance.GeneralData.IsMusicOn = true;
            AudioManager.instance.Play("bg_music");
            // set the position of the ButtonCircleImage as RightSight position
            MusicButtonCircleImage.transform.position = MusicRightSight.transform.position;
            MusicButton.sprite = TurnedOn;
        }

    }

    public void OnSoundButtonClicked(){
        
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn){
            AudioManager.instance.Play("click");
        }
        // Change the sound button background image when pressed
        if(SoundButton.sprite == TurnedOn){
            SaveGame.Instance.GeneralData.IsSoundEffectsOn = false;
            // set the position of the ButtonCircleImage as LeftSight position
            SoundButtonCircleImage.transform.position = SoundLeftSight.transform.position;
            SoundButton.sprite = TurnedOff;
        }else{
            SaveGame.Instance.GeneralData.IsSoundEffectsOn = true;
            // set the position of the ButtonCircleImage as RightSight position
            SoundButtonCircleImage.transform.position = SoundRightSight.transform.position;
            SoundButton.sprite = TurnedOn;
        }
    }

    
}
