﻿using UnityEngine;
using System.Collections;

public class ZombieAttack : MonoBehaviour {
	public int damage=1;					//敌人攻击造成的伤害值
	public float timeBetweenAttack=0.8f;	//敌人攻击之间的最小间隔（敌人攻击动画约为0.8秒，为了使得动画正常播放，该值最好设为0.8秒）
	public AudioClip enemyAttackAudio;		//敌人的攻击音效

	private float timer;				//攻击时间间隔，记录敌人从上次攻击到现在经过的时间
	private Animator animator;			//敌人的Animator组件，用于控制敌人动画的播放
	private ZombieHealth enemyHealth;	//敌人的生命值脚本

	// Use this for initialization
	void Start () {
		timer = 0.0f;								//将攻击时间间隔初始化
		animator = GetComponent<Animator> ();		//获取敌人的Animator组件	
		enemyHealth = GetComponent<ZombieHealth> ();	//获取敌人的生命值脚本
	}

	//与勾选了isTrigger属性的COllider组件共同用于检测：是否有物体进入敌人的攻击范围
	void OnTriggerStay(Collider collider1){
		if (enemyHealth.health <= 0) 	//若敌人生命值小于等于0，则说明敌人已经死亡，不具备攻击能力
			return;
		//当攻击间隔大于敌人攻击之间的最小间隔，且进入敌人攻击范围的对象标签是玩家时
		if (timer>=timeBetweenAttack && collider1.gameObject.tag == "Player") {
			//当游戏状态为游戏进行中（Playing）时
			if(GameManager.gameManager==null || GameManager.gameManager.gameState==GameManager.GameState.Playing){
				timer=0.0f;			//攻击后将攻击时间间隔清零
				animator.SetTrigger("attack");
				animator.SetBool ("isWalk", false);

				if(enemyAttackAudio!=null)				//在敌人位置处播放敌人的攻击音效
					AudioSource.PlayClipAtPoint(enemyAttackAudio,transform.position);
				if (GameManager.gameManager != null) {
					GameManager.gameManager.player.takeDamage (damage);//通过GameManager游戏管理类实现玩家扣血的效果
				}
			}
		}
	}

	//每帧执行一次，更新攻击间隔
	void Update(){
		timer += Time.deltaTime;	//更新攻击间隔，增加上一帧所花费的时间

	}
}
