using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using TMPro;

public class MoveToGoalAgent : Agent {

    //public Rigidbody ball;
    //Rigidbody player;
    //public float speed = 30.0f;
    //float diff = 0.0f;
    //float previousDiff = 0.0f;
    //float previousY = 5.0f;
    //bool collied = false;

    //private void OnCollisionEnter(Collision collision) 
    //{
    //    if (collison.rigidbody == ball)
    //    {
    //        collied = true;
    //    }
    //}

    //public override void InitializeAgent()
    //{
    //    player = this.GetComponent<Rigidbody>();
    //}

    //public override void CollectObsercations()
    //{
    //    AddVectorObs(ball.transform.localPosition); //3d
    //    AddVectorObs(ball.velocity); //3d
    //    AddVectorObs(ball.rotation); //4d
    //    AddVectorObs(ball.angularVelocity); //3d

    //    AddVectorObs(player.transform.localPosition); //3d
    //    AddVectorObs(player.velocity); //3d
    //    AddVectorObs(player.rotation); //4d
    //    AddVectorObs(player.angularVelocity); //3d
    //}

    //public override void AgentAction(float[] vectorAction)
    //{
    //    Vector3.controlSignal = Vector3.zero;
    //    controlSignal.x = vectorAction[0];
    //    controlSignal.z = vectorAction[1];
    //    if (player.transform.localPosition.y == 1.0f)
    //    {
    //        controlSignal.y = vectorAction[2] * 10.0f;
    //    }

    //    player.AddForce(controlSignal * speed);

    //    diff = ball.transform.localPosition.y - previousY;
    //    if (diff > 0.0f && previousDiff < 0.0f && collied)
    //    {
    //        AddReward(0.1f);
    //    }
    //    collied = false;
    //    previousDiff = diff;
    //    previousY = ball.transform.localPosition.y;

    //    if (ball.transform.localPosition.y < 1.5f ||
    //        Mathf.Abs(player.transform.localPosition.x) > 10.0f ||
    //        Mathf.Abs(player.transform.localPosition.z) > 10.0f
    //        )
    //    {
    //        Done();
    //    }
    //}

    //public override void AgentReset()
    //{
    //    ball.transform.localPosition = new Vector3(Random.value * 10 - 5, 5.0f, Random.value * 10 - 5);
    //    ball.velocity = Vector3.zero;
    //    ball.rotation = Quaternion.Euler(Vector3.zero);
    //    ball.angularVelocity = Vector3.zero;

    //    player.transform.localPosition = Vector3.up;
    //    player.velocity = Vector3.zero;
    //    player.rotation = Quaternion.Euler(Vector3.zero);
    //    player.angularVelocity = Vector3.zero;

    //    diff = 0.0f;
    //    previousDiff = 0.0f;
    //    previousY = 5.0f;
    //    collied = false;
    //}

    //public override float[] Heuristic()
    //{
    //    float[] vecgtorAction = new float[3];
    //    vectorAction[0] = Input.GetAxis("Horizontal");
    //    vectorAction[1] = Input.GetAxis("Vertical");
    //    vectorAction[2] = Input.GetAxis("Jump");
    //    return vectorAction;

    //}
}







