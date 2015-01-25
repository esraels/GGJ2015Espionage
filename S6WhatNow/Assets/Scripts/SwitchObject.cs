using UnityEngine;
using System.Collections;

public class SwitchObject : MonoBehaviour {


	//bool m_bPlayerIsNear = false;

	public GameObject[] m_listObstables;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

//		if(m_bPlayerIsNear){
//			if(Input.GetKeyUp("k")){
//				DeactivateObstacles();
//			}
//		}
	
	}

	public void DeactivateObstacles(){

		Debug.Log("Deactivating switches...");

		foreach(GameObject obstacle in m_listObstables){
			if(obstacle == null) continue;

			DoorObject door = obstacle.GetComponent<DoorObject>();
			if(door){ door.OpenDoor(); continue;}

			SensorObject sensor = obstacle.GetComponent<SensorObject>();
			if(sensor){ sensor.CloseSensor(); continue;}

		}
	}

	public void ActivateObstacles(){

		Debug.Log("Activating switches...");
		
		foreach(GameObject obstacle in m_listObstables){
			if(obstacle == null) continue;
			
			DoorObject door = obstacle.GetComponent<DoorObject>();
			if(door){ door.CloseDoor(); continue;}
			
			SensorObject sensor = obstacle.GetComponent<SensorObject>();
			if(sensor){ sensor.OpenSensor(); continue;}
			
		}
	}

	void OnTriggerEnter2D(Collider2D p_collidedObj){
		PlayerInField player = p_collidedObj.gameObject.GetComponent<PlayerInField>();
		if(player){
			//m_bPlayerIsNear = true;
			player.ShowPasscode("-");
			player.BecomeNearTo(gameObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D p_collidedObj){
		//=================================
		// If player goes outside the range 
		//   of this door, hide passcode.
		//---------------------------------
		PlayerInField player = p_collidedObj.gameObject.GetComponent<PlayerInField>();
		if(player){
			CircleCollider2D circBounds = GetComponent<CircleCollider2D>();
			if(circBounds){
				Vector2 posP = player.transform.position;
				Vector2 posG = transform.position;
				float radius = circBounds.radius * transform.localScale.x;
				
				if((posP - posG).sqrMagnitude > radius * radius){
					//m_bPlayerIsNear = false;
					player.HidePasscode();
					player.BecomeFarTo(gameObject);
				}
			}
			BoxCollider2D boxBounds = GetComponent<BoxCollider2D>();
			if(boxBounds){
				if(boxBounds.bounds.Contains(transform.position)){
					player.HidePasscode();
					player.BecomeFarTo(gameObject);
				}
			}
		}
	}

}
