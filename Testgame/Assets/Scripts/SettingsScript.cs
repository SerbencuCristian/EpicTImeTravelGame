using UnityEngine;
using UnityEngine.UI;
public class SettingsScript : MonoBehaviour
{
    public GameObject Pausemenu;
    public GameObject settingsImage;
    public Resolution[] resolutions;
    public Dropdown resolutionDropdown;
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
        if(Pausemenu != null)
        {
            Pausemenu.SetActive(true);
        }
    }
}
