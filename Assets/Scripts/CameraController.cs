using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    // Camera position + screen up direction
    // Back = Screen out
    public enum Orientations {
        UpForward,
        UpLeft,
        UpBack,
        UpRight,
        DownBack,
        DownLeft,
        DownForward,
        DownRight,
        ForwardUp,
        ForwardRight,
        ForwardDown,
        ForwardLeft,
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

    static float height = 10.0f;
    Vector3 POSITION_UP = new Vector3 (0.0f, height, 0.0f);
    Vector3 POSITION_DOWN = new Vector3 (0.0f, -height, 0.0f);
    Vector3 POSITION_FORWARD = new Vector3 (0.0f, 0.0f, height);
    Vector3 POSITION_BACK = new Vector3 (0.0f, 0.0f, -height);
    Vector3 POSITION_LEFT = new Vector3 (-height, 0.0f, 0.0f);
    Vector3 POSITION_RIGHT = new Vector3 (height, 0.0f, 0.0f);

    Quaternion[] rotations = {
        Quaternion.Euler (90.0f, 0.0f, 0.0f),        // UpForward
        Quaternion.Euler (90.0f, 0.0f, 90.0f),       // UpLeft
        Quaternion.Euler (90.0f, 0.0f, 180.0f),      // UpBack
        Quaternion.Euler (90.0f, 0.0f, 270.0f),      // UpRight

        Quaternion.Euler (-90.0f, 0.0f, 0.0f),       // DownBack
        Quaternion.Euler (-90.0f, 90.0f, 0.0f),     // DownLeft
        Quaternion.Euler (-90.0f, 180.0f, 0.0f),   // DownForward
        Quaternion.Euler (-90.0f, 270.0f, 0.0f),   // DownRight

        Quaternion.Euler (0.0f, 180.0f, 0.0f),      // ForwardUp
        Quaternion.Euler (0.0f, 180.0f, 90.0f), // ForwardRight
        Quaternion.Euler (0.0f, 180.0f, 180.0f),// ForwardDown
        Quaternion.Euler (0.0f, 180.0f, 270.0f),// ForwardLeft

        Quaternion.Euler (0.0f, 0.0f, 0.0f),       // BackUp
        Quaternion.Euler (0.0f, 0.0f, 90.0f),      // BackLeft
        Quaternion.Euler (0.0f, 0.0f, 180.0f),     // BackDown
        Quaternion.Euler (0.0f, 0.0f, 270.0f),     // BackRight

        Quaternion.Euler (0.0f, 90.0f, 0.0f),      // LeftUp
        Quaternion.Euler (0.0f, 90.0f, 90.0f),     // LeftForward
        Quaternion.Euler (0.0f, 90.0f, 180.0f),    // LeftDown
        Quaternion.Euler (0.0f, 90.0f, 270.0f),    // LeftBack

        Quaternion.Euler (0.0f, 270.0f, 0.0f),    // RightUp
        Quaternion.Euler (0.0f, 270.0f, 90.0f),   // RightBack
        Quaternion.Euler (0.0f, 270.0f, 180.0f),  // RightDown
        Quaternion.Euler (0.0f, 270.0f, 270.0f)  // RightForward
    };

    public GameObject player;
    public float gRotationSpeed;
    public Orientations startOrientation;

    Vector3 targetPosition;
    Vector3 normal;
    Orientations orientation;
    Quaternion targetRotation;

    void Start ()
    {
        SetOrientation(startOrientation);
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

    void SetOrientation (Orientations o)
    {
        orientation = o;
        targetRotation = rotations[(int)o];
        if (o < Orientations.DownBack) {
            targetPosition = POSITION_UP;
        }
        else if (o >= Orientations.DownBack && o < Orientations.ForwardUp) {
            targetPosition = POSITION_DOWN;
        }
        else if (o >= Orientations.ForwardUp && o < Orientations.BackUp) {
            targetPosition = POSITION_FORWARD;
        }
        else if (o >= Orientations.BackUp && o < Orientations.LeftUp) {
            targetPosition = POSITION_BACK;
        }
        else if (o >= Orientations.LeftUp && o < Orientations.RightUp) {
            targetPosition = POSITION_LEFT;
        }
        else if (o >= Orientations.RightUp) {
            targetPosition = POSITION_RIGHT;
        }
    }

    public void RotateUp ()
    {
        if (orientation == Orientations.LeftForward
                || orientation == Orientations.RightForward
                || orientation == Orientations.BackUp
                || orientation == Orientations.ForwardDown) {
            SetOrientation(Orientations.UpForward);
        }
        else if (orientation == Orientations.LeftBack
                || orientation == Orientations.RightBack
                || orientation == Orientations.BackDown
                || orientation == Orientations.ForwardUp) {
            SetOrientation(Orientations.UpBack);
        }
        else if (orientation == Orientations.ForwardLeft
                || orientation == Orientations.BackLeft
                || orientation == Orientations.RightUp
                || orientation == Orientations.LeftDown) {
            SetOrientation(Orientations.UpLeft);
        }
        else if (orientation == Orientations.ForwardRight
                || orientation == Orientations.BackRight
                || orientation == Orientations.LeftUp
                || orientation == Orientations.RightDown) {
            SetOrientation(Orientations.UpRight);
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
            SetOrientation(Orientations.DownBack);
        }
        else if (orientation == Orientations.LeftForward
                || orientation == Orientations.RightForward
                || orientation == Orientations.BackDown
                || orientation == Orientations.ForwardUp) {
            SetOrientation(Orientations.DownForward);
        }
        else if (orientation == Orientations.ForwardLeft
                || orientation == Orientations.BackLeft
                || orientation == Orientations.LeftUp
                || orientation == Orientations.RightDown) {
            SetOrientation(Orientations.DownLeft);
        }
        else if (orientation == Orientations.ForwardRight
                || orientation == Orientations.BackRight
                || orientation == Orientations.RightUp
                || orientation == Orientations.LeftDown) {
            SetOrientation(Orientations.DownRight);
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
            SetOrientation(Orientations.ForwardDown);
        }
        else if (orientation == Orientations.UpBack
                || orientation == Orientations.RightUp
                || orientation == Orientations.LeftUp
                || orientation == Orientations.DownForward) {
            SetOrientation(Orientations.ForwardUp);
        }
        else if (orientation == Orientations.UpLeft
                || orientation == Orientations.RightForward
                || orientation == Orientations.DownLeft
                || orientation == Orientations.LeftBack) {
            SetOrientation(Orientations.ForwardLeft);
        }
        else if (orientation == Orientations.UpRight
                || orientation == Orientations.LeftForward
                || orientation == Orientations.DownRight
                || orientation == Orientations.RightBack) {
            SetOrientation(Orientations.ForwardRight);
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
            SetOrientation(Orientations.BackUp);
        }
        else if (orientation == Orientations.UpBack
                || orientation == Orientations.RightDown
                || orientation == Orientations.LeftDown
                || orientation == Orientations.DownForward) {
            SetOrientation(Orientations.BackDown);
        }
        else if (orientation == Orientations.UpLeft
                || orientation == Orientations.LeftForward
                || orientation == Orientations.DownLeft
                || orientation == Orientations.RightBack) {
            SetOrientation(Orientations.BackLeft);
        }
        else if (orientation == Orientations.UpRight
                || orientation == Orientations.RightForward
                || orientation == Orientations.DownRight
                || orientation == Orientations.LeftBack) {
            SetOrientation(Orientations.BackRight);
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
            SetOrientation(Orientations.LeftForward);
        }
        else if (orientation == Orientations.UpBack
                || orientation == Orientations.DownBack
                || orientation == Orientations.BackRight
                || orientation == Orientations.ForwardLeft) {
            SetOrientation(Orientations.LeftBack);
        }
        else if (orientation == Orientations.BackUp
                || orientation == Orientations.ForwardUp
                || orientation == Orientations.UpRight
                || orientation == Orientations.DownLeft) {
            SetOrientation(Orientations.LeftUp);
        }
        else if (orientation == Orientations.BackDown
                || orientation == Orientations.ForwardDown
                || orientation == Orientations.UpLeft
                || orientation == Orientations.DownRight) {
            SetOrientation(Orientations.LeftDown);
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
            SetOrientation(Orientations.RightForward);
        }
        else if (orientation == Orientations.UpBack
                || orientation == Orientations.DownBack
                || orientation == Orientations.BackLeft
                || orientation == Orientations.ForwardRight) {
            SetOrientation(Orientations.RightBack);
        }
        else if (orientation == Orientations.BackUp
                || orientation == Orientations.ForwardUp
                || orientation == Orientations.UpLeft
                || orientation == Orientations.DownRight) {
            SetOrientation(Orientations.RightUp);
        }
        else if (orientation == Orientations.BackDown
                || orientation == Orientations.ForwardDown
                || orientation == Orientations.UpRight
                || orientation == Orientations.DownLeft) {
            SetOrientation(Orientations.RightDown);
        }
        else {
            return;
        }
        targetPosition = POSITION_RIGHT;

    }
}
