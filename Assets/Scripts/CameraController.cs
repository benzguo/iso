using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

		public GameObject player;
		public float gRotationSpeed = 6.0f;
		
		Vector3 POSITION_BACK       = new Vector3 (0.0f, 0.0f, -10.0f);
		Quaternion ROTATION_BACK    = Quaternion.Euler (0.0f, 0.0f, 0.0f);
		Vector3 POSITION_DOWN_X     = new Vector3 (0.0f, -10.0f, 0.0f);				// LR
		Quaternion ROTATION_DOWN_X  = Quaternion.Euler (-90.0f, 90.0f, 90.0f);
		Vector3 POSITION_DOWN_Z     = new Vector3 (0.0f, -10.0f, 0.0f);				// BF
		Quaternion ROTATION_DOWN_Z  = Quaternion.Euler (-90.0f, 0.0f, 0.0f);	
		Vector3 POSITION_FORWARD    = new Vector3 (0.0f, 0.0f, 10.0f);
		Quaternion ROTATION_FORWARD = Quaternion.Euler (0.0f, 180.0f, 180.0f);	
		Vector3 POSITION_LEFT_Y     = new Vector3 (-10.0f, 0.0f, 0.0f);				// DU
		Quaternion ROTATION_LEFT_Y  = Quaternion.Euler (0.0f, 90.0f, 90.0f);
		Vector3 POSITION_LEFT_Z     = new Vector3 (-10.0f, 0.0f, 0.0f);				// BF
		Quaternion ROTATION_LEFT_Z  = Quaternion.Euler (0.0f, 0.0f, 0.0f);	
		Vector3 POSITION_RIGHT      = new Vector3 (10.0f, 0.0f, 0.0f);
		Quaternion ROTATION_RIGHT   = Quaternion.Euler (0.0f, -90.0f, -90.0f);
		Vector3 POSITION_UP         = new Vector3 (0.0f, 10.0f, 0.0f);
		Quaternion ROTATION_UP      = Quaternion.Euler (90.0f, 0.0f, 0.0f);

		Vector3 targetPosition;
		Vector3 normal;
		Quaternion targetRotation;

		void Start ()
		{
				normal = Vector3.up;
				targetPosition = POSITION_UP;
				targetRotation = ROTATION_UP;
		}
	
		void LateUpdate ()
		{
//		if (transform.position != targetPosition) {
				transform.position = Vector3.Lerp (transform.position, 
											   player.transform.position + targetPosition, 
											   gRotationSpeed * Time.deltaTime);
//		}
				if (transform.rotation != targetRotation) {
						transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, gRotationSpeed * Time.deltaTime);	
				}
		}
	
		public void RotateBack ()
		{
				targetPosition = POSITION_BACK;
				targetRotation = ROTATION_BACK;
				normal = Vector3.back;
		}
		
		public void RotateDown ()
		{
				if (normal == Vector3.left || normal == Vector3.right) {
						targetPosition = POSITION_DOWN_X;
						targetRotation = ROTATION_DOWN_X;
						normal = Vector3.down;
				}
				else if (normal == Vector3.back || normal == Vector3.forward) {
						targetPosition = POSITION_DOWN_Z;
						targetRotation = ROTATION_DOWN_Z;
						normal = Vector3.down;
				}
		}
		
		public void RotateForward ()
		{
				targetPosition = POSITION_FORWARD;
				targetRotation = ROTATION_FORWARD;
				normal = Vector3.forward;
		}
	
		public void RotateLeft ()
		{
				if (normal == Vector3.down || normal == Vector3.up) {
						targetPosition = POSITION_LEFT_Y;
						targetRotation = ROTATION_LEFT_Y;
						normal = Vector3.left;
				}
				else if (normal == Vector3.back || normal == Vector3.forward) {
						targetPosition = POSITION_LEFT_Z;
						targetRotation = ROTATION_LEFT_Z;
						normal = Vector3.left;
				}
		}
	
		public void RotateUp ()
		{
				targetPosition = POSITION_UP;
				targetRotation = ROTATION_UP;
				normal = Vector3.up;
		}
	
		public void RotateRight ()
		{
				targetPosition = POSITION_RIGHT;
				targetRotation = ROTATION_RIGHT;
				normal = Vector3.right;
		}
}
