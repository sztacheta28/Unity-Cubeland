using UnityEngine;
using System.Collections;

public class ScoresManager : MonoBehaviour {
    public static int bestGlobalScores = int.MaxValue;
    public static int currentPoints = 0;
    public static int lastSaveOffset = 0;
    public static int waitingOffset = 1000;
    public string globalScoresUpdateUrl = "http://ithome.webd.pl/scores.php?scores="; //url to update scores

    static public ScoresManager _instance;

    void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        ScoresManager.waitingOffset = 1000; 
        ScoresManager.lastSaveOffset = 0;
    }

    void Update () {
        //update only if player is alive
        if (!Player.die)
        {
            //ScoresManager.lastSaveOffset -> loaded points from save
            //points dont smaller than 0
            int timeInSeconds = (int)(Time.timeSinceLevelLoad - waitingOffset);
            ScoresManager.currentPoints = timeInSeconds > 0 ? timeInSeconds + ScoresManager.lastSaveOffset : 0 + ScoresManager.lastSaveOffset; 
        }
	}

    public static void saveBestScore()
    {
        if (ScoresManager.isNewBestScore()) {
            PlayerPrefs.SetInt("BestScore", ScoresManager.currentPoints);
        }
    }

    //save global best scores on WWW server
    public static void saveBestGlobalScore()
    {
        if (ScoresManager.isNewBestGlobalScore())
        {
            ScoresManager.bestGlobalScores = ScoresManager.currentPoints;

            _instance.StartCoroutine(_instance.SendGlobalBestScores(ScoresManager.bestGlobalScores)); //StartCoroutine need non static object
        }
    }
        
    IEnumerator SendGlobalBestScores(int scores)
    {
        WWW www = new WWW(globalScoresUpdateUrl+scores);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.error);
        }      
    }

    public static bool isNewBestScore()
    {
        return PlayerPrefs.GetInt("BestScore") < ScoresManager.currentPoints;    
    }

    public static bool isNewBestGlobalScore()
    {
        return ScoresManager.bestGlobalScores < ScoresManager.currentPoints;
    }
}
