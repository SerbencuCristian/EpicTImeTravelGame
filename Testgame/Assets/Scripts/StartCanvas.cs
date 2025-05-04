using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartCanvas : MonoBehaviour
{
    public GameObject settingsImage;
    public void OnStartClick()
    {

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
}
