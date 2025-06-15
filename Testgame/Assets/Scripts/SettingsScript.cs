using UnityEngine;
using UnityEngine.UI;
public class SettingsScript : MonoBehaviour
{
    public GameObject Pausemenu;
    public GameObject settingsImage;
    public GameObject KeybindsImage;
    public Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public GameObject KeybindsImageGp;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        foreach (Resolution res in resolutions)
        {
            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = System.Array.IndexOf(resolutions, res);
            }
            resolutionDropdown.options.Add(new Dropdown.OptionData(res.width + "x" + res.height));
        }
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        if(KeybindsImage !=null)
            KeybindsImage.SetActive(false);
        if(KeybindsImageGp != null)
            KeybindsImageGp.SetActive(false);
    }
    public void FullscreenToggle(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void ReturnButton()
    {
        settingsImage.SetActive(false);
        if (Pausemenu != null)
        {
            Pausemenu.SetActive(true);
        }
    }
    public void KeybindsButton()
    {
        settingsImage.SetActive(false);
        if (KeybindsImage != null)
        {
            KeybindsImage.SetActive(true);
        }
    }
    public void KeybindsReturnButton()
    {
        KeybindsImage.SetActive(false);
        if (settingsImage != null)
        {
            settingsImage.SetActive(true);
        }
    }
    public void KeybindsButtonGp()
    {
        if (settingsImage != null)
        {
            KeybindsImageGp.SetActive(true);
        }
    }
    public void KeybindsReturnButtonGp()
    {
        KeybindsImageGp.SetActive(false);
        if (settingsImage != null)
        {
            KeybindsImage.SetActive(true);
        }
    }
}
