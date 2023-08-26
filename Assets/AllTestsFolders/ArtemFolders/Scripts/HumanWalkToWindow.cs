using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanWalkToWindow : MonoBehaviour
{
    public Transform[] pointsArray = new Transform[3];
    public GameObject[] humansArray = new GameObject[2];
    private MovingCar car;
    private bool humanChecker = true;
    public GameObject human;
    private int currentPointIndex = 0;
    public float humanSpeed = 0.01f;
    public bool hCreatedCh = false;

    private void Start()
    {
        car = FindObjectOfType<MovingCar>();
    }

    private void Update()
    {
        if (car.gm.transform.position == car._endPoint.position)
        {
            if (humanChecker)
            {
                StartCoroutine(ExecuteWithDelay());
                humanChecker = false;
               
            }
        }
    }

    private IEnumerator MoveHumanToPoint()
    {
        for (int i = currentPointIndex; i < pointsArray.Length; i++)
        {
            while (Vector3.Distance(human.transform.position, pointsArray[i].position) > 0.01f)
            {
                human.transform.position = Vector3.MoveTowards(human.transform.position, pointsArray[i].position, humanSpeed * Time.deltaTime);
                yield return null;
            }
            currentPointIndex = i + 1; // Переходим к следующей точке
        }

        hCreatedCh = true;
    } 
    private IEnumerator ExecuteWithDelay()
    {
        yield return new WaitForSeconds(5);
        human = Instantiate(humansArray[Random.Range(0, 2)], pointsArray[0].position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(3);
        StartCoroutine(MoveHumanToPoint());
    }
}
   
