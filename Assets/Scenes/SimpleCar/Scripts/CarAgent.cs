using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class CarAgent : Agent
{
    private Rigidbody rb;
    private UnityStandardAssets.Vehicles.Car.CarController carController;
    [SerializeField] private GameObject goal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        carController = GetComponent<UnityStandardAssets.Vehicles.Car.CarController>();
        Monitor.SetActive(true);
    }

    public override void AgentReset()
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().Stop();
    }

    public override void CollectObservations()
    {

        //Monitor.Log("PosX", transform.position.x / 30f);
        //AddVectorObs(transform.position.x / 30f);

        //Monitor.Log("PosZ", transform.position.z / 30f);
        //AddVectorObs(transform.position.z / 30f);

        Monitor.Log("VecX", transform.forward.x);
        AddVectorObs(transform.forward.x);

        Monitor.Log("VecZ", transform.forward.z);
        AddVectorObs(transform.forward.z);

        Monitor.Log("GoalX", (goal.transform.position.x - transform.position.x) / 30f);
        AddVectorObs((goal.transform.position.x - transform.position.x) / 30f);

        Monitor.Log("GoalZ", (goal.transform.position.z - transform.position.z) / 30f);
        AddVectorObs((goal.transform.position.z - transform.position.z) / 30f);

    }

    public override void AgentAction(float[] vectorAction)
    {
        carController.Move(vectorAction[0], vectorAction[1], vectorAction[1], 0f);
    }

    public void Goal(float score)
    {
        SetReward(score);
        print("Reward: " + GetReward());
        Done();
    }

    public void Fail(float score)
    {
        SetReward(score);
        print("Reward: " + GetReward());
        Done();
    }

    public override float[] Heuristic()
    {
        var action = new float[2];
        action[0] = Input.GetAxis("Horizontal");
        action[1] = Input.GetAxis("Vertical");
        return action;
    }
}
