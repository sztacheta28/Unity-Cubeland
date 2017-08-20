using UnityEngine;
using System.Collections;
using UnityEngine.PostProcessing;

public class Player : MonoBehaviour {
    private static int life = 100;
    public static bool die = false;
    public float dieHeight = -15f;

    private static bool lockUpdate = false;

    void Awake()
    {
        Player.lockUpdate = false;
    }


    public static int Life
    {
        get
        {
            return Player.life;
        }
        set
        {
            Player.life = value;
        }
    }

    void Update()
    {
        if (!Player.die && !lockUpdate && transform.position.y < dieHeight)
        {
            lockUpdate = true;
            Die();
        }
    }


    public static void HitPlayer(int hitLife) {
        Player.life = Player.life < hitLife ? 0 : Player.life - hitLife;

        if (Player.life == 0) {
            Die();
        }

    }

    private static void Die()
    {
        Time.timeScale = 0f;
        Player.die = true;
        GameObject.Find("FirstPersonController/MainCamera").GetComponent<PostProcessingBehaviour>().enabled = true;
        
    }

    public static void RestartLife()
    {
        Player.life = 100;
        Player.die = false;            
    }
}
