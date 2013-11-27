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
		Vector3 POSITION_DOWN = new Vector3 (0.0f, -10.0f, 0.0f);
		Vector3 POSITION_FORWARD = new Vector3 (0.0f, 0.0f, 10.0f);
		Vector3 POSITION_BACK = new Vector3 (0.0f, 0.0f, -10.0f);
		Vector3 POSITION_LEFT = new Vector3 (-10.0f, 0.0f, 0.0f);
		Vector3 POSITION_RIGHT = new Vector3 (10.0f, 0.0f, 0.0f);
		
		Quaternion ROTATION_UP_F = Quaternion.Euler (90.0f, 0.0f, 0.0f);
		Quaternion ROTATION_UP_L = Quaternion.Euler (90.0f, 0.0f, 90.0f);
		Quaternion ROTATION_UP_B = Quaternion.Euler (90.0f, 0.0f, 180.0f);
		Quaternion ROTATION_UP_R = Quaternion.Euler (90.0f, 0.0f, 270.0f);
		
		Quaternion ROTATION_DOWN_B = Quaternion.Euler (-90.0f, 0.0f, 0.0f);
		Quaternion ROTATION_DOWN_L = Quaternion.Euler (-90.0f, 90.0f, 0.0f);	
		Quaternion ROTATION_DOWN_F = Quaternion.Euler (-90.0f, 180.0f, 0.0f);	
		Quaternion ROTATION_DOWN_R = Quaternion.Euler (-90.0f, 270.0f, 0.0f);	
		
		Quaternion ROTATION_FORWARD_U = Quaternion.Euler (0.0f, 180.0f, 0.0f);
		Quaternion ROTATION_FORWARD_R = Quaternion.Euler (0.0f, 180.0f, 90.0f);
		Quaternion ROTATION_FORWARD_D = Quaternion.Euler (0.0f, 180.0f, 180.0f);
		Quaternion ROTATION_FORWARD_L = Quaternion.Euler (0.0f, 180.0f, 270.0f);
		
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
				orientation = Orientations.UpLeft;
				targetPosition = POSITION_UP;
				targetRotation = ROTATION_UP_L;
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
					  || orientation == Orientations.RightUp
					  || orientation == Orientations.LeftDown) {
						targetRotation = ROTATION_UP_L;
						orientation = Orientations.UpLeft;	  
			  	}
			  	else if (orientation == Orientations.ForwardRight
			  		  || orientation == Orientations.BackRight
			  		  || orientation == Orientations.LeftUp
			  		  || orientation == Orientations.RightDown) {
			  			targetRotation = ROTATION_UP_R;
			  			orientation = Orientations.UpRight;	  
	  		    }
  		        else {
  		        		return;
  		        }
				targetPosition = POSITION_UP;	
		}
		
		public void RotateDown ()
		{
				if (orientation == Orientations.LeftBack
				 || orientation == Orientations.RightBack
				 || orientation == Orientations.BackUp
				 || orientation == Orientations.ForwardDown) {
						targetRotation = ROTATION_DOWN_B;
						orientation = Orientations.DownBack; 
				}
				else if (orientation == Orientations.LeftForward
					  || orientation == Orientations.RightForward
					  || orientation == Orientations.BackDown
					  || orientation == Orientations.ForwardUp) {
					  	targetRotation = ROTATION_DOWN_F;
					  	orientation = Orientations.DownForward;
			  	}
			  	else if (orientation == Orientations.ForwardLeft
			  	      || orientation == Orientations.BackLeft
			  	      || orientation == Orientations.LeftUp
			  	      || orientation == Orientations.RightDown) {
						targetRotation = ROTATION_DOWN_L;
						orientation = Orientations.DownLeft;
				}
				else if (orientation == Orientations.ForwardRight
					  || orientation == Orientations.BackRight
					  || orientation == Orientations.RightUp
					  || orientation == Orientations.LeftDown) {
						targetRotation = ROTATION_DOWN_R;
						orientation = Orientations.DownRight;	  
				}
				else {
						return;
				}
				targetPosition = POSITION_DOWN;
		}	
		
		public void RotateForward ()
		{
				if (orientation == Orientations.UpForward
				 || orientation == Orientations.RightDown
				 || orientation == Orientations.LeftDown
				 || orientation == Orientations.DownBack) {
						targetRotation = ROTATION_FORWARD_D;
						orientation = Orientations.ForwardDown; 
				}
				else if (orientation == Orientations.UpBack
				      || orientation == Orientations.RightUp
				      || orientation == Orientations.LeftUp
				      || orientation == Orientations.DownForward) {
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
			          || orientation == Orientations.LeftForward
			          || orientation == Orientations.DownRight
			          || orientation == Orientations.RightBack) {
			    		targetRotation = ROTATION_FORWARD_R;
			    		orientation = Orientations.ForwardRight;      
	            }
	            else {
	            		return;
	            }
	            targetPosition = POSITION_FORWARD;
		}
	
		public void RotateBack ()
		{
				if (orientation == Orientations.UpForward
				 || orientation == Orientations.RightUp
				 || orientation == Orientations.LeftUp
				 || orientation == Orientations.DownBack) {
						targetRotation = ROTATION_BACK_U;
						orientation = Orientations.BackUp; 
				}
				else if (orientation == Orientations.UpBack
				      || orientation == Orientations.RightDown
				      || orientation == Orientations.LeftDown
				      || orientation == Orientations.DownForward) {
				      	targetRotation = ROTATION_BACK_D;
				      	orientation = Orientations.BackDown;
		        }
				else if (orientation == Orientations.UpLeft
				      || orientation == Orientations.LeftForward
				      || orientation == Orientations.DownLeft
				      || orientation == Orientations.RightBack) {
						targetRotation = ROTATION_BACK_L;
						orientation = Orientations.BackLeft;      
			    }
			    else if (orientation == Orientations.UpRight
			          || orientation == Orientations.RightForward
			          || orientation == Orientations.DownRight
			          || orientation == Orientations.LeftBack) {
			    		targetRotation = ROTATION_BACK_R;
			    		orientation = Orientations.BackRight;      
	            }
	            else {
	            		return;
	            }
	            targetPosition = POSITION_BACK;		
    	}
	            
			
		public void RotateLeft ()
		{
				if (orientation == Orientations.UpForward
				 || orientation == Orientations.DownForward
				 || orientation == Orientations.BackLeft
				 || orientation == Orientations.ForwardRight) {
						targetRotation = ROTATION_LEFT_F;
						orientation = Orientations.LeftForward; 
				}
				else if (orientation == Orientations.UpBack
				      || orientation == Orientations.DownBack
				      || orientation == Orientations.BackRight
				      || orientation == Orientations.ForwardLeft) {
				      	targetRotation = ROTATION_LEFT_B;
				      	orientation = Orientations.LeftBack;
		      	}
		      	else if (orientation == Orientations.BackUp
		      		  || orientation == Orientations.ForwardUp
		      		  || orientation == Orientations.UpRight
		      		  || orientation == Orientations.DownLeft) {
		      		  	targetRotation = ROTATION_LEFT_U;
		      		  	orientation = Orientations.LeftUp;
      		  	}
      		  	else if (orientation == Orientations.BackDown
      		  		  || orientation == Orientations.ForwardDown
      		  		  || orientation == Orientations.UpLeft
      		  		  || orientation == Orientations.DownRight) {
      		  		  	targetRotation = ROTATION_LEFT_D;
      		  		  	orientation = Orientations.LeftDown;
	  		  	}
				else {
						return;
				}
				targetPosition = POSITION_LEFT;
		}
	
		public void RotateRight ()
		{
				if (orientation == Orientations.UpForward
				 || orientation == Orientations.DownForward
			     || orientation == Orientations.BackRight
			     || orientation == Orientations.ForwardLeft) {
						targetRotation = ROTATION_RIGHT_F;
						orientation = Orientations.RightForward; 
				}
				else if (orientation == Orientations.UpBack
			          || orientation == Orientations.DownBack
			          || orientation == Orientations.BackLeft
			          || orientation == Orientations.ForwardRight) {
						targetRotation = ROTATION_RIGHT_B;
						orientation = Orientations.RightBack;
				}
				else if (orientation == Orientations.BackUp
			          || orientation == Orientations.ForwardUp
			          || orientation == Orientations.UpLeft
		    	      || orientation == Orientations.DownRight) {
						targetRotation = ROTATION_RIGHT_U;
						orientation = Orientations.RightUp;
				}
				else if (orientation == Orientations.BackDown
    			      || orientation == Orientations.ForwardDown
				      || orientation == Orientations.UpRight
				      || orientation == Orientations.DownLeft) {
						targetRotation = ROTATION_RIGHT_D;
						orientation = Orientations.RightDown;
				}
				else {
						return;
				}
				targetPosition = POSITION_RIGHT;

		}
}
