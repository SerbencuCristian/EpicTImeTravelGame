using UnityEngine;
using System.Collections.Generic;
public class SaveData : MonoBehaviour
{
    public Data data = new Data();
    public string keybindOverrides = "";
    public GameObject keybindsManager;
    void Awake()
    {
        keybindsManager = GameObject.Find("KeybindsImage");
        LoadFromJson(GameObject.FindObjectOfType<Carry>().save);
    }

    public void SaveToJson(int saveIndex = 1)
    {
        data.keybindOverrides = KeybindManager.Instance.SaveOverridesToString();
        string playerData = JsonUtility.ToJson(data);
        string filePath = Application.persistentDataPath + "/Save" + saveIndex + ".json";
        List<bool> triggeredScenes = GameObject.FindObjectOfType<GameController>().triggeredScenes;
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath,playerData);

    }
    public void LoadFromJson(int saveIndex = 1)
    {
        string filePath = Application.persistentDataPath + "/Save" + saveIndex + ".json";
        if (System.IO.File.Exists(filePath))
        {
            string playerData = System.IO.File.ReadAllText(filePath);
            data = JsonUtility.FromJson<Data>(playerData);
            KeybindManager.Instance.GetOverrides();
            SaveToJson(GameObject.FindObjectOfType<Carry>().save);
        }
        else
        {
            data = new Data();
            data.lastCheckpoint = Vector2.zero;
            data.timeindicator = 0;
            data.TriggeredScenes = new List<bool>();
            KeybindManager.Instance.GetOverrides();
            SaveToJson(GameObject.FindObjectOfType<Carry>().save);
        }
    }
}
[System.Serializable]
public class Data
{
    public Vector2 lastCheckpoint = Vector2.zero;
    public int timeindicator = 0;
    public string keybindOverrides = "";
    public List<bool> TriggeredScenes = new List<bool>();
}

