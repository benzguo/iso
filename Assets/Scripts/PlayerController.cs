using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public GameObject mainCamera;
	public float speed = 10.0f;
	
	float cameraHeight;
	Vector3 targetPos;
	
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
				targetPos = roundedVec(transform.position + cardinalize(rMousePos));
			}
		}
		
		Vector3 targetDir = cardinalize (targetPos - transform.position);
		bool shouldMove = !Physics.Raycast (transform.position, targetDir, 0.5f);
		if (shouldMove) {
			transform.position = Vector3.MoveTowards (transform.position, targetPos, speed * Time.deltaTime);
		}
	}

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
