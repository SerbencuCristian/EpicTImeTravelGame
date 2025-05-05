using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public GameObject Pausemenu;
    public GameObject player;
    public GameObject settingsImage;

    void Start()
    {
        Pausemenu = GameObject.Find("PauseMenu");
        player = GameObject.Find("Player");
        settingsImage = GameObject.Find("SettingsImage"); 
        settingsImage.SetActive(false);
        Pausemenu.SetActive(false);  
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        Pausemenu.SetActive(false);
    }
    public void UnstuckButton()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(player.GetComponent<PlayerHealth>().currentHealth);
        ResumeButton();
    }
    public void SettingsButton()
    {
        settingsImage.SetActive(true);
        Pausemenu.SetActive(false);
    }
    public void MainMenuButton()
    {
        Time.timeScale=1;
        SceneManager.LoadScene("StartMenu");
        GameObject.Find("SaveData").GetComponent<SaveData>().data.timeindicator = GameObject.Find("GameController").GetComponent<GameController>().timeindicator;
        GameObject.Find("SaveData").GetComponent<SaveData>().data.lastCheckpoint = GameObject.Find("GameController").GetComponent<GameController>().lastCheckpoint;
        GameObject.Find("SaveData").GetComponent<SaveData>().SaveToJson();
    }
    public void ExitButton()
    {
        GameObject.Find("SaveData").GetComponent<SaveData>().data.timeindicator = GameObject.Find("GameController").GetComponent<GameController>().timeindicator;
        GameObject.Find("SaveData").GetComponent<SaveData>().data.lastCheckpoint = GameObject.Find("GameController").GetComponent<GameController>().lastCheckpoint;
        GameObject.Find("SaveData").GetComponent<SaveData>().SaveToJson();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif //for editor
        Application.Quit();//for build
    }
    public void PauseGame(InputAction.CallbackContext context)
    {
        if(context.performed && Time.timeScale == 1)
        {
            Time.timeScale=0;
            Pausemenu.SetActive(true);
        }
        else if(context.performed && !settingsImage.activeSelf)
        {
            ResumeButton();
        }
    }
}
