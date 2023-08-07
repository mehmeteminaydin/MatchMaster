using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SNG.Save;
using UnityEngine.UI;



public class PlayerLevelController : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Experience;
    public Image ExperienceBar;
    public TMPro.TextMeshProUGUI LevelText;

    public void LevelUpdate(bool isWin){
        if(isWin){
            SaveGame.Instance.PlayerData.Experience += 60;
        }
        else{
            SaveGame.Instance.PlayerData.Experience += 20;
        }
        

        int currentPlayerLevelPlusOne = SaveGame.Instance.PlayerData.PlayerLevel + 1;

        long exp = (20 * (currentPlayerLevelPlusOne * currentPlayerLevelPlusOne)) - (20 * currentPlayerLevelPlusOne);

        //Player Level Up
        if(SaveGame.Instance.PlayerData.Experience >= exp){
            float previousRatio;
            if(isWin){
                if(SaveGame.Instance.PlayerData.Experience - 60 < 0){
                    previousRatio = 0;
                }
                else{
                    previousRatio = (float)(SaveGame.Instance.PlayerData.Experience - 60) / (float)exp;
                }
            }
            else{
                if(SaveGame.Instance.PlayerData.Experience - 20 < 0){
                    previousRatio = 0;
                }
                else{
                    previousRatio = (float)(SaveGame.Instance.PlayerData.Experience - 20) / (float)exp;
                }
            }
            
            StartCoroutine(ExperienceBarAnimation(previousRatio, SaveGame.Instance.PlayerData.Experience, exp));
            
        }
        else{
            float previousRatio;
            if(isWin){
                if(SaveGame.Instance.PlayerData.Experience - 60 < 0){
                    previousRatio = 0;
                }
                else{
                    previousRatio = (float)(SaveGame.Instance.PlayerData.Experience - 60) / (float)exp;
                }
            }
            else{
                if(SaveGame.Instance.PlayerData.Experience - 20 < 0){
                    previousRatio = 0;
                }
                else{
                    previousRatio = (float)(SaveGame.Instance.PlayerData.Experience - 20) / (float)exp;
                }
            }
            
            StartCoroutine(ExperienceBarAnimation(previousRatio, SaveGame.Instance.PlayerData.Experience, exp));
        }
        
    }


    IEnumerator ExperienceBarAnimation(float previousRatio, long myExp, long exp){
        // fill the ExperienceBar image as the ratio of myExp/exp using animation
        float ratio = (float)myExp/exp;
        if(ratio >= 1){
            LevelText.text = SaveGame.Instance.PlayerData.PlayerLevel.ToString();
            ratio = 1;
            float currentRatio = previousRatio;
            Experience.text = exp + " / " + exp;
            while(currentRatio < ratio){
                currentRatio += 0.01f;
                ExperienceBar.fillAmount = currentRatio;
                yield return new WaitForSeconds(0.01f);
            }

            SaveGame.Instance.PlayerData.PlayerLevel++;
            int currentPlayerLevelPlusOne = SaveGame.Instance.PlayerData.PlayerLevel + 1;
            LevelText.text = SaveGame.Instance.PlayerData.PlayerLevel.ToString();
            SaveGame.Instance.PlayerData.Experience = myExp - exp;
            exp = (20 * (currentPlayerLevelPlusOne * currentPlayerLevelPlusOne)) - (20 * currentPlayerLevelPlusOne);
            float currentRatio2 = 0;
            ratio = (float)SaveGame.Instance.PlayerData.Experience/exp;
            Experience.text = SaveGame.Instance.PlayerData.Experience + " / " + exp;
            if(ratio == 0){
                ExperienceBar.fillAmount = 0;
            }
            while(currentRatio2 < ratio){
                currentRatio2 += 0.01f;
                ExperienceBar.fillAmount = currentRatio2;
                yield return new WaitForSeconds(0.01f);
            }
            

        }
        else{
            LevelText.text = SaveGame.Instance.PlayerData.PlayerLevel.ToString();
            Experience.text = SaveGame.Instance.PlayerData.Experience + " / " + exp;
            float currentRatio3 = previousRatio;
            while(currentRatio3 < ratio){
                currentRatio3 += 0.01f;
                ExperienceBar.fillAmount = currentRatio3;
                yield return new WaitForSeconds(0.01f);
            }
            
        }
        
    }
}
