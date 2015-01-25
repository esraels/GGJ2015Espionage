using UnityEngine;
using System.Collections;

public class SensorObject : MonoBehaviour {

	enum State {
		TURNING_OFF,
		TURNING_ON
	};


	public string keyToTurnOn = "c";
	public string keyToTurnOff = "v";

	public bool m_Enable = true;
	public string m_passcode;
	public GameObject m_sensorPartner = null;
	Animator m_partner = null;

	Animator m_animator = null;

	// Use this for initialization
	void Start () {
		if(m_animator == null){
			m_animator = gameObject.GetComponent<Animator>();
		}
		if(m_sensorPartner){
			m_partner = m_sensorPartner.GetComponent<Animator>();
		}

		if(m_Enable)
			m_animator.Play("SensorStayOn");
		else 
			m_animator.Play("SensorStayOff");
	}
	
	// Update is called once per frame
	void Update () {
		//???: test code only
		if(Input.GetKeyUp(keyToTurnOn)){
			OpenSensor();
		}
		else if(Input.GetKeyUp(keyToTurnOff)){
			CloseSensor();
		}
	}

	void OnTriggerEnter2D(Collider2D p_collidedObj){
		PlayerInField player = p_collidedObj.gameObject.GetComponent<PlayerInField>();
		if(player){
			GameFieldManager.ReloadStage();
		}
	}

//	void OnTriggerEnter2D(Collider2D p_collidedObj){
//		PlayerInField player = p_collidedObj.gameObject.GetComponent<PlayerInField>();
//		if(player){
//			player.ShowPasscode(m_passcode);
//		}
//	}
//	
//	void OnTriggerExit2D(Collider2D p_collidedObj){
//		//=================================
//		// If player goes outside the range 
//		//   of this door, hide passcode.
//		//---------------------------------
//		PlayerInField player = p_collidedObj.gameObject.GetComponent<PlayerInField>();
//		if(player){
//			CircleCollider2D bounds = GetComponent<CircleCollider2D>();
//			Vector2 posP = player.transform.position;
//			Vector2 posG = transform.position;
//			float radius = bounds.radius * transform.localScale.x;
//			
//			if((posP - posG).sqrMagnitude > radius * radius){
//				player.HidePasscode();
//			}
//		}
//	}

	public void OpenSensor(){
		m_animator.SetInteger("SensorState", (int)State.TURNING_ON );
		if(m_partner){
			m_partner.SetInteger("SensorState", (int)State.TURNING_OFF);		
		}
	}
	
	public void CloseSensor(){
		m_animator.SetInteger("SensorState", (int)State.TURNING_OFF );
		if(m_partner){
			m_partner.SetInteger("SensorState", (int)State.TURNING_ON);		
		}
	}


}
