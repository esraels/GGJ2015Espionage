using UnityEngine;
using System.Collections;

public class GameFieldManager : MonoBehaviour {

	public static int maxStage = 2;
	public static int currentStage = 1;

	public static PlayerInField.PlayerID activePlayer = PlayerInField.PlayerID.ONE;
	static PlayerInField m_curPlayer = null;

	static GameObject m_curStageGO = null;


	// Use this for initialization
	void Start () {

		LoadStage(1); //load the 1st stage.


	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyUp("1")){
			FocusToPlayer(PlayerInField.PlayerID.ONE);
		}
		else if(Input.GetKeyUp("2")){
			FocusToPlayer(PlayerInField.PlayerID.TWO);
		}

	}

	static void FocusToPlayer(PlayerInField.PlayerID p_playerNum){

		PlayerInField player = GameObject.FindWithTag("Hero" + (int)p_playerNum).GetComponent<PlayerInField>();
		if (player && player != m_curPlayer) {

			if(m_curPlayer) m_curPlayer.ShowIdle();

			activePlayer = p_playerNum;  //this will disable inputs for previous player in focus.
			//player.FocusToCamera();

			m_curPlayer = player;
		}

	}

	public static void ReloadStage(){
		LoadStage(currentStage);
	}

	public static void LoadNextStage(){
		LoadStage (currentStage + 1);
	}

	public static void LoadPrevStage(){
		LoadStage (currentStage - 1);
	}


	public static void LoadStage(int p_stageNum){

		//=======================
		// clamp to valid value
		//-----------------------
		currentStage = p_stageNum;

		if (currentStage < 1) {
				currentStage = 1;
		} 
		else if (currentStage > maxStage) {
				currentStage = maxStage;
		}

		//=======================
		// destroy previously 
		//   loaded stage
		//-----------------------
		if(m_curStageGO){
			Debug.Log("Destroying previous stage");
			Destroy(m_curStageGO);
		}

		//=======================
		// load the new stage
		//-----------------------
		m_curStageGO = Instantiate(Resources.Load("Stage"+p_stageNum)) as GameObject;


		GameFieldManager.FocusToPlayer(PlayerInField.PlayerID.ONE);

	}

}
