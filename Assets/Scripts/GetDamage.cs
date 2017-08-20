using UnityEngine;

public class GetDamage : MonoBehaviour
{
    public int damage = 5;
	private Enemy enemyComponent;

	void Start () {
        enemyComponent = GetComponent<Enemy>();
    }

	void OnCollisionEnter(Collision collision){	
        //if bullet hit enemy	
		if (collision.collider.transform.root.tag == "Bullet") {
			Destroy(collision.collider.gameObject); //destroy bullet
			enemyComponent.HitEnemy(damage); //get damage
		}
	}
}
