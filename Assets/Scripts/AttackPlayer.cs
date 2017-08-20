using UnityEngine;

public class AttackPlayer : MonoBehaviour
{
	private Transform player;  

	private float nextTimeHit = 0f;
	public float rateTimeHit = 0.5f;
	public float rateTimeRecalculatePath = 3.0f;
	private float nextTimeRecalculatePath = 0.0f;

	public int damage = 2;

	public UnityEngine.AI.NavMeshAgent navMeshAgent;

	void Start () {
		player = GameObject.FindWithTag("Player").transform; //find player       

	}

	void Update(){
        //recalculate path
		if(nextTimeRecalculatePath < Time.time){
			nextTimeRecalculatePath = Time.time + rateTimeRecalculatePath;
			navMeshAgent.SetDestination (player.position);
		}

        //give player damage
		if (Vector3.Distance (player.position, transform.position) < 1.8f && !GameplayMenu.visibleMenu) {
			nextTimeHit = Time.time + rateTimeHit;
			Player.HitPlayer(damage);
		}
	}
}