using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour {
    public bool antialiasing = false;
    public bool anisotropic = true;
    public int shadowsType = 1; // 0 - Disabled, 1 - Hard only, 2 - All
    public static bool startedAttack = false;

    void Start()
    {
        //reset static members
        Time.timeScale = 1f;
        EnemiesManager.RestartCount();
        Player.RestartLife();
        Shooting.bullets = 50;
        Gameplay.startedAttack = false;

        //load settings
        antialiasing = PlayerPrefs.GetInt("Antialiasing", antialiasing ? 1 : 0) == 1 ? true : false;
        anisotropic = PlayerPrefs.GetInt("Anisotropic", anisotropic ? 1 : 0) == 1 ? true : false;
        shadowsType = PlayerPrefs.GetInt("Shadows", shadowsType);

        //set settings
        QualitySettings.antiAliasing = antialiasing ? 2 : 0;
        QualitySettings.anisotropicFiltering = anisotropic ? AnisotropicFiltering.Enable : AnisotropicFiltering.Disable;

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

        GameplayMenu.visibleMenu = false;
    }

    //on restart game...
    public static void Restart()
    {
        ScoresManager.saveBestScore(); //save local best score
        ScoresManager.saveBestGlobalScore(); //save global best score
        SceneManager.LoadScene("gameplay", LoadSceneMode.Single); //load gameplay scene again
    }
}
