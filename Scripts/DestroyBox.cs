using UnityEngine;
using System.Collections;

public class DestroyBox : MonoBehaviour {

	public void selfDestroy(){
		Destroy (gameObject, 3.5f);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
