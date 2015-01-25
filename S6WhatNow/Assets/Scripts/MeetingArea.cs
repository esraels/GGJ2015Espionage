using UnityEngine;
using System.Collections;

public class MeetingArea : MonoBehaviour {

	public enum AreaType{
		NORMAL_MEET,
		FINISH_MEET
	}

	public AreaType m_areaType = AreaType.NORMAL_MEET;
	public GameObject m_goPrereq = null;  //prerequisit meeting area
	MeetingArea m_prereq = null;

	bool m_bPlayerOneEntered = false;
	bool m_bPlayerTwoEntered = false;

	bool m_bMeetingDone = false;
	// Use this for initialization
	void Start () {
	
		if(m_goPrereq){
			m_prereq = m_goPrereq.GetComponent<MeetingArea>();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool IsMeetingDone(){
		return m_bMeetingDone;
	}

	void OnTriggerEnter2D(Collider2D p_collidedObj){

		if (m_bMeetingDone)	return;

		Debug.Log("Entered Meeting Area...");

		if(m_prereq && !m_prereq.IsMeetingDone()){
			return;
		}

		PlayerInField player = p_collidedObj.GetComponent<PlayerInField>();
		if(player){
			PlayerInField.PlayerID ePlayer = player.GetPlayerNum();

			     if(ePlayer == PlayerInField.PlayerID.ONE) m_bPlayerOneEntered = true;
			else if(ePlayer == PlayerInField.PlayerID.TWO) m_bPlayerTwoEntered = true;
		
			if(m_bPlayerOneEntered && m_bPlayerTwoEntered){

				m_bMeetingDone = true;

				//Do other actions here
				if(m_areaType == AreaType.FINISH_MEET){
					Debug.Log("Game is Finished!");
					GameFieldManager.LoadNextStage();
				}
				else {
					//normal
					Debug.Log("Meeting Goal done!");
				}

			}

		}

	}


}
