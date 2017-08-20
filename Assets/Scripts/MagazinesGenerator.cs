using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazinesGenerator : MonoBehaviour {
    private float nextTime = 0;
    public float rateTime = 1;

    public int probability = 5000;

    public GameObject magazinePrefab;

    void Start()
    {
        magazinePrefab = Resources.Load<GameObject>("Magazine"); //load prefab in runtime
    }

    void Update () {
        //instantiate magazines in random period
        if (nextTime < Time.time)
        {
            nextTime = Time.time + rateTime;

            if (Random.Range(0, probability) == 0)
            {
                Instantiate<GameObject>(magazinePrefab, new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z), Quaternion.Euler(new Vector3(45, 45, 45)));
            }
        }
	}
}
