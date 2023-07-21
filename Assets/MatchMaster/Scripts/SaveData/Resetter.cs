using UnityEngine;
using UnityEditor;
using System.IO;

#if UNITY_EDITOR
public static class Resetter
{
    [MenuItem("SNG/Save Load/Reset Data")]
    private static void ResetData()
    {
        // reset in game data
        string dataPath = Path.Combine(Application.persistentDataPath, "Data");
        if (Directory.Exists(dataPath))
            Directory.Delete(dataPath, true);

        PlayerPrefs.DeleteAll();
        Debug.LogWarning("Data is reset");
    }

    [MenuItem("SNG/Save Load/Reset Backup Data")]
    private static void ResetBackUpData()
    {
        // reset in game backup data
        string dataPath = Path.Combine(Application.persistentDataPath, "Data_backup");
        if (Directory.Exists(dataPath))
            Directory.Delete(dataPath, true);
        Debug.LogWarning("Backup data is reset");
    }

    [MenuItem("SNG/Save Load/Backup the Data")]
    private static void BackUpData()
    {
        // backup in game data
        string dataPath = Path.Combine(Application.persistentDataPath, "Data");
        string backupPath = Path.Combine(Application.persistentDataPath, "Data_backup");
        if (Directory.Exists(backupPath))
            Directory.Delete(backupPath, true);
        FileUtil.CopyFileOrDirectory(dataPath, backupPath);
        Debug.LogWarning("Backup is taken");
    }

    [MenuItem("SNG/Save Load/Use Backup Data")]
    private static void UseBackUpData()
    {
        ResetData();

        string dataPath = Path.Combine(Application.persistentDataPath, "Data");
        string backupPath = Path.Combine(Application.persistentDataPath, "Data_backup");
        if (Directory.Exists(backupPath))
        {
            FileUtil.CopyFileOrDirectory(backupPath, dataPath);
            PlayerPrefs.SetInt("FirstTime", 1);
        }
        Debug.LogWarning("Using backup data");
    }
}
#endif


