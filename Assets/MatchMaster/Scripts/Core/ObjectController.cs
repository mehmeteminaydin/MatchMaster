using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using SNG.Save;


public class ObjectController : MonoBehaviour
{
    public Dragging Dragging;
    public int[] ObjectNumberList;
    public List<GameObject> ObjectList;
    public int Level1ObjectNumber = 2;
    // I will edit the textmesh pro on Ui
    public TMPro.TextMeshProUGUI HintText;
    public TMPro.TextMeshProUGUI MagnetText;

    private int _magnetRepeat = 3;
    private System.Random _random = new System.Random();
    private int _xCoor = 380;
    private int _zCoor = -1070;
    private bool _isHintActive = false;
    private bool _isMagnetActive = false;
    private List<GameObject> _instantiatedObjects = new List<GameObject>();
    private Vector3 _leftHolePosition;
    private Vector3 _rightHolePosition;

    // Start is called before the first frame update
    void Start()
    {   
        SaveGame.Instance.PlayerData.HintCounter = 9;
        SaveGame.Instance.PlayerData.MagnetCounter = 9;
        
        _leftHolePosition = new Vector3(380, 542, -1152);
        _rightHolePosition = new Vector3(394, 542, -1152);

        // it shows the remaining hint count on the screen
        HintText.text =  SaveGame.Instance.PlayerData.HintCounter.ToString();
        MagnetText.text =  SaveGame.Instance.PlayerData.MagnetCounter.ToString();

        CreateNumberList();
        
        for (int i = 0; i < Level1ObjectNumber; i++)
        {
            for(int j = 0; j < 4; j++){
                GameObject newObject = Instantiate(ObjectList[ObjectNumberList[i]]);
                GeneratePosition();
                Vector3 position = new Vector3(_xCoor, 540, _zCoor);
                newObject.transform.position = position;
                _instantiatedObjects.Add(newObject);
            }
        }
    }

    

    void CreateNumberList()
    {
        ObjectNumberList = new int[50];
        for (int i = 0; i < 50; i++)
        {
            ObjectNumberList[i] = i;
        }
        ShuffleNumbers(ObjectNumberList);
    }

    void ShuffleNumbers(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

    void GeneratePosition()
    {
        _xCoor = _random.Next(356, 420);
        _zCoor = _random.Next(-1125, -1040);
    }

    public void OnHintButtonPress()
    {
        if(_isMagnetActive){
            return;
        }
        if(_isHintActive){
            return;
        }
        if(SaveGame.Instance.PlayerData.HintCounter == 0){
            return;
        }
        if(GetSizeOfList(_instantiatedObjects) < 2){
            return;
        }
        _isHintActive = true;
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn)
        {    
            AudioManager.instance.PlaySoundEffect("click");
        }
        // Select a random index for the twin type
        int randomTypeIndex = _random.Next(0, Level1ObjectNumber);
        bool nullFlag = true;
        // Calculate the indices of the twin type
        int startIndex = randomTypeIndex * 4;
        // if start index is null, find the next valid index
        for(int j = 0; j < 4; j++){
            if(_instantiatedObjects[startIndex + j] != null){
                nullFlag = false;
                break;
            }
        }
        if(nullFlag){
            _isHintActive = false;
            OnHintButtonPress();
            return;
        }
        // Find the valid twin objects within the same type
        List<int> validIndices = FindValidTwinIndices(startIndex, true);

        // Check if there are any valid twin objects
        if (validIndices.Count < 2)
        {
            // Handle the case when there are not enough valid twin objects
            Debug.Log("Not enough valid twin objects.");
            OnHintButtonPress();
        }

        // Select two random valid indices from the list
        int randomIndex1 = validIndices[_random.Next(0, validIndices.Count)];
        // remove the first selected index from the list
        validIndices.Remove(randomIndex1);
        int randomIndex2 = validIndices[_random.Next(0, validIndices.Count)];
        // Start the coroutine to initiate blinking for the selected twin objects
        StartCoroutine(BlinkObjects(randomIndex1, randomIndex2));
        SaveGame.Instance.PlayerData.HintCounter--;

        HintText.text =  SaveGame.Instance.PlayerData.HintCounter.ToString();
    }

    IEnumerator BlinkObjects(int index1, int index2)
    {
        _isHintActive = true;
        // Define the number of times the objects will blink
        int blinkCount = 3;

        // Define the duration of each blink
        float blinkDuration = 0.5f;

        // Define the colors to blink between
        Color originalColor1 = _instantiatedObjects[index1].GetComponent<Renderer>().material.color;
        Color originalColor2 = _instantiatedObjects[index2].GetComponent<Renderer>().material.color;
        Color blinkColor = Color.white;
        

        _instantiatedObjects[index1].transform.DOShakePosition(0.5f, 0.5f, 50, 50, false, true);
        _instantiatedObjects[index2].transform.DOShakePosition(0.5f, 0.5f, 50, 50, false, true);

        for (int i = 0; i < blinkCount; i++)
        {
            if (_instantiatedObjects[index1] != null && _instantiatedObjects[index2] != null){
                // Set the colors to the blink color
                SetMaterialColor(index1, blinkColor);
                SetMaterialColor(index2, blinkColor);

                // Wait for the blink duration
                yield return new WaitForSeconds(blinkDuration);
                if(_instantiatedObjects[index1] != null || _instantiatedObjects[index2] != null){
                    if(_instantiatedObjects[index1] != null){
                        SetMaterialColor(index1, originalColor1);
                    }
                    if(_instantiatedObjects[index2] != null){
                        SetMaterialColor(index2, originalColor2);
                    }

                    // Wait for the blink duration
                    yield return new WaitForSeconds(blinkDuration);
                }
                
            }
            

        }
        _isHintActive = false;
    }

    void SetMaterialColor(int index, Color color)
    {
        _isHintActive = true;
        _instantiatedObjects[index].GetComponent<Renderer>().material.color = color;
    }

    List<int> FindValidTwinIndices(int startIndex, bool isHint)
    {
        if(isHint){
            _isHintActive = true;
        }
        else{
            _isMagnetActive = true; 
        }

        List<int> validIndices = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            int currentIndex = startIndex + i;

            // check if the object destroyed, dont check it using color
            if (_instantiatedObjects[currentIndex] != null )
            {
                validIndices.Add(currentIndex);
            }
        }

        return validIndices;
    }
    
    public void MarkObjectDestroyed(GameObject destroyedObject)
    {
        // mark the destroyed object as null in _instantiatedObjects list
        _instantiatedObjects[_instantiatedObjects.IndexOf(destroyedObject)] = null;
        
    }
    public void OnMagnetButtonPress()
    {
        if(_isHintActive){
            return;
        }
        if(_isMagnetActive){
            return;
        }
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn)
        {    
            AudioManager.instance.PlaySoundEffect("click");
        }
        ChangeTagToUntagged();
        OnMagnetButtonPressHelper();
    }
    public void OnMagnetButtonPressHelper()
    {
        if(SaveGame.Instance.PlayerData.MagnetCounter == 0){
            ChangeTagToCube();
            return;
        }
        if(GetSizeOfList(_instantiatedObjects) == 0){
            if(_magnetRepeat == 1 || _magnetRepeat == 2){
                _magnetRepeat = 3;
                SaveGame.Instance.PlayerData.MagnetCounter--;
                MagnetText.text =  SaveGame.Instance.PlayerData.MagnetCounter.ToString();
                return;
            }
            else{
                return;
            }
        }
        
        Dragging.ClearHole();
        _isMagnetActive = true;

        // Select a random index for the twin type
        int randomTypeIndex = _random.Next(0, Level1ObjectNumber);
        bool nullFlag = true;
        // Calculate the indices of the twin type
        int startIndex = randomTypeIndex * 4;
        // if start index is null, find the next valid index
        for(int j = 0; j < 4; j++){
            if(_instantiatedObjects[startIndex + j] != null){
                nullFlag = false;
                break;
            }
        }
        if(nullFlag){
            _isMagnetActive = false;
            OnMagnetButtonPressHelper();
            return;
        }
        // Find the valid twin objects within the same type "false" indicates it is magnet
        List<int> validIndices = FindValidTwinIndices(startIndex, false);


        // Select two random valid indices from the list
        int randomIndex1 = validIndices[_random.Next(0, validIndices.Count)];
        // remove the first selected index from the list
        validIndices.Remove(randomIndex1);
        int randomIndex2 = validIndices[_random.Next(0, validIndices.Count)];
        // Start the coroutine to initiate blinking for the selected twin objects
        StartCoroutine(PlaceObjects(randomIndex1, randomIndex2));

    }

    IEnumerator PlaceObjects(int index1, int index2)
    {
        _isMagnetActive = true;

        // Define the duration of each movement
        float moveDuration = 0.5f;

        _instantiatedObjects[index1].transform.DOMove(_leftHolePosition, 0.3f);
        _instantiatedObjects[index2].transform.DOMove(_rightHolePosition, 0.3f);

        yield return new WaitForSeconds(moveDuration);
        _isMagnetActive = false;
        Destroy(_instantiatedObjects[index1]);
        Destroy(_instantiatedObjects[index2]);
        if(SaveGame.Instance.GeneralData.IsSoundEffectsOn)
        {    
            AudioManager.instance.PlaySoundEffect("matched_2");
        }
        _instantiatedObjects[index1] = null;
        _instantiatedObjects[index2] = null;
        Dragging.DecreaseObjectCounter();
        _magnetRepeat--;
        if(_magnetRepeat > 0){
            OnMagnetButtonPressHelper();
        }
        else{
            _magnetRepeat = 3;
            SaveGame.Instance.PlayerData.MagnetCounter--;
            MagnetText.text =  SaveGame.Instance.PlayerData.MagnetCounter.ToString();
            ChangeTagToCube();
        }
    }

    private int GetSizeOfList(List<GameObject> list){
        int count = 0;
        for(int i = 0; i < list.Count; i++){
            if(list[i] != null){
                count++;
            }
        }
        return count;
    }

    private void ChangeTagToCube(){
        for(int i = 0; i < _instantiatedObjects.Count; i++){
            if(_instantiatedObjects[i] != null){
                _instantiatedObjects[i].gameObject.tag = "cube";
            }
        }
    }

    private void ChangeTagToUntagged(){
        for(int i = 0; i < _instantiatedObjects.Count; i++){
            if(_instantiatedObjects[i] != null){
                _instantiatedObjects[i].gameObject.tag = "Untagged";
            }
        }
    }
    
}
