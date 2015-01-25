using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayButton : MonoBehaviour {

	GameLoader gameLoader;
	//GameFieldManager gameMgr;

	void Start () {
	
		gameLoader = GameObject.Find ("GameLoader").GetComponent<GameLoader> ();
		//gameMgr = GameObject.Find ("GameFieldManager").GetComponent<GameFieldManager> ();

	}

	public void LoadNextStage ()
	{
		gameLoader.DestroyTitleScreen();
		GameFieldManager.LoadStage(1);
	}
}
