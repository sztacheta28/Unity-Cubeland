using UnityEngine;

public class GraphicUserInterface : MonoBehaviour {

    void OnGUI()
    {
        //HUD
        GUI.Box(new Rect(10, 10, 80, 25), "Life : " + Player.Life);
        GUI.Box(new Rect(10, 45, 180, 25), "Enemies : " + EnemiesManager.Count);
        GUI.Box(new Rect(10, 80, 180, 25), "Bullets : " + Shooting.bullets);
        GUI.Box(new Rect(10, 115, 180, 25), "Scores : " + ScoresManager.currentPoints);

        //if player is dead show alert
        if (Player.die)
        {
            GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200), "LOST");

            //is new record?
            bool newRecord = ScoresManager.isNewBestScore();
            bool newGlobalRecord = ScoresManager.isNewBestGlobalScore();

            if (!newGlobalRecord)
            {
                GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 75, 130, 50), newRecord ? "NEW RECORD!!!" : "NO NEW RECORD :(");
            }
            else
            {
                GUI.Label(new Rect(Screen.width / 2 - 80, Screen.height / 2 - 75, 160, 50), "NEW GLOBAL RECORD!!!");
            }

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 25, 200, 50), "PLAY AGAIN"))
            {
                Gameplay.Restart();
            }
        }
    }
}
