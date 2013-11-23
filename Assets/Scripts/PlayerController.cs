using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// The camera following the player.
	public GameObject gFollowingCamera;
	// The player's current speed.
	public float gSpeed = 6.0f;
	// The player's current normal vector.
	public Vector3 gNormal;
	// The player's current plane.
	public Plane gPlane;
	
	Vector3 targetPos;
	bool atTargetPos;

	void Start() {
		gNormal = Vector3.up;
		gPlane = new Plane(gNormal, transform.position);
		targetPos = transform.position;
	}

	void Update() {
		Vector3 position = transform.position;
		
		// get the target position from the mouse (or touch) position
		if (Input.GetMouseButton(0)) {
			Vector3 rMousePos = relativeMousePosition();
			Vector3 newTargetPos = intVector3(position + toGrid(rMousePos));
			Vector3 targetDir = toGrid(newTargetPos - position);
			
			// Update the target position if:
			// * the current position is the target position
			// * the mouse is not over the player
			// * there's no collider in front of the player 
			bool shouldUpdateTargetPosition = position == targetPos
				&& Vector3.Distance(rMousePos, Vector3.zero) > 1
				&& !Physics.Raycast(position, targetDir, 1.0f);
				
			if (shouldUpdateTargetPosition) {
				targetPos = newTargetPos;
			}
		}

		transform.position = Vector3.MoveTowards(position, targetPos, gSpeed * Time.deltaTime);
	}

	Vector3 relativeMousePosition() {
		float cameraHeight = gPlane.GetDistanceToPoint(gFollowingCamera.transform.position);
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = cameraHeight;
		return Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
	}

	// Converts a Vector3 to the grid on the current plane
	Vector3 toGrid(Vector3 vector) {
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
			y = Mathf.RoundToInt(gPlane.distance);
		}
		else if (gNormal == Vector3.left || gNormal == Vector3.right) {
			x = Mathf.RoundToInt(gPlane.distance);		
		}
		else if (gNormal == Vector3.forward || gNormal == Vector3.back) {
			z = Mathf.RoundToInt(gPlane.distance);
		}
		return new Vector3 (x, y, z);
	}

}
