using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // The camera following the player.
    public Camera gFollowingCamera;
    // The player's current speed.
    public float gSpeed = 6.0f;
    // The player's current normal vector.
    public Vector3 gNormal;

    // The player's current plane.
    public Plane gPlane;
    const string kRotatorBackTag = "RotatorBack";
    const string kRotatorDownTag = "RotatorDown";
    const string kRotatorForwardTag = "RotatorForward";
    const string kRotatorLeftTag = "RotatorLeft";
    const string kRotatorRightTag = "RotatorRight";
    const string kRotatorUpTag = "RotatorUp";
    Vector3 targetPos;
    GameObject ground;
    CameraController cameraController;

    void Start()
    {
        gNormal = Vector3.up;
        gPlane = new Plane (gNormal, transform.position);
        targetPos = transform.position;
        cameraController = gFollowingCamera.GetComponent<CameraController> ();
    }

    void Update()
    {
        Vector3 position = transform.position;
        bool atTargetPosition = position == targetPos;
        if (atTargetPosition) {
            RaycastHit hit = default(RaycastHit);
            Debug.Log(hit);
            Vector3 hitPoint = default(Vector3);
            Physics.Raycast(position, -gNormal, out hit);
            if (hit.collider) {
                ground = hit.collider.gameObject;
                Debug.Log(ground);
                /* cameraController.Rotate(ground.tag); */
                hitPoint = hit.point;
            }

            if (Input.GetKeyDown("w")) {
                cameraController.RotateWithKey("w");
            }
            else if (Input.GetKeyDown("a")) {
                cameraController.RotateWithKey("a");
            }
            else if (Input.GetKeyDown("s")) {
                cameraController.RotateWithKey("s");
            }
            else if (Input.GetKeyDown("d")) {
                cameraController.RotateWithKey("d");
            }

            // tap on the cube to push the cube down to the plane below
            else if (Input.GetMouseButtonDown(0)) {
                Vector3 rMousePos = relativeMousePosition();

                bool mouseOnPlayer = Vector3.Distance(rMousePos, Vector3.zero) < 1;
                if (mouseOnPlayer && hit.collider) {
                    Plane groundPlane = new Plane(gNormal, hitPoint);
                    int distanceToGround = (int)Mathf.Floor(groundPlane.GetDistanceToPoint(transform.position) + 0.5f);
                    if (distanceToGround > 0) {
                        targetPos = transform.position - gNormal*distanceToGround + gNormal;
                    }
                }
            }

            // tap next to the cube to move it
            else if (Input.GetMouseButton(0)) {
                Vector3 rMousePos = relativeMousePosition();
                Vector3 newTargetPos = toPosition(position + toGrid (rMousePos));
                Vector3 targetDir = toGrid(newTargetPos - position);

                bool mouseOnPlayer = Vector3.Distance(rMousePos, Vector3.zero) < 1;
                // Update target position if
                // - you've reached the target position and
                // - the touch isn't directly over you
                bool shouldUpdateTargetPosition = atTargetPosition && !mouseOnPlayer;

                if (shouldUpdateTargetPosition) {
                    bool colliderAtTarget = Physics.Raycast(position, targetDir, 1.0f);
                    bool voidAtTarget = colliderAtTarget ? false : !Physics.Raycast(newTargetPos, -gNormal, 2.0f);

                    bool stepAboveTarget = false;
                    if (colliderAtTarget) {
                        stepAboveTarget = Physics.Raycast(newTargetPos + gNormal, -gNormal, 0.5f);
                        /* && !Physics.Raycast(position + groundVector, -gNormal, 1.0f) */
                        /* && !Physics.Raycast(position + gNormal, targetDir, 1.5f); */
                    }

                    bool stepBelowTarget = false;
                    if (!colliderAtTarget) {
                        stepBelowTarget = Physics.Raycast(newTargetPos - gNormal, -gNormal, 1.0f)
                            && !Physics.Raycast(newTargetPos, -gNormal, 0.5f);
                    }

                    if (!colliderAtTarget && !voidAtTarget) {
                        // Step down.
                        if (stepBelowTarget) {
                            newTargetPos -= gNormal;
                            gPlane.SetNormalAndPosition (newTargetPos, gNormal);
                            targetPos = newTargetPos;
                            // Step forward.
                        } else {
                            targetPos = newTargetPos;
                        }
                    }
                    // Step up.
                    else if (colliderAtTarget && stepAboveTarget) {
                        newTargetPos += gNormal;
                        gPlane.SetNormalAndPosition (newTargetPos, gNormal);
                        targetPos = newTargetPos;
                    }
                }

            }
        }
        transform.position = stepTowards(position, targetPos, gSpeed * Time.deltaTime);
    }

    // Returns the vector moving the current vector towards the target vector.
    // If the target point is above the current point, this will will move it up and then along the new plane.
    // If the target point is below the current point, this will move it along the current plane and then down.
    Vector3 stepTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
    {
        Vector3 v = current;
        Plane currentPlane = new Plane(gNormal, current);
        float targetPlaneDelta = currentPlane.GetDistanceToPoint (target);
        Vector3 targetPlaneVector = new Vector3(gNormal.x * targetPlaneDelta,
                gNormal.y * targetPlaneDelta,
                gNormal.z * targetPlaneDelta);
        Vector3 targetInCurrentPlane = target - targetPlaneVector;
        Vector3 currentInTargetPlane = current + targetPlaneVector;
        if (targetPlaneDelta == 0) {
            v = Vector3.MoveTowards (current, target, maxDistanceDelta);
        } else if (targetPlaneDelta > 0) {
            v = Vector3.MoveTowards (current, currentInTargetPlane, maxDistanceDelta);
        } else {
            if (current != targetInCurrentPlane) {
                v = Vector3.MoveTowards (current, targetInCurrentPlane, maxDistanceDelta);
            } else {
                v = Vector3.MoveTowards (current, currentInTargetPlane, maxDistanceDelta);
            }
        }
        return v;
    }

    Vector3 relativeMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        // NOTE: assuming camera is directly above player
        float cameraHeight = Vector3.Distance(gFollowingCamera.transform.position, transform.position);
        mousePos.z = cameraHeight;
        return Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
    }

    // Converts a Vector3 to the grid
    Vector3 toGrid(Vector3 vector)
    {
        Vector3 v = Vector3.zero;
        float ax = Mathf.Abs(vector.x);
        float ay = Mathf.Abs(vector.y);
        float az = Mathf.Abs(vector.z);

        if (gNormal == Vector3.down || gNormal == Vector3.up) {
            if (ax > az) {
                v.x = vector.x / ax;
            } else {
                v.z = vector.z / az;
            }
        } else if (gNormal == Vector3.left || gNormal == Vector3.right) {
            if (ay > az) {
                v.y = vector.y / ay;
            } else {
                v.z = vector.z / az;
            }
        } else if (gNormal == Vector3.forward || gNormal == Vector3.back) {
            if (ax > ay) {
                v.x = vector.x / ax;
            } else {
                v.y = vector.y / ay;
            }
        }
        return v;
    }

    // Rounds a Vector3 to a valid position
    Vector3 toPosition(Vector3 v)
    {
        int x = Mathf.RoundToInt(v.x);
        int y = Mathf.RoundToInt(v.y);
        int z = Mathf.RoundToInt(v.z);
        return new Vector3(x, y, z);
    }

}
