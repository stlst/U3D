using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

//readme: 调用方法- “GameManager.gameManager.player.(functions of player)”

public class GameManager : MonoBehaviour {
	static public GameManager gameManager; //static gamecontroller, the only one gm object in the game
	public GameObject playerGameObject;//找到游戏中的gameobject player
	public CharacterDemoController player; //player control的脚本，用于调用相关函数
	public enum GameState {Start,Playing,Pause,Win,Lose};
	public GameState gameState;
	// Use this for initialization
	void Start () {
		gameManager = GetComponent<GameManager> ();
		player = playerGameObject.GetComponent<CharacterDemoController>();
		if (playerGameObject == null)
			playerGameObject = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		switch (gameState) {
		case GameState.Start:
			//SceneManager.LoadScene ("start");
			break;

		case GameState.Win:
			//SceneManager.LoadScene ("win");
			break;

		case GameState.Pause:
			//SceneManager.LoadScene ("pause");
			break;

		case GameState.Playing:
			//SceneManager.LoadScene ("playing");
			if (player.isDead ())
				gameManager.gameState = GameState.Lose;
			//else if() -----------if the boss is defeated, gamestate will change into "win"
			break;

		case GameState.Lose:
			//SceneManager.LoadScene ("lose");
			SceneManager.LoadScene("level11");
			break;
		}
	}
}

