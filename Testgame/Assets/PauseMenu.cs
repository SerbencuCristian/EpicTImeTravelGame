using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public GameObject Pausemenu;
    public GameObject player;
    public GameObject settingsImage;
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
    }
    public void ExitButton()
    {
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
        else if(context.performed)
        {
            ResumeButton();
        }
    }
}
