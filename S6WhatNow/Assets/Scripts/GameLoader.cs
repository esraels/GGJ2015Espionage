using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour {

	GameObject m_curStage = null;

	// Use this for initialization
	void Start () {
	
		//LoadStage(2);

	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyUp("1")){
			LoadStage(1);
		}
		else if(Input.GetKeyUp("2")){
			LoadStage(2);
		}

	}

	public void LoadStage(int p_stageNum){

		if(m_curStage){
			Debug.Log("Destroying previous stage");
			Destroy(m_curStage);
		}

		Debug.Log("Loading a stage...");
		m_curStage = Instantiate(Resources.Load("Stage"+p_stageNum)) as GameObject;
		if(m_curStage){
			Debug.Log("Finished loading...");
		}
		else {
			Debug.Log("Failed to load Stage"+p_stageNum+"...");
		}

	}

}
