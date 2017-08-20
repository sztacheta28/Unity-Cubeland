using UnityEngine;

public class MenuMusicPlayer : MonoBehaviour {
    public static MenuMusicPlayer _instance;

    public GameObject sound1;
    public GameObject sound2;
    public GameObject sound3;

    public int songIdx = 0;

    void Awake()
    {
        //no more than one player music!!!
        if (!_instance)
            _instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject); //dont stop music when go to settings menu from main menu
    }

    void Start()
    {
        songIdx = PlayerPrefs.GetInt("SongIdx", songIdx); //song to play
        switch (songIdx)
        {
            case 0:
                PlaySound1();
                break;
            case 1:
                PlaySound2();
                break;
            case 2:
                PlaySound3();
                break;
            default:
                PlaySound1();
                break;
        }
    }

    public void PlaySound1()
    {
        sound1.SetActive(true);
        sound2.SetActive(false);
        sound3.SetActive(false);
    }

    public void PlaySound2()
    {
        sound1.SetActive(false);
        sound2.SetActive(true);
        sound3.SetActive(false);
    }

    public void PlaySound3()
    {
        sound1.SetActive(false);
        sound2.SetActive(false);
        sound3.SetActive(true);
    }
}
