using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
		// Camera position + screen up direction
		// Back = Screen out
		enum Orientations {
				UpForward,
				UpRight,
				UpBack,
				UpLeft,
				ForwardUp,
				ForwardRight,
				ForwardDown,
				ForwardLeft,
				DownBack,
				DownRight,
				DownForward,
				DownLeft,
				BackUp,
				BackLeft,
				BackDown,
				BackRight,
				LeftUp,
				LeftForward,
				LeftDown,
				LeftBack,
				RightUp,
				RightBack,
				RightDown,
				RightForward		
		};
		
		Vector3 POSITION_UP = new Vector3 (0.0f, 10.0f, 0.0f);
		Vector3 POSITION_FORWARD = new Vector3 (0.0f, 0.0f, 10.0f);
		Vector3 POSITION_DOWN = new Vector3 (0.0f, -10.0f, 0.0f);
		Vector3 POSITION_BACK = new Vector3 (0.0f, 0.0f, -10.0f);
		Vector3 POSITION_LEFT = new Vector3 (-10.0f, 0.0f, 0.0f);
		Vector3 POSITION_RIGHT = new Vector3 (10.0f, 0.0f, 0.0f);
		
		Quaternion ROTATION_UP_F = Quaternion.Euler (90.0f, 0.0f, 0.0f);
		Quaternion ROTATION_UP_L = Quaternion.Euler (90.0f, 0.0f, 90.0f);
		Quaternion ROTATION_UP_B = Quaternion.Euler (90.0f, 0.0f, 180.0f);
		Quaternion ROTATION_UP_R = Quaternion.Euler (90.0f, 0.0f, 270.0f);
		
		Quaternion ROTATION_FORWARD_U = Quaternion.Euler (0.0f, 180.0f, 0.0f);
		Quaternion ROTATION_FORWARD_R = Quaternion.Euler (0.0f, 180.0f, 90.0f);
		Quaternion ROTATION_FORWARD_D = Quaternion.Euler (0.0f, 180.0f, 180.0f);
		Quaternion ROTATION_FORWARD_L = Quaternion.Euler (0.0f, 180.0f, 270.0f);
		
		Quaternion ROTATION_DOWN_B = Quaternion.Euler (-90.0f, 0.0f, 0.0f);
		Quaternion ROTATION_DOWN_L = Quaternion.Euler (-90.0f, 90.0f, 0.0f);	
		Quaternion ROTATION_DOWN_F = Quaternion.Euler (-90.0f, 180.0f, 0.0f);	
		Quaternion ROTATION_DOWN_R = Quaternion.Euler (-90.0f, 270.0f, 0.0f);	
		
		Quaternion ROTATION_BACK_U = Quaternion.Euler (0.0f, 0.0f, 0.0f);
		Quaternion ROTATION_BACK_L = Quaternion.Euler (0.0f, 0.0f, 90.0f);	
		Quaternion ROTATION_BACK_D = Quaternion.Euler (0.0f, 0.0f, 180.0f);	
		Quaternion ROTATION_BACK_R = Quaternion.Euler (0.0f, 0.0f, 270.0f);	
		
		Quaternion ROTATION_LEFT_U = Quaternion.Euler (0.0f, 90.0f, 0.0f);
		Quaternion ROTATION_LEFT_F = Quaternion.Euler (0.0f, 90.0f, 90.0f);
		Quaternion ROTATION_LEFT_D = Quaternion.Euler (0.0f, 90.0f, 180.0f);
		Quaternion ROTATION_LEFT_B = Quaternion.Euler (0.0f, 90.0f, 270.0f);
		
		Quaternion ROTATION_RIGHT_U = Quaternion.Euler (0.0f, 270.0f, 0.0f);
		Quaternion ROTATION_RIGHT_B = Quaternion.Euler (0.0f, 270.0f, 90.0f);
		Quaternion ROTATION_RIGHT_D = Quaternion.Euler (0.0f, 270.0f, 180.0f);
		Quaternion ROTATION_RIGHT_F = Quaternion.Euler (0.0f, 270.0f, 270.0f);
		
		public GameObject player;
		public float gRotationSpeed = 6.0f;
		
		Vector3 targetPosition;
		Vector3 normal;
		Orientations orientation;
		Quaternion targetRotation;

		void Start ()
		{
				orientation = Orientations.UpForward;
				targetPosition = POSITION_UP;
				targetRotation = ROTATION_UP_F;
		}
	
		void LateUpdate ()
		{
				transform.position = Vector3.Lerp (transform.position, 
											   player.transform.position + targetPosition, 
											   gRotationSpeed * Time.deltaTime);
				if (transform.rotation != targetRotation) {
						transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, gRotationSpeed * Time.deltaTime);	
				}
		}
	
		public void RotateUp ()
		{
				if (orientation == Orientations.LeftForward 
				 || orientation == Orientations.RightForward
				 || orientation == Orientations.BackUp 
				 || orientation == Orientations.ForwardDown) {
						targetRotation = ROTATION_UP_F;
						orientation = Orientations.UpForward;	
				}
				else if (orientation == Orientations.LeftBack
	  		          || orientation == Orientations.RightBack
	  		          || orientation == Orientations.BackDown
	  		          || orientation == Orientations.ForwardUp) {
	  		    		targetRotation = ROTATION_UP_B;
	  		    		orientation = Orientations.UpBack;      
  		        }
				else if (orientation == Orientations.ForwardLeft
					  || orientation == Orientations.BackLeft 
					  || orientation == Orientations.LeftUp
					  || orientation == Orientations.RightDown) {
						targetRotation = ROTATION_UP_L;
						orientation = Orientations.UpLeft;	  
			  	}
			  	else if (orientation == Orientations.ForwardRight
			  		  || orientation == Orientations.BackRight
			  		  || orientation == Orientations.RightUp
			  		  || orientation == Orientations.RightDown) {
			  			targetRotation = ROTATION_UP_R;
			  			orientation = Orientations.UpRight;	  
	  		    }
  		        else {
  		        		return;
  		        }
				targetPosition = POSITION_UP;	
		}
		
		public void RotateForward ()
		{
				if (orientation == Orientations.UpForward
				 || orientation == Orientations.RightForward
				 || orientation == Orientations.LeftForward
				 || orientation == Orientations.DownBack) {
						targetRotation = ROTATION_FORWARD_D;
						orientation = Orientations.ForwardDown; 
				}
				else if (orientation == Orientations.UpBack
				      || orientation == Orientations.RightBack
				      || orientation == Orientations.LeftBack
				      || orientation == Orientations.DownBack) {
				      	targetRotation = ROTATION_FORWARD_U;
				      	orientation = Orientations.ForwardUp;
		        }
				else if (orientation == Orientations.UpLeft
				      || orientation == Orientations.RightForward
				      || orientation == Orientations.DownLeft
				      || orientation == Orientations.LeftBack) {
						targetRotation = ROTATION_FORWARD_L;
						orientation = Orientations.ForwardLeft;      
			    }
			    else if (orientation == Orientations.UpRight
			          || orientation == Orientations.RightBack
			          || orientation == Orientations.DownRight
			          || orientation == Orientations.LeftForward) {
			    		targetRotation = ROTATION_FORWARD_R;
			    		orientation = Orientations.ForwardRight;      
	            }
	            else {
	            		return;
	            }
	            targetPosition = POSITION_FORWARD;
		}
		
		public void RotateDown ()
		{

		}	
		
		public void RotateBack ()
		{

		}
			
		public void RotateLeft ()
		{

		}
	
		public void RotateRight ()
		{

		}
}
