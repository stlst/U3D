using UnityEngine;
using System.Collections;

public class FindTheTreasure : MonoBehaviour {
	public static bool isSuccess;
	public AudioClip success;
	public Collider c;

	void Start(){
		isSuccess = false;
		c = GetComponent<Collider> ();
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player" && Maze.timer <= Maze.MaxTime) {
			isSuccess = true;
			AudioSource.PlayClipAtPoint (success, collider.transform.position);
		
			c.enabled = false;			//禁用敌人的collider组件，使其不会与其他物体发生碰撞
			Destroy (gameObject, 1.0f);			//1秒后删除敌人对象
		}
	}
}
