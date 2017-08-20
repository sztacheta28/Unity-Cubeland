using UnityEngine;

public class AutoDestroy : MonoBehaviour {
	void Start () {
        //destroy bullet after 5 seconds
        Destroy(gameObject, 5f);
    }
}
