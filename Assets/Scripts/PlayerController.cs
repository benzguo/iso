using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public GameObject mainCamera;
	public float speed = 10.0f;
	
	float cameraHeight;
	Vector3 targetPos;
	//	Vector3 previousPos;
	
	void Start () {
		cameraHeight = mainCamera.transform.position.y; 
	}
	
	void Update () {
		// get the target position from the mouse (or touch) position
		if (Input.GetMouseButton(0)) {
			var mousePos = Input.mousePosition;
			mousePos.z = cameraHeight;
			
			var rMousePos = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
			var cMousePos = cardinalize (rMousePos);
			if (Vector3.Distance(rMousePos, Vector3.zero) >= 1) 
			{
				//				previousPos = targetPos;
				targetPos = roundedVec(transform.position + cardinalize(rMousePos));
			}
			//			Debug.Log (targetPos);
		}
		
		Vector3 targetDir = cardinalize (targetPos - transform.position);
		bool shouldMove = !Physics.Raycast (targetPos, targetDir, 0.5f);
		if (shouldMove) {
			transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
		}
		Debug.Log (targetPos);
		
		
		//		transform.rotation = Quaternion.identity;
	}
	
	//	void OnTriggerEnter (Collider other)
	//	{
	//		var collisionPos = other.gameObject.transform.position;
	//		lastCollisionDir = cardinalize (collisionPos - transform.position);
	//		Debug.Log ("TRIGGER");
	//		targetPos = transform.position;
	//		transform.position = new Vector3 (transform.position.x, 0, transform.position.z);
	//		if (other.gameObject.tag == "Ground") {
	//		Debug.Log (lastCollisionDir);
	//		}
	//	}
	
	Vector3 cardinalize (Vector3 vector) {
		Vector3 cv = Vector3.zero;
		float ax = Mathf.Abs (vector.x);
		float az = Mathf.Abs (vector.z);
		if (ax > az) {
			cv.x = vector.x/ax;
		} else {
			cv.z = vector.z/az;
		}
		return cv;
	}
	
	// TODO learn about vector math
	// TODO move this somewhere shared
	Vector3 roundedVec (Vector3 v) {
		return new Vector3 (Mathf.RoundToInt (v.x),
		                    0,
		                    Mathf.RoundToInt (v.z));
	}
	
}
