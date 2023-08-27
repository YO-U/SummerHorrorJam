using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HumanWalkToWindow : MonoBehaviour
{
    public Transform[] pointsArray = new Transform[3];
    public GameObject[] humansArray = new GameObject[2];
    [SerializeField] private CameraMove cameraMove;
    public bool leavingSequence = false;
    private MovingCar car;
    private bool humanChecker = true;
    public bool beenRejected;
    public GameObject human;
    [SerializeField] private int currentPointIndex = 0;
    [SerializeField] private MovingCar movingCar;
    public float humanSpeed = 0.01f;
    public bool hCreatedCh = false;

    private void Start()
    {
        car = FindObjectOfType<MovingCar>();
    }

    private void Update()
    {
        if (!movingCar.isNextCarReady) CarPositionCheck();
    }

    private void CarPositionCheck()
    {
		if (car.gm.transform.position.x < new Vector3(2.2001f, 0.004999995f, -0.943f).x && leavingSequence == false)
		{
			if (humanChecker)
			{
				StartCoroutine(ExecuteWithDelayCreate());
				humanChecker = false;
				leavingSequence = true;
			}
		}
	}

    public IEnumerator HumanNahuiPoshel()
    {
		yield return new WaitForSeconds(1);
		cameraMove.CameraDirectionChange(new Vector3(0, 180, 0), 0.05f, 0.5f, "mid");
        cameraMove.currentState = "mid";
        cameraMove.mid = true;
        cameraMove.left = false;
        cameraMove.right = false;
        cameraMove.down = false;
		StartCoroutine(ExecuteWithDelayDelete());
		yield return new WaitForSeconds(10);
		movingCar.readyToDepart = true;
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
    
    private IEnumerator MoveHumanToCar()
    {
		hCreatedCh = false;

		for (int i = currentPointIndex-1; i != -1; i--)
		{
			while (Vector3.Distance(human.transform.position, pointsArray[i].position) > 0.01f)
			{
				human.transform.position = Vector3.MoveTowards(human.transform.position, pointsArray[i].position, humanSpeed * Time.deltaTime);
				yield return null;
			}
			currentPointIndex = i - 1; // Переходим к следующей точке
		}
        currentPointIndex= 0;
	}

    private IEnumerator ExecuteWithDelayCreate()
    {
		yield return new WaitForSeconds(2);
		human = Instantiate(humansArray[Random.Range(0, 2)], pointsArray[0].position, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(2);
        StartCoroutine(MoveHumanToPoint());
    }
	private IEnumerator ExecuteWithDelayDelete()
	{
		yield return new WaitForSeconds(5);
		StartCoroutine(MoveHumanToCar());
		yield return new WaitForSeconds(2);
        Destroy(human);
        humanChecker = true;
	}
}
   
