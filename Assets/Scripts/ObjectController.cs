using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{

    private System.Random _random = new System.Random();
    public int[] objectNumberList;
    public List<GameObject> objectList;
    private int x = 400;
    private int z = -1100;

    // Start is called before the first frame update
    void Start()
    {
        CreateNumberList();
        for (int i = 0; i < 30; i++)
        {
            GameObject newObject = Instantiate(objectList[objectNumberList[i]]);
            GeneratePosition();
            Vector3 position = new Vector3(x, 530, z);
            newObject.transform.position = position;

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

    void GeneratePosition()
    {
        x = _random.Next(356, 420);
        z = _random.Next(-1100, -1023);
    }
     
}
