using UnityEngine;
using System.Collections;

public class Maze : MonoBehaviour {

	public static bool collectTheTreasure;
	public static float timer;
	public static float MaxTime = 30.0f;
	public AudioClip successAudio;
	GameObject player;
	 
	void Start(){
		collectTheTreasure = false;
		timer = 0.0f;
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void Update(){
		float currDis = Vector3.Distance (player.transform.position, transform.position);
		Debug.Log ("current distance = " + currDis);

		if ( currDis <= 50) {
			Debug.Log ("inside the maze");

			timer += Time.deltaTime;
		}
		if(currDis > 50) {
			Debug.Log ("reenter the maze" );
			timer = 0.0f;
		}
		if (timer >= MaxTime && FindTheTreasure.isSuccess == false) {
			collectTheTreasure = false;
			Debug.Log("fail task");
		}
		if (FindTheTreasure.isSuccess == true) {
			collectTheTreasure = true;
			Debug.Log("become Hero!!!");
		}
	}
}
