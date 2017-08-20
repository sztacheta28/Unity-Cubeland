using UnityEngine;

public class Shooting : MonoBehaviour {
    public GameObject bulletPrefab;
    public GameObject gun; //gun in front of camera
    public static int bullets = 50;
    public AudioSource audioSourceShot;
    public AudioSource audioSourceDryFireGun;

    void Update () {        
        if (Input.GetButtonDown("Fire1") && !Player.die && !GameplayMenu.visibleMenu)
        {
            if (bullets > 0) {
                Shot();
            }
            else
            {
                audioSourceDryFireGun.Play(); //no bullets, empty magazine sound
            }
        }
    }

    void Shot() {
        --bullets;
        GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation) as GameObject; //instantiate bullet in gun position
        bullet.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)); //random color of bullet
        bullet.transform.rotation = Quaternion.Euler(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)); //random rotation
        bullet.GetComponent<Rigidbody>().AddForce(gun.transform.forward * 500); //add force to bullet
        audioSourceShot.Play(); //play sound
    }

    public static void AddBullets(int numBullets)
    {
        bullets += numBullets;
    }
}
