using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour {

	public static bool collectTheTreasure;
	public static float timer;
	public static float MaxTime = 60.0f;
	public AudioClip enterAudio;
	 
	void Start(){
		collectTheTreasure = false;
		timer = 0.0f;
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player") {
			AudioSource.PlayClipAtPoint (enterAudio, collider.transform.position);
		}
	}

	void OnTriggerStay(Collider collider){
		if (collider.gameObject.tag == "Player") {
			timer += Time.deltaTime;
		}
	}

	void OnTriggerExit(Collider collider){
		if (collider.gameObject.tag == "Player") {
			timer = 0.0f;
		}
	}

	void Update(){ 
		if (timer >= MaxTime) {
			collectTheTreasure = false;
		}
		if (FindTheTreasure.isSuccess == true) {
			collectTheTreasure = true;
		}
	}
}
