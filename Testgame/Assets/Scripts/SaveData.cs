using UnityEngine;
using System.Collections.Generic;
public class SaveData : MonoBehaviour
{
    public Data data = new Data();
    void Awake()
    {
        LoadFromJson();
    }

    public void SaveToJson()
    {
        string playerData = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + "/Save.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath,playerData);

    }
    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/Save.json";
        if (System.IO.File.Exists(filePath))
        {
            string playerData = System.IO.File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(playerData);
        }
        else
        {
            SaveToJson();
        }
    }
}
[System.Serializable]
public class Data
{
    public Vector2 lastCheckpoint = Vector2.zero;
    public int timeindicator = 0;
}

