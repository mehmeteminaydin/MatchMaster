using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public int[] ObjectNumberList;
    public List<GameObject> ObjectList;
    public int Level1ObjectNumber = 20;

    private System.Random _random = new System.Random();
    private int _xCoor = 380;
    private int _zCoor = -1070;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = Physics.gravity * 9f;
        CreateNumberList();
        for (int i = 0; i < Level1ObjectNumber; i++)
        {
            // create the first object
            GameObject newObject = Instantiate(ObjectList[ObjectNumberList[i]]);
            GeneratePosition();
            Vector3 position = new Vector3(_xCoor, 540, _zCoor);
            newObject.transform.position = position;
        
            // create the twin
            newObject = Instantiate(ObjectList[ObjectNumberList[i]]);
            GeneratePosition();
            GeneratePosition();
            position = new Vector3(_xCoor, 540, _zCoor);
            newObject.transform.position = position;

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
        _zCoor = _random.Next(-1100, -1040);
    }
     
}
