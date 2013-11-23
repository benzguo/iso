using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public GameObject gMainCamera;
	public float gSpeed = 1.0f;
	public Vector3 gNormal;

	Vector3 targetPos;
	bool atTargetPos;

	void Start() {
		gNormal = Vector3.down;
		targetPos = transform.position;
		atTargetPos = true;
	}

	void Update() {
		// get the target position from the mouse (or touch) position
		if (Input.GetMouseButton(0)) {
			Vector3 rMousePos = relativeMousePosition();
				
			if (Vector3.Distance(rMousePos, Vector3.zero) > 1 && atTargetPos)
			{
				targetPos = intVector3(transform.position + vecToGridPlane(rMousePos));
				atTargetPos = false;
			}
		}

		Vector3 targetDir = vecToGridPlane(targetPos - transform.position);
		bool shouldMove = !Physics.Raycast(transform.position + targetDir, targetDir, 0.5f);
		if (shouldMove) {
			Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, gSpeed * Time.deltaTime);
			transform.position = newPos;
			atTargetPos = newPos == targetPos;
		}
	}

	Vector3 relativeMousePosition() {
		// NOTE: assuming the camera starts directly above the player
		float cameraHeight = Mathf.Abs(Vector3.Distance(gMainCamera.transform.position, transform.position));
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = cameraHeight;
		return Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
	}

	Vector3 vecToGridPlane(Vector3 vector) {
		Vector3 v = Vector3.zero;
		float ax = Mathf.Abs(vector.x);
		float ay = Mathf.Abs(vector.y);
		float az = Mathf.Abs(vector.z);
		
		if (gNormal == Vector3.down || gNormal == Vector3.up) {	
			if (ax > az) {
				v.x = vector.x/ax;
			} else {
				v.z = vector.z/az;
			}
		}
		else if (gNormal == Vector3.left || gNormal == Vector3.right) {
			if (ay > az) {
				v.y = vector.y/ax;
			} else {
				v.z = vector.z/az;
			}
		}
		else if (gNormal == Vector3.forward || gNormal == Vector3.back) {
			if (ax > ay) {
				v.x = vector.x/ax;
			} else {
				v.y = vector.y/ay;
			}	
		}
		return v;
	}

	Vector3 intVector3(Vector3 v) {
		int x = Mathf.RoundToInt(v.x);
		int y = Mathf.RoundToInt(v.y);
		int z = Mathf.RoundToInt(v.z);
		if (gNormal == Vector3.down || gNormal == Vector3.up) {	
			y = 0;
		}
		else if (gNormal == Vector3.left || gNormal == Vector3.right) {
			x = 0;		
		}
		else if (gNormal == Vector3.forward || gNormal == Vector3.back) {
			z = 0;
		}
		return new Vector3 (x, y, z);
	}

}
