using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float min_goal_range;
    [SerializeField] private float max_goal_range;

    [SerializeField] private GameObject playerCar;
    [SerializeField] private GameObject goal;

    [SerializeField] private float time = 0f;
    [SerializeField] private float timeLimit = 60f;


    // Start is called before the first frame update
    void Start()
    {
        ResetGoal();
    }

    // Update is called once per frame
    void Update()
    {
        //print(0.5 - Vector3.Distance(playerCar.transform.position, goal.transform.position)/85);
        if (playerCar.transform.position.y < -2f)
        {
            playerCar.GetComponent<CarAgent>().Fail(-1);
        }
        else if(time > timeLimit)
        {
            playerCar.GetComponent<CarAgent>().Fail(0.5f - Vector3.Distance(playerCar.transform.position, goal.transform.position) / 85);
            ResetGoal();    
        }

        time += Time.deltaTime;

    }

    void ResetGoal()
    {
        float xPos = Random.Range(min_goal_range, max_goal_range) * (Random.Range(0, 2) * 2 - 1); // Multiply by 1 or -1
        float zPos = Random.Range(min_goal_range, max_goal_range) * (Random.Range(0, 2) * 2 - 1);

        goal.transform.SetPositionAndRotation(new Vector3(xPos, 0, zPos), Quaternion.identity);
        time = 0f;

    }

    public void GoalScored()
    {
        print("Goal!");
        //playerCar.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        //playerCar.GetComponent<UnityStandardAssets.Vehicles.Car.CarController>().Stop();
        playerCar.GetComponent<CarAgent>().Goal(1);
        ResetGoal();
    }

}
