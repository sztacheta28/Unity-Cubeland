using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int life = 100;
    private bool died = false;
	public float dieHeight = -15f;

    public GameObject explosion;

    public int Life
    {
        get
        {
            return life;
        }
    }

    void Start()
    {
        //enemies counter up
        EnemiesManager.Add();
    }

	void Update(){
        //enemy is out of map so kill his
		if(transform.position.y < dieHeight){
			HitEnemy (100);
		}
	}

    public void HitEnemy(int hitLife)
    {
        GameObject expObj;

        life = life - hitLife; //get damage

        //player is death
        if (life < 0 && !died)
        {
            died = true;
            Destroy(gameObject);
            EnemiesManager.Delete(); //enemies counter down
            expObj = Instantiate(explosion, gameObject.transform.position, Quaternion.identity) as GameObject; //exlosion after death

            Destroy(expObj, 0.8f); //destroy explosion gameobject
        }
    }
}
