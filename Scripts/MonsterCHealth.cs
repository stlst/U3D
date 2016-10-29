﻿using UnityEngine;
using System.Collections;

public class MonsterCHealth : MonoBehaviour {

	public int health=2;	//敌人的生命值
	public int XP=1;		//玩家击杀敌人后所获得的分数
	public AudioClip enemyHurtAudio;	//敌人的受伤音效

	private Animator animator;			//敌人的Animator组件
	private Collider collider1;			//敌人的Collider组件
	private Rigidbody rigidbody1;		//敌人的rigidbody组件
	private float hittimer;

	//初始化，获取敌人的组件
	void Start(){
		animator = GetComponent<Animator> ();	//获取敌人的Animator组件
		collider1 = GetComponent<Collider> ();	//获取敌人的Collider组件
		rigidbody1 = GetComponent<Rigidbody> ();	//获取敌人的Rigidbody组件
		hittimer = 0.0f;
	}

	//敌人受伤函数，用于PlayerAttack脚本中调用
	public void TakeDamage(int damage){	
		
		hittimer = 0.0f;
		health -= damage;
		Debug.Log ("damage:"+damage);
		Debug.Log ("health:"+health);
		animator.SetBool ("isHit", true);

		//敌人受伤扣血
		if (enemyHurtAudio != null)	//在敌人位置处播放敌人受伤音效
			AudioSource.PlayClipAtPoint (enemyHurtAudio, transform.position);

		if (health <= 0) {			//当敌人生命值小于等于0时，表明敌人已死亡
			if (GameManager.gameManager != null) {	
				GameManager.gameManager.player.addXP (XP);//玩家获得击杀敌人后得分
				if (GameManager.gameManager.player.getTaskB == true) {
					
					GameManager.gameManager.player.num_killMonsterC ++;
					Debug.Log ("killMonsterC: " + GameManager.gameManager.player.num_killMonsterC);
				}
			}
			animator.applyRootMotion = true;	//设置Animator组件的ApplyRootMotion属性，使敌人的移动与位移受动画的影响
			animator.SetTrigger ("isDead");		//设置动画参数，设置isDead的Trigger参数，播放敌人死亡动画
			collider1.enabled = false;			//禁用敌人的collider组件，使其不会与其他物体发生碰撞
			rigidbody1.useGravity = false;		//因为敌人的collider组件被禁用，敌人会因重力穿过地形系统下落，取消敌人受到的重力可以避免该现象
			Destroy (gameObject, 1.5f);			//3秒后删除敌人对象
		}

	}

	void Update(){
		hittimer += Time.deltaTime;
		if (hittimer >= 1.5f) {
			animator.SetBool ("isHit", false);
		}
	} 
}
