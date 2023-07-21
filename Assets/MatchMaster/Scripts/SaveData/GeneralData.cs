using UnityEngine;
namespace SNG.Save
{
    [System.Serializable]
    public class GeneralData
    {
        // put relevant fields here and set their values in the constructor
        public int Test = 5;

        public GeneralData()
        {
            Test = 51;
        }
    }
}
