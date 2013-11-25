using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	Vector3 POSITION_LEFT = new Vector3(-10.0f, 0.0f, 0.0f); 
	Quaternion ROTATION_LEFT = Quaternion.Euler(0.0f, 90.0f, 0.0f);

	Vector3 POSITION_UP = new Vector3(0.0f, 10.0f, 0.0f);
	Quaternion ROTATION_UP = Quaternion.Euler(90.0f, 0.0f, 0.0f);

	Vector3 targetPosition;
	Quaternion targetRotation;

	void Start () {
		targetPosition = POSITION_UP;
		targetRotation = ROTATION_UP;
	}
	
	void LateUpdate () {
		transform.position = player.transform.position + targetPosition;
//		transform.position = targetPosition;
		transform.rotation = targetRotation;	
	}
	
	public void RotateLeft () {
		targetPosition = POSITION_LEFT;
		targetRotation = ROTATION_LEFT;
	}
	
	public void RotateUp () {
		targetPosition = POSITION_UP;
		targetRotation = ROTATION_UP;
	}
}
