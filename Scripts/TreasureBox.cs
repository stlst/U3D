using UnityEngine;
using System.Collections;

public class TreasureBox : MonoBehaviour {
	public int treasure_num;
	Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator>();
		System.Random rd = new System.Random ();
		int randKey = rd.Next(1,100); 
		if (0 < randKey && randKey <= 40)
			treasure_num = 1;
		else if (40 < randKey && randKey <= 70)
			treasure_num = 2;
		else if (70 < randKey && randKey <= 85)
			treasure_num = 3;
		else if (85 < randKey && randKey <= 95)
			treasure_num = 4;
		else
			treasure_num = 5;
	}
	
	// Update is called once per frame
	void Update () {

	}
	public int count_treasure(){
		return treasure_num;
	}
	public void distribution(){
		treasure_num--;
		if (treasure_num < 0)
			treasure_num = 0;
	}
	public bool isEmpty(){
		if (treasure_num > 0)
			return false;
		else
			return true;
	}
	public void box_behaviour(){
		animator.SetBool ("isOpen", true);
//		Destroy (gameObject.transform.parent, 5.5f);	
		Invoke ("setClose", 2.5f);
		DestroyBox parentBox = gameObject.GetComponentInParent<DestroyBox> ();
		parentBox.selfDestroy ();
	}

	void setClose(){
		animator.SetBool ("isOpen", false);
	}
}
