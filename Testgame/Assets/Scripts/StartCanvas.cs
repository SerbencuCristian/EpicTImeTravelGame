using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartCanvas : MonoBehaviour
{
    public Carry carry;
    public GameObject settingsImage;
    public GameObject savesImage;
    void Start()
    {
        savesImage.SetActive(false);
        carry = GameObject.FindObjectOfType<Carry>();
    }
    public void OnStartClick(int save)
    {
        if (carry != null)
        {
            carry.save = save;
        }
        if(GameObject.Find("KeybindsManager")!= null)
            GameObject.Find("KeybindsManager").SetActive(true);
        SceneManager.LoadScene("MainScene");
    }
    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif //for editor
        Application.Quit();//for build

    }
    public void OnSettingsClick()
    {
        settingsImage.SetActive(true);
    }
    public void DeleteSave(int saveIndex)
    {
        string saveFilePath = Application.persistentDataPath + "/Save" + saveIndex + ".json";
        if (System.IO.File.Exists(saveFilePath))
        {
            System.IO.File.Delete(saveFilePath);
        }
    }
    public void ReturnStart()
    {
        savesImage.SetActive(false);
    }
    public void OnSavesClick()
    {
        savesImage.SetActive(true);
    }
}
