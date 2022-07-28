using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovementScript : MonoBehaviour{

    Rigidbody ourDrone;

    void Awake() {
        ourDrone = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        MovementUpDown();
        MovementForward();
        Rotation();
        ClampingSpeedValues();
        Swerwe();

        ourDrone.AddRelativeForce(Vector3.up * upForce);
        ourDrone.rotation = Quaternion.Euler(
                new Vector3(tiltAmountForward, currentYRotation, tiltAmoutSideways)
            );
    }

    // 上下移动
    public float upForce;
    void MovementUpDown() {

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f || Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f) {
            if (Input.GetKey(KeyCode.I) || Input.GetKey(KeyCode.K)) {
                ourDrone.velocity = ourDrone.velocity;
            }
            if (!Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.L)) {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 281;
            }
            if (Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)) {
                ourDrone.velocity = new Vector3(ourDrone.velocity.x, Mathf.Lerp(ourDrone.velocity.y, 0, Time.deltaTime * 5), ourDrone.velocity.z);
                upForce = 110;
            }
            if(Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.L)) {
                upForce = 410;
            }
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f) {
            upForce = 135;
        }

        if (Input.GetKey(KeyCode.I)) {
            upForce = 450;
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f) {
                upForce = 500;
            }
        }
        else if (Input.GetKey(KeyCode.K)) {
            upForce = -200;
        }
        else if ( !Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.K) && (Mathf.Abs(Input.GetAxis("Vertical")) < 0.2f  && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f) ) {
            upForce = 98.1f;  // 飞机质量10kg
        }
    }

    // 前后移动
    private float movementForwardSpeed = 500.0f;
    private float tiltAmountForward = 0;
    private float tiltVelocityForward;

    void MovementForward() {
        if (Input.GetAxis("Vertical") != 0) {
            ourDrone.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * movementForwardSpeed);
            tiltAmountForward = Mathf.SmoothDamp(tiltAmountForward, 20 * Input.GetAxis("Vertical"), ref tiltVelocityForward, 0.1f);
        }
    }

    // 左右旋转
    private float wantedYRotation;
    private float currentYRotation;
    private float rotateAmoutByKeys = 2.5f;
    private float rotationYVelocity;

    void Rotation() {
        if (Input.GetKey(KeyCode.J)) {
            wantedYRotation -= rotateAmoutByKeys;
        }
        if (Input.GetKey(KeyCode.L)) {
            wantedYRotation += rotateAmoutByKeys;
        }

        currentYRotation = Mathf.SmoothDamp(currentYRotation, wantedYRotation, ref rotationYVelocity, 0.25f);
    }

    // 限制速度
    private Vector3 velocityToSmoothDampToZero;
    void ClampingSpeedValues() {
        if ( Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f ) {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if ( Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f ) {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 10.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)   {
            ourDrone.velocity = Vector3.ClampMagnitude(ourDrone.velocity, Mathf.Lerp(ourDrone.velocity.magnitude, 5.0f, Time.deltaTime * 5f));
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.2f && Mathf.Abs(Input.GetAxis("Horizontal")) < 0.2f)   {
            ourDrone.velocity = Vector3.SmoothDamp(ourDrone.velocity, Vector3.zero, ref velocityToSmoothDampToZero, 0.95f);
        }
    }

    // 左右滚转
    private float sideMovementAmount = 300.0f;
    private float tiltAmoutSideways;
    private float tiltAmoutVelocity;
    void Swerwe() {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            ourDrone.AddRelativeForce(Vector3.right * Input.GetAxis("Horizontal") * sideMovementAmount);
            tiltAmoutSideways = Mathf.SmoothDamp(tiltAmoutSideways, -20 * Input.GetAxis("Horizontal"), ref tiltAmoutVelocity, 0.1f);
        }
        else {
            tiltAmoutSideways = Mathf.SmoothDamp(tiltAmoutSideways, 0, ref tiltAmoutVelocity, 0.1f);
        }

    }

}
