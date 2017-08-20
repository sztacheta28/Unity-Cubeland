using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {
	void Update () {
        transform.Rotate(45 * Vector3.right * Time.deltaTime, Space.World); //slowly rotate magazine in world cordinates
	}

    //if player touch magazine
    void OnTriggerEnter(Collider other)
    {
        Shooting.AddBullets(20); //add player bullets
        Destroy(gameObject); //destroy magazine
    }
}
