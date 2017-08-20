using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour {
    float defaultVolume = 0.5f;
    int defaultSongIdx = 0;

    float volume = 0.5f;
    public int songIdx = 0;
    public bool antialiasing = false; //true => 2x, false => 0x
    public bool anisotropic = true;
    public int shadowsType = 1; // 0 - Disabled, 1 - Hard only, 2 - All

    //GUI controls
    public Dropdown songDropdown;
    public Slider volumeSlider;
    public Toggle antialiasingToggle;
    public Toggle anisotropicToggle;
    public Dropdown shadowsDropdown;

    void Start () {
        //load saved settings
		songDropdown.value = PlayerPrefs.GetInt("SongIdx", songIdx);
        songIdx = PlayerPrefs.GetInt("SongIdx", songIdx);

        volumeSlider.value = PlayerPrefs.GetFloat("Volume", volume);
        volume = PlayerPrefs.GetFloat("Volume", volume);

        antialiasingToggle.isOn = PlayerPrefs.GetInt("Antialiasing", antialiasing ? 2 : 0) == 2 ? true : false;
        antialiasing = PlayerPrefs.GetInt("Antialiasing", antialiasing ? 2 : 0) == 2 ? true : false;

        anisotropicToggle.isOn = PlayerPrefs.GetInt("Anisotropic", anisotropic ? 1 : 0) == 1 ? true : false;
        anisotropic = PlayerPrefs.GetInt("Anisotropic", anisotropic ? 1 : 0) == 1 ? true : false;

        shadowsDropdown.value = PlayerPrefs.GetInt("Shadows", shadowsType);
        shadowsType = PlayerPrefs.GetInt("Shadows", shadowsType);
    }

    public void OnVolumeChange()
    {
        this.volume = volumeSlider.value;
        AudioListener.volume = volumeSlider.value;
    }

    public void OnSongChange()
    {
        this.songIdx = songDropdown.value;

        switch (songIdx)
        {
            case 0:
                MenuMusicPlayer._instance.GetComponent<MenuMusicPlayer>().PlaySound1();
                break;
            case 1:
                MenuMusicPlayer._instance.GetComponent<MenuMusicPlayer>().PlaySound2();
                break;
            case 2:
                MenuMusicPlayer._instance.GetComponent<MenuMusicPlayer>().PlaySound3();
                break;
            default:
                MenuMusicPlayer._instance.GetComponent<MenuMusicPlayer>().PlaySound1();
                break;
        }        
    }

    public void OnAntialiasingChange()
    {
        this.antialiasing = antialiasingToggle.isOn;
    }

    public void OnAnisotropicChange()
    {
        this.anisotropic = anisotropicToggle.isOn;
    }

    public void OnShadowsTypeChange()
    {
        this.shadowsType = shadowsDropdown.value;
    }

    public void OnSaveSettings()
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.SetInt("SongIdx", songIdx);
        PlayerPrefs.SetInt("Antialiasing", antialiasing ? 2 : 0);
        PlayerPrefs.SetInt("Anisotropic", anisotropic ? 1 : 0);
        PlayerPrefs.SetInt("Shadows", shadowsType);

        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }

    public void OnExitWithoutSave()
    {
        //load default
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", defaultVolume);

        songIdx = PlayerPrefs.GetInt("SongIdx", defaultSongIdx);
        switch (songIdx)
        {
            case 0:
                MenuMusicPlayer._instance.GetComponent<MenuMusicPlayer>().PlaySound1();
                break;
            case 1:
                MenuMusicPlayer._instance.GetComponent<MenuMusicPlayer>().PlaySound2();
                break;
            case 2:
                MenuMusicPlayer._instance.GetComponent<MenuMusicPlayer>().PlaySound3();
                break;
            default:
                MenuMusicPlayer._instance.GetComponent<MenuMusicPlayer>().PlaySound1();
                break;
        }


        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
    }
}
