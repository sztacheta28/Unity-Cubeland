using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayMenu : MonoBehaviour {
    public static bool visibleMenu = false;

    public GameObject gameplayMenu;
    public GameObject settingsMenu;

    //if didnt save settings yet use default settings
    float defaultVolume = 0.5f;
    int defaultAntialiasing = 0;
    bool defaultAnisotropic = true;
    int defaultShadowsType = 1;

    //current settings
    float volume = 0.5f;
    public bool antialiasing = false; //0 or 2
    public bool anisotropic = true;
    public int shadowsType = 1; // 0 - Disabled, 1 - Hard only, 2 - All

    //GUI components
    public Slider volumeSlider;
    public Toggle antialiasingToggle;
    public Toggle anisotropicToggle;
    public Dropdown shadowsDropdown;
    public Button saveAndReturnToMainMenuButton;

    public SaveManager saveManager;

    void Start ()
    {
        //load settings
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", volume);
        volume = PlayerPrefs.GetFloat("Volume", volume);

        antialiasingToggle.isOn = PlayerPrefs.GetInt("Antialiasing", antialiasing ? 2 : 0) == 2 ? true : false;
        antialiasing = PlayerPrefs.GetInt("Antialiasing", antialiasing ? 2 : 0) == 2 ? true : false;

        anisotropicToggle.isOn = PlayerPrefs.GetInt("Anisotropic", anisotropic ? 1 : 0) == 1 ? true : false;
        anisotropic = PlayerPrefs.GetInt("Anisotropic", anisotropic ? 1 : 0) == 1 ? true : false;

        shadowsDropdown.value = PlayerPrefs.GetInt("Shadows", shadowsType);
        shadowsType = PlayerPrefs.GetInt("Shadows", shadowsType);
    }    

    void Update()
    {
        //show in gameplay menu when press Esc
        if (!visibleMenu && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowGameplayMenu();
        }

        //save enabled only if player alive and enemies attack him
        if (Player.die || !Gameplay.startedAttack)
        {
            saveAndReturnToMainMenuButton.interactable = false;
        }
        else
        {
            saveAndReturnToMainMenuButton.interactable = true;
        }
    }

    public void OnVolumeChange()
    {
        this.volume = volumeSlider.value;
        AudioListener.volume = volumeSlider.value;
    }

    public void OnAntialiasingChange()
    {
        this.antialiasing = antialiasingToggle.isOn;
        QualitySettings.antiAliasing = antialiasing ? 2 : 0;
    }

    public void OnAnisotropicChange()
    {
        this.anisotropic = anisotropicToggle.isOn;
        QualitySettings.anisotropicFiltering = anisotropic ? AnisotropicFiltering.Enable : AnisotropicFiltering.Disable;
    }

    public void OnShadowsTypeChange()
    {
        this.shadowsType = shadowsDropdown.value;

        switch (shadowsType)
        {
            case 0:
                QualitySettings.shadows = ShadowQuality.Disable;
                break;
            case 1:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
            case 2:
                QualitySettings.shadows = ShadowQuality.All;
                break;
            default:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
        }
    }

    public void ShowGameplayMenu()
    {
        visibleMenu = true;
        Time.timeScale = 0.0f; //stop time when menu in gameplay is visible
        gameplayMenu.SetActive(true);
        settingsMenu.SetActive(false);

    }

    public void ShowSettingsMenu()
    {
        gameplayMenu.SetActive(false);
        settingsMenu.SetActive(true);

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", volume);
        volume = PlayerPrefs.GetFloat("Volume", volume);

        antialiasingToggle.isOn = PlayerPrefs.GetInt("Antialiasing", antialiasing ? 2 : 0) == 2 ? true : false;
        antialiasing = PlayerPrefs.GetInt("Antialiasing", antialiasing ? 2 : 0) == 2 ? true : false;

        anisotropicToggle.isOn = PlayerPrefs.GetInt("Anisotropic", anisotropic ? 1 : 0) == 1 ? true : false;
        anisotropic = PlayerPrefs.GetInt("Anisotropic", anisotropic ? 1 : 0) == 1 ? true : false;

        shadowsDropdown.value = PlayerPrefs.GetInt("Shadows", shadowsType);
        shadowsType = PlayerPrefs.GetInt("Shadows", shadowsType);
    }

    public void ResumePlaying()
    {
        Time.timeScale = 1.0f; //when resume playing start time again
        visibleMenu = false;
        gameplayMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void SaveAndReturnToMainMenu()
    {
        saveManager.Save();

        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }

    public void SaveSettingsAndExit()
    {
        //save settings
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("Antialiasing", antialiasing ? 2 : 0);
        PlayerPrefs.SetInt("Anisotropic", anisotropic ? 1 : 0);
        PlayerPrefs.SetInt("Shadows", shadowsType);

        //go to gameplay menu
        ShowGameplayMenu();
    }

    public void ExitSettingsWithoutSave()
    {
        //load again default values
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", defaultVolume);
        QualitySettings.antiAliasing = PlayerPrefs.GetInt("Antialiasing", defaultAntialiasing);
        QualitySettings.anisotropicFiltering = PlayerPrefs.GetInt("Anisotropic", defaultAnisotropic ? 1 : 0) == 1 ? AnisotropicFiltering.Enable : AnisotropicFiltering.Disable;

        int shadowsType = PlayerPrefs.GetInt("Shadows", defaultShadowsType);

        switch (shadowsType)
        {
            case 0:
                QualitySettings.shadows = ShadowQuality.Disable;
                break;
            case 1:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
            case 2:
                QualitySettings.shadows = ShadowQuality.All;
                break;
            default:
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
        }

        ShowGameplayMenu();
    }
}
