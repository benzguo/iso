using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

		public GameObject player;
		public float gRotationSpeed = 6.0f;
		Vector3 POSITION_DOWN = new Vector3 (0.0f, -10.0f, 0.0f);
		Quaternion ROTATION_DOWN = Quaternion.Euler (-90.0f, 90.0f, 90.0f);
		Vector3 POSITION_LEFT = new Vector3 (-10.0f, 0.0f, 0.0f);
		Quaternion ROTATION_LEFT = Quaternion.Euler (0.0f, 90.0f, 90.0f);
		Vector3 POSITION_RIGHT = new Vector3 (10.0f, 0.0f, 0.0f);
		Quaternion ROTATION_RIGHT = Quaternion.Euler (0.0f, -90.0f, -90.0f);
		Vector3 POSITION_UP = new Vector3 (0.0f, 10.0f, 0.0f);
		Quaternion ROTATION_UP = Quaternion.Euler (90.0f, 0.0f, 0.0f);
		Vector3 targetPosition;
		Quaternion targetRotation;

		void Start ()
		{
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
	
		public void RotateDown ()
		{
				targetPosition = POSITION_DOWN;
				targetRotation = ROTATION_DOWN;
		}
	
		public void RotateLeft ()
		{
				targetPosition = POSITION_LEFT;
				targetRotation = ROTATION_LEFT;
		}
	
		public void RotateUp ()
		{
				targetPosition = POSITION_UP;
				targetRotation = ROTATION_UP;
		}
	
		public void RotateRight ()
		{
				targetPosition = POSITION_RIGHT;
				targetRotation = ROTATION_RIGHT;
		}
}
