using UnityEngine;
using System.Collections;

public class MonsterBAttack : MonoBehaviour {

	public int damage=1;					//敌人攻击造成的伤害值
	public float timeBetweenAttack=0.8f;	//敌人攻击之间的最小间隔（敌人攻击动画约为0.8秒，为了使得动画正常播放，该值最好设为0.8秒）
	public AudioClip enemyAttackAudio;		//敌人的攻击音效

	private float timer;				//攻击时间间隔，记录敌人从上次攻击到现在经过的时间
	private Animator animator;			//敌人的Animator组件，用于控制敌人动画的播放
	private MonsterBHealth monsterBHealth;	//敌人的生命值脚本

	// Use this for initialization
	void Start () {
		timer = 0.0f;								//将攻击时间间隔初始化
		animator = GetComponent<Animator> ();		//获取敌人的Animator组件	
		monsterBHealth = GetComponent<MonsterBHealth> ();	//获取敌人的生命值脚本
	}

	//与勾选了isTrigger属性的COllider组件共同用于检测：是否有物体进入敌人的攻击范围
	void OnTriggerStay(Collider collider1){
		if (monsterBHealth.health <= 0) 	//若敌人生命值小于等于0，则说明敌人已经死亡，不具备攻击能力
			return;
		//当攻击间隔大于敌人攻击之间的最小间隔，且进入敌人攻击范围的对象标签是玩家时
		if (timer>=timeBetweenAttack && collider1.gameObject.tag == "Player") {
			//当游戏状态为游戏进行中（Playing）时
			if(GameManager.gameManager!=null && GameManager.gameManager.gameState==GameManager.GameState.Playing
				&& GameManager.gameManager.player.WeaponState != 4 && GameManager.gameManager.player.heroList [1] != true){
				timer=0.0f;			//攻击后将攻击时间间隔清零
				animator.SetBool("attack", true);
				animator.SetBool ("isWalk", false);

				if(enemyAttackAudio!=null)				//在敌人位置处播放敌人的攻击音效
					AudioSource.PlayClipAtPoint(enemyAttackAudio,transform.position);
				if (GameManager.gameManager != null) {
					GameManager.gameManager.player.takeDamage (damage);//通过GameManager游戏管理类实现玩家扣血的效果
				}
			}
		}
	}

	//与勾选了isTrigger属性的COllider组件共同用于检测：是否有物体离开敌人的攻击范围
	void OnTriggerExit(Collider collider1){
		//若离开敌人攻击范围的物体标签是玩家时
		if (collider1.gameObject.tag == "Player"){
			animator.SetBool ("attack", false);	//设置动画参数，将isAttack布尔型参数设置为false，停止播放敌人攻击动画
			animator.SetBool ("isWalk", true);
		}
	}

	//每帧执行一次，更新攻击间隔
	void Update(){
		timer += Time.deltaTime;	//更新攻击间隔，增加上一帧所花费的时间

	}
}
