
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CharacterDemoController : MonoBehaviour 
{
	public int XP;
	public int level;
	//public bool weapon;
	//	public State state;
	public bool[] heroList;
	public int[] levelXP;
	public int[] levelHP;
	public int[] levelDamage;
	public int HP=100;
	public int maxHP;
	public int attackDamage;
//	public int speed;
	public float minAttackTime = 1.5f;
	private float timer = 0.0f;	// record attack time duration
	public float minDist=1f; //	public float attackRange; 
	public GameObject target;  //receive user's input to change attack target
	private Animator animator;
	private Collider collider;			
	private Rigidbody rigidbody;	
	private bool defeatTeddy = false;
	public AudioClip playerHurtAudio;	
	public AudioClip playerAttackAudio;
	float				rotateSpeed = 20.0f; //used to smooth out turning
	public Vector3 		movementTargetPosition;
	public Vector3 		attackPos;
	public Vector3		lookAtPos;
	public Vector3      deltaTarget;
	public int 			WeaponState=1;
	Ray ray;
	RaycastHit hitInfo;
	GameObject gameObj;
	int rightmouse = -1;
	int disguisePermit = 0;


	public void Start () 
	{	
		levelXP = new int[]{100,300,900,2700,99999999 };
		levelHP = new int[]{100,105,110,115,120};
		levelDamage = new int[]{8,9,10,11,12 };
		heroList = new bool[3]{ false, false, false };
		animator = GetComponentInChildren<Animator>();//need this...
		movementTargetPosition = transform.position;//initializing our movement target as our current position

	}
	
	// Update is called once per frame
	public void Update () 
	{
		//The Update logic does:
		//	Get UI input from keyboard, and mouse clicks
		//	Tells mecanim what weaponstate we are in
		//	Tells mecanim what animation we should be playing based on variables such as idling, pain or death
		//	Handle movement and direction, apply root motion to move

		changeWeapon();
		characterMove ();
		isDead ();
		

	}

	public void characterMove(){
		if (!Input.GetKey (KeyCode.LeftAlt)) {//if we are not using the ALT key(camera control)...
			if (Input.GetMouseButton (0)) {
				rightmouse = 0;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);//从摄像机发出到点击坐标的射线
				if (Physics.Raycast (ray, out hitInfo)) {				
					Debug.DrawLine (ray.origin, hitInfo.point);//划出射线，只有在scene视图中才能看到
					gameObj = hitInfo.collider.gameObject;
					Debug.Log ("left click object name is " + gameObj.name);
					movementTargetPosition = hitInfo.point;
					minDist = 1f;
				}
			}
			if (rightmouse == 0) {
				if (gameObj.tag == "MonsterA") {
					Debug.Log ("MonsterA");
					if (WeaponState != 2 && heroList [0] == false) {
						minDist = 3f;
						movementTargetPosition = gameObj.transform.position;
					}
				} else if (gameObj.tag == "MonsterB") {
					Debug.Log ("MonsterB");
					if (WeaponState != 4 && heroList [1] == false) {
						minDist = 3f;
						movementTargetPosition = gameObj.transform.position;
					}
				} else if (gameObj.tag == "MonsterC") {
					Debug.Log ("MonsterC");
					if (WeaponState != 7 && heroList [2] == false) {
						minDist = 3f;
						movementTargetPosition = gameObj.transform.position;
					}
				} else if (gameObj.tag == "Teddy") {
					Debug.Log ("Teddy");
					if (defeatTeddy == false) {
						minDist = 3f;
						movementTargetPosition = gameObj.transform.position;
					}
				} else if (gameObj.tag == "Zombie") {
					Debug.Log ("Zombie");
					minDist = 3f;
					movementTargetPosition = gameObj.transform.position;
				} else if (gameObj.tag == "Boss") {
					Debug.Log ("Boss");
					minDist = 3f;
					movementTargetPosition = gameObj.transform.position;
				}
			}

				
			
			if (Input.GetMouseButton (1)) {
				rightmouse = 1;
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);//从摄像机发出到点击坐标的射线
				if (Physics.Raycast (ray, out hitInfo)) {				
					Debug.DrawLine (ray.origin, hitInfo.point);//划出射线，只有在scene视图中才能看到
					gameObj = hitInfo.collider.gameObject;
					Debug.Log ("right click object name is " + gameObj.name);
				}
			}
			
			if (rightmouse == 1) {
				if (gameObj.tag == "MonsterA") {
					Debug.Log ("MonsterA");
					if (WeaponState != 2 && heroList [0] == false) {
						minDist = 3.5f;
						movementTargetPosition = gameObj.transform.position;
					}
				} else if (gameObj.tag == "MonsterB") {
					Debug.Log ("MonsterB");
					if (WeaponState != 4 && heroList [1] == false) {
						minDist = 3.5f;
						movementTargetPosition = gameObj.transform.position;
					}
				} else if (gameObj.tag == "MonsterC") {
					Debug.Log ("MonsterC");
					if (WeaponState != 7 && heroList [2] == false) {
						minDist = 3.5f;
						movementTargetPosition = gameObj.transform.position;
					}
				} else if (gameObj.tag == "Teddy") {
					Debug.Log ("Teddy");
					if (defeatTeddy == false) {
						minDist = 3.5f;
						movementTargetPosition = gameObj.transform.position;
					}
				} else if (gameObj.tag == "Zombie") {
					Debug.Log ("Zombie");
					minDist = 3.5f;
					movementTargetPosition = gameObj.transform.position;
				} else if (gameObj.tag == "Boss") {
					Debug.Log ("Boss");
					minDist = 3.5f;
					movementTargetPosition = gameObj.transform.position;
				}
			}

				
			
		}
			
		//AttackCode has to go here for targeting reasons
		//		Vector3 deltaTarget = movementTargetPosition - transform.position;
		deltaTarget = movementTargetPosition - transform.position;

		lookAtPos = transform.position + deltaTarget.normalized*2.0f;
		lookAtPos.y = transform.position.y;


		Quaternion tempRot = transform.rotation; 	//save current rotation
		transform.LookAt(lookAtPos);						
		Quaternion hitRot = transform.rotation;		// store the new rotation
		// now we slerp orientation
		transform.rotation = Quaternion.Slerp(tempRot, hitRot, Time.deltaTime * rotateSpeed);

		if(Vector3.Distance(movementTargetPosition,transform.position)>minDist)
		{
			animator.SetBool("Idling", false);
		}
		else
		{
			animator.SetBool("Idling", true);

			if (minDist == 3.5f && timer > minAttackTime) {
				timer = 0.0f;
				attack ();


			} else {
				timer += Time.deltaTime;
			}
		}
	}

	public void changeWeapon(){
		switch(Input.inputString)//get keyboard input, probably not a good idea to use strings here...Garbage collection problems with regards to local string usage are known to happen
		{						 //the garbage collection memory problem arises from local alloction of memory, and not freeing it up efficiently
		case "0":
			WeaponState = 1;//1H one handed weapon
			break;
		case "1":
			if(disguisePermit == 1 || heroList[0] == true)
				WeaponState = 2;//2H two handed weapon(longsword or heavy axe)
			break;
		case "2":
			if(disguisePermit == 2 || heroList[1] == true)
				WeaponState = 4;//dual weild(short swords, light axes)
			break;
		case "3":
			if(disguisePermit == 3 || heroList[2] == true)
				WeaponState = 7;//spear
			break;		
		default:
			break;
		}
		changeState ();
		animator.SetInteger("WeaponState", WeaponState);// probably would be better to check for change rather than bashing the value in like this
	}

	public void attack(){
		gameObj = hitInfo.collider.gameObject;
		if (gameObj.tag == "MonsterA") {
			Debug.Log ("Attack MonsterA");
			MonsterAHealth monsterAHealth = gameObj.GetComponent<MonsterAHealth> ();
			if (monsterAHealth != null && monsterAHealth.health > 0) {
				animator.SetTrigger ("Use");//tell mecanim to do the attack animation(trigger)
				monsterAHealth.TakeDamage (attackDamage);
			} else if (monsterAHealth.health <= 0) {
				disguisePermit = 1;
			}
		} 
		else if (gameObj.tag == "MonsterB") {
			Debug.Log ("Attack MonsterB");
			MonsterBHealth monsterBHealth = gameObj.GetComponent<MonsterBHealth> ();
			if (monsterBHealth != null  && monsterBHealth.health>0 ) {
				animator.SetTrigger ("Use");
				monsterBHealth.TakeDamage (attackDamage);
			} else if (monsterBHealth.health <= 0) {
				disguisePermit = 2;
			}
		}else if (gameObj.tag == "MonsterC") {
			Debug.Log ("Attack MonsterC");
			MonsterCHealth monsterCHealth = gameObj.GetComponent<MonsterCHealth> ();
			if (monsterCHealth != null  && monsterCHealth.health>0 ) {
				animator.SetTrigger ("Use");
				monsterCHealth.TakeDamage (attackDamage);
			} else if (monsterCHealth.health <= 0) {
				disguisePermit = 3;
			}
		}
	/*	else if (gameObj.tag == "Teddy") {
			Debug.Log ("Attack Teddy");
			TeddyHealth teddyHealth = gameObj.GetComponent<TeddyHealth> ();
			if (teddyHealth != null) {
				teddyHealth.TakeDamage (attackDamage);
			}
		}*/
		else if (gameObj.tag == "Zombie") {
			Debug.Log ("Attack Zombie");
			ZombieHealth zombieHealth = gameObj.GetComponent<ZombieHealth> ();
			if (zombieHealth != null  && zombieHealth.health>0) {
				animator.SetTrigger ("Use");
				zombieHealth.TakeDamage (attackDamage);
			}
		}else if (gameObj.tag == "Boss") {
			Debug.Log ("Attack Boss");
			BossHealth bossHealth = gameObj.GetComponent<BossHealth> ();
			if (bossHealth != null  && bossHealth.health>0 ) {
				animator.SetTrigger ("Use");
				bossHealth.TakeDamage (attackDamage);
			}
		} 
	}

	public bool isDead(){
		if (HP <= 0) {
//		if(Input.GetMouseButton (2)){
//			animator.SetInteger ("Death", 1);
			return true;
		}
		else
			return false;
	}

	public void takeDamage(int damage){
//		if (Input.GetMouseButton (1)) {
//			animator.SetTrigger ("Pain");
//		}
//		damaged = true;
		animator.SetTrigger ("Pain");
		HP -= damage;

		if (HP < 0)
			HP = 0;
	}
	public void addXP(int bonusXP){
		XP += bonusXP;
		levelUp ();
	}

	public void levelUp(){
		if (XP < levelXP [0]) {
			level = 0;
		} else if (XP >= levelXP [0] && XP < levelXP [1]) {
			level = 1;
		} else if (XP >= levelXP [1] && XP < levelXP [2]) {
			level = 2;
		} else if (XP >= levelXP [2] && XP < levelXP [3]) {
			level = 3;
		} else if (XP >= levelXP [3] && XP < levelXP [4]) {
			level = 4;
		}
		maxHP = levelHP [level];
		attackDamage = levelDamage [level];	
	}

	public void changeState(){
		if (WeaponState == 1) {
			maxHP = levelHP [level];
			attackDamage = levelDamage [level];	
			if (HP > maxHP)
				HP = maxHP;
		} else if (WeaponState == 2) {
			maxHP = levelHP [level] + 20;
			attackDamage = levelDamage [level] + 1;
			if (HP > maxHP)
				HP = maxHP;
		} else if (WeaponState == 4) {
			maxHP = levelHP [level];
			attackDamage = levelDamage [level] + 1;
			if (HP > maxHP)
				HP = maxHP;
		} else if (WeaponState == 7) {
			maxHP = levelHP [level];
			attackDamage = levelDamage [level] + 3;
			if (HP > maxHP)
				HP = maxHP;
		}
	}
}