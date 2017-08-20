using UnityEngine;

public class AudioManager : MonoBehaviour {
	public AudioSource audioSourceWater;
	public AudioSource audioSourceJump;

	void OnTriggerStay(Collider other){
        //if touch water play water sound
		if(other.gameObject.tag == "Water"){
			if(!audioSourceWater.isPlaying){
				audioSourceWater.Play ();
			}
		}
	}

	void OnTriggerExit(Collider other){
        //if dont touch water dont play water sound
        if (other.gameObject.tag == "Water"){
			if(audioSourceWater.isPlaying){
				audioSourceWater.Pause ();
			}
		}
	}

	void Update(){
        //if pressed jump key play jump sound
		if(Input.GetButtonDown("Jump")){
			audioSourceJump.Play();
		}
	}
}
