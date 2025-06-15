using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class PauseMenu : MonoBehaviour
{
    public GameObject Pausemenu;
    public GameObject player;
    public GameObject settingsImage;
    private PlayerControls controls;

    void Awake()
    {
        controls = KeybindManager.Instance.controls;
    }
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
        controls.Player.Pause.performed -= PauseGame;
        controls.Disable();
        GameObject.Find("KeybindsManager").SetActive(false);
        SceneManager.LoadScene("StartMenu");
        GameObject.Find("SaveData").GetComponent<SaveData>().data.timeindicator = GameObject.Find("GameController").GetComponent<GameController>().timeindicator;
        GameObject.Find("SaveData").GetComponent<SaveData>().data.lastCheckpoint = GameObject.Find("GameController").GetComponent<GameController>().lastCheckpoint;
        GameObject.Find("SaveData").GetComponent<SaveData>().SaveToJson(GameObject.FindObjectOfType<Carry>().save);
    }
    public void ExitButton()
    {
        GameObject.Find("SaveData").GetComponent<SaveData>().data.timeindicator = GameObject.Find("GameController").GetComponent<GameController>().timeindicator;
        GameObject.Find("SaveData").GetComponent<SaveData>().data.lastCheckpoint = GameObject.Find("GameController").GetComponent<GameController>().lastCheckpoint;
        GameObject.Find("SaveData").GetComponent<SaveData>().SaveToJson(GameObject.FindObjectOfType<Carry>().save);
        controls.Player.Pause.performed -= PauseGame;
        controls.Disable();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif //for editor
        Application.Quit();//for build
    }
    void OnEnable()
    {
        controls.Player.Pause.performed += PauseGame;
    }
    public void PauseGame(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Pausemenu.SetActive(true);
        }
        else if (context.performed && !settingsImage.activeSelf)
        {
            ResumeButton();
        }
    }
}
