using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : MonoBehaviour
{
    public Image SoundButton;
    public Sprite TurnedOff;
    public Sprite TurnedOn;
    public Image ButtonCircleImage;
    public GameObject LeftSight;
    public GameObject RightSight;


    public void OnSoundButtonClicked(){
        // Change the sound button background image when pressed
        if(SoundButton.sprite == TurnedOn){
            // set the position of the ButtonCircleImage as LeftSight position
            ButtonCircleImage.transform.position = LeftSight.transform.position;
            SoundButton.sprite = TurnedOff;
        }else{
            // set the position of the ButtonCircleImage as RightSight position
            ButtonCircleImage.transform.position = RightSight.transform.position;
            SoundButton.sprite = TurnedOn;
        }

    }
}
