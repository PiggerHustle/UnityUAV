﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;

public class JuggleAgent : Agent
{
    public Rigidbody ball;
    Rigidbody player;
    public float speed = 30.0f;
    public TextMeshPro display;
    float diff = 0.0f; // 球员当前的y轴与上一步的的差异
    float previousDiff = 0.0f; // 保留了上一个diff
    float previousY = 5.0f; // 球员上一步的y轴的值
    bool collied = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody == ball)
        {
            collied = true;
        }
    }

    public override void InitializeAgent()
    {
        player = this.GetComponent<Rigidbody>();
    }

    public override void CollectObservations()
    {
        AddVectorObs(ball.transform.localPosition);
        AddVectorObs(ball.velocity);
        AddVectorObs(ball.rotation);
        AddVectorObs(ball.angularVelocity);

        AddVectorObs(player.transform.localPosition);
        AddVectorObs(player.velocity);
        AddVectorObs(player.rotation);
        AddVectorObs(player.angularVelocity);
    }

    public override void AgentAction(float[] vectorAction)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = vectorAction[0];
        controlSignal.z = vectorAction[1];
        if(player.transform.localPosition.y == 1.0f)
        {
            controlSignal.y = vectorAction[2] * 10.0f;
        }

        player.AddForce(controlSignal * speed);

        diff = ball.transform.localPosition.y - previousY;
        if(diff > 0.0f && previousDiff < 0.0f && collied)
        {
            AddReward(0.1f);
        }
        collied = false;
        previousDiff = diff;
        previousY = ball.transform.localPosition.y;

        if(ball.transform.localPosition.y < 1.5f || Mathf.Abs(player.transform.localPosition.x) > 10.0f || Mathf.Abs(player.transform.localPosition.z) > 10.0f)
        {
            Done();
        }
        display.text = GetCumulativeReward().ToString("N1");
    }

    public override void AgentReset()
    {
        ball.transform.localPosition = new Vector3(Random.value * 10 - 5, 5.0f, Random.value * 10 - 5);
        ball.velocity = Vector3.zero;
        ball.rotation = Quaternion.Euler(Vector3.zero);
        ball.angularVelocity = Vector3.zero;

        player.transform.localPosition = Vector3.up;
        player.velocity = Vector3.zero;
        player.rotation = Quaternion.Euler(Vector3.zero);
        player.angularVelocity = Vector3.zero;

        diff = 0.0f; 
        previousDiff = 0.0f;
        previousY = 5.0f;
        collied = false;
    }

    public override float[] Heuristic()
    {
        float[] vectorAction = new float[3];
        vectorAction[0] = Input.GetAxis("Horizontal");
        vectorAction[1] = Input.GetAxis("Vertical");
        vectorAction[2] = Input.GetAxis("Jump");
        return vectorAction;
    }
}
