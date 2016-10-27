using UnityEngine;
using System.Collections;

public class changeMaterial : MonoBehaviour {
	public Material[] material;
	Renderer rend;
	CharacterDemoController ac;
	// Use this for initialization
	void Start () {
		ac = GetComponentInParent<CharacterDemoController>();
		rend = GetComponent<Renderer> ();
		rend.enabled = true;

		if (ac.WeaponState == 1)
			rend.sharedMaterial = material [0];
		else if (ac.WeaponState == 2)
			rend.sharedMaterial = material [1];
		else if (ac.WeaponState == 4)
			rend.sharedMaterial = material [2];
		else if (ac.WeaponState == 7)
			rend.sharedMaterial = material [3];
	}
	
	// Update is called once per frame
	void Update () {
//		ac = GetComponent<CharacterDemoController>();
//		Debug.Log ("weaponState" + ac.WeaponState);
		if (ac.WeaponState == 1)
			rend.sharedMaterial = material [0];
		else if (ac.WeaponState == 2)
			rend.sharedMaterial = material [1];
		else if (ac.WeaponState == 4)
			rend.sharedMaterial = material [2];
		else if (ac.WeaponState == 7)
			rend.sharedMaterial = material [3];
	}
}
