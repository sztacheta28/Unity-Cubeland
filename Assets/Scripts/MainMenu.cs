using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Text scoresValueText;
    public Text globalScoresValueText;
    public Button loadSaveButton;
    public GameObject closeButtonGameObject;


    public string globalScoresUrl = "http://ithome.webd.pl/scores.txt";

    float defaultVolume = 0.5f;

    void Start()
    {
        scoresValueText.text = PlayerPrefs.GetInt("BestScore")+""; //load best score text field
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", defaultVolume); //load volume value

        //if didnt save game yet disable load save button
        bool savedSomething = PlayerPrefs.GetInt("savedSomething", 0) == 1 ? true : false;

        if (savedSomething)
        {
            loadSaveButton.interactable = true;
        }

        //dont load global best score in main rendering thread
        StartCoroutine(GetGlobalBestScores());

        //if not fullscreen disable close button
        if (!Screen.fullScreen)
        {
            closeButtonGameObject.SetActive(false);
        }
    }

    IEnumerator GetGlobalBestScores()
    {
        ScoresManager.bestGlobalScores = int.MaxValue;
      
        WWW www = new WWW(globalScoresUrl); //load scores from WWW
        yield return www; //wait

        //error
        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
            globalScoresValueText.text = "#err";
        }
        else //OK
        {
            Debug.Log(www.text);
            globalScoresValueText.text = www.text;

            if(!int.TryParse(www.text, out ScoresManager.bestGlobalScores))
            {
                ScoresManager.bestGlobalScores = int.MaxValue; //set loaded best global scores
            }

        }
    }
    
    public void onStartClick () {
        //on start gameplay first disable menu music 
        Destroy(MenuMusicPlayer._instance.gameObject);
        SceneManager.LoadScene("gameplay", LoadSceneMode.Single);
    }

    public void onLoadSaveClick()
    {
        //on load save first disable menu music 
        Destroy(MenuMusicPlayer._instance.gameObject);
        SceneManager.LoadScene("saveLoad", LoadSceneMode.Single);
    }

    public void onSettingsClick()
    {
        SceneManager.LoadScene("settings", LoadSceneMode.Single);
    }

    public void onCloseClick()
    {
        print("Close Application");
        Application.Quit();
    }
}
