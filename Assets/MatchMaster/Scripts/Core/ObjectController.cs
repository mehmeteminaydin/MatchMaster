using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ObjectController : MonoBehaviour
{
    public int[] ObjectNumberList;
    public List<GameObject> ObjectList;
    public int Level1ObjectNumber = 2;
    public int HintCounter = 3;
    // I will edit the textmesh pro on Ui
    public TMPro.TextMeshProUGUI HintText;

    private System.Random _random = new System.Random();
    private int _xCoor = 380;
    private int _zCoor = -1070;
    private bool _isHintActive = false;
    private List<GameObject> _instantiatedObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // it shows the remaining hint count on the screen
        HintText.text =  HintCounter.ToString();


        Physics.gravity = Physics.gravity * 9f;
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

    public void onHintButtonPress()
    {
        if(_isHintActive){
            return;
        }
        if(HintCounter == 0){
            return;
        }
        if(_instantiatedObjects.Count < 2){
            return;
        }
        _isHintActive = true;

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
            onHintButtonPress();
            return;
        }
        // Find the valid twin objects within the same type
        List<int> validIndices = FindValidTwinIndices(startIndex);

        // Check if there are any valid twin objects
        if (validIndices.Count < 2)
        {
            // Handle the case when there are not enough valid twin objects
            Debug.Log("Not enough valid twin objects.");
            onHintButtonPress();
        }

        // Select two random valid indices from the list
        int randomIndex1 = validIndices[_random.Next(0, validIndices.Count)];
        // remove the first selected index from the list
        validIndices.Remove(randomIndex1);
        int randomIndex2 = validIndices[_random.Next(0, validIndices.Count)];
        Debug.Log("Random Index (1): " + randomIndex1 + " Random Index (2): " + randomIndex2);
        // Start the coroutine to initiate blinking for the selected twin objects
        StartCoroutine(BlinkObjects(randomIndex1, randomIndex2));
        HintCounter--;
        HintText.text =  HintCounter.ToString();
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
        Color blinkColor = Color.yellow;

        for (int i = 0; i < blinkCount; i++)
        {
            // Set the colors to the blink color
            SetMaterialColor(index1, blinkColor);
            SetMaterialColor(index2, blinkColor);

            // Wait for the blink duration
            yield return new WaitForSeconds(blinkDuration);

            // Set the colors back to the original colors
            SetMaterialColor(index1, originalColor1);
            SetMaterialColor(index2, originalColor2);

            // Wait for the blink duration
            yield return new WaitForSeconds(blinkDuration);
        }
        _isHintActive = false;
    }

    void SetMaterialColor(int index, Color color)
    {
        _isHintActive = true;
        _instantiatedObjects[index].GetComponent<Renderer>().material.color = color;
    }

    List<int> FindValidTwinIndices(int startIndex)
    {
        _isHintActive = true;
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
    
}
