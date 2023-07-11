using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    private System.Random _random = new System.Random();
    public int[] objectNumberList;
    public List<GameObject> objectList;
    private int x = 400;
    private int y = 600;

    // Start is called before the first frame update
    void Start()
    {
        CreateNumberList();
        for (int i = 0; i < 30; i++)
        {
            GameObject newObject = Instantiate(objectList[objectNumberList[i]]);
            //newObject.transform.SetParent(GameObject.Find("Canvas").transform, false);

            // set the position of the object using GeneratePosition function
            GeneratePosition();
            Vector3 position = new Vector3(x, y, -990);
            newObject.transform.position = position;
            // scale the object and the child so that I can touch the object easily, make them bigger
            //newObject.transform.localScale = new Vector3(64, 64, 64);
            //newObject.transform.GetChild(0).transform.localScale = new Vector3(5  , 5, 5);
            

        }
    }

    void CreateNumberList()
    {
        objectNumberList = new int[50];
        for (int i = 0; i < 50; i++)
        {
            objectNumberList[i] = i;
        }
        ShuffleNumbers(objectNumberList);
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

    // generate position x and y values. X should be between 355 and 420, Y should be between 576 and 708
    // 355, 576 is the bottom left corner of the screen and 420, 708 is the top right corner of the screen
    // generate only one (x and y)

    void GeneratePosition()
    {
        x = _random.Next(355, 420);
        y = _random.Next(576, 708);
    }
     
}
