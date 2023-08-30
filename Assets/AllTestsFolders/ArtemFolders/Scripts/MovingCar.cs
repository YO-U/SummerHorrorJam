using DG.Tweening;
using DG.Tweening.Plugins.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Range = UnityEngine.SocialPlatforms.Range;

public class MovingCar : MonoBehaviour
{
    public Transform _startPoint;
    public Transform _endPoint;
    public float _carSpeed = 0.1f;
    private bool _cheker = true;
    public GameObject[] array = new GameObject[1];
    public GameObject gm;
    public bool readyToDepart = false;
    public bool isNextCarReady = true;
    private bool isBoostReady = true;
    private bool spawnSiren = true;
    [SerializeField] private EndingController endingController;
    [SerializeField] private GameObject policeLights;
    [SerializeField] private HumanWalkToWindow humanWalk;
    [SerializeField] private Vector3[] carDepartPath = new Vector3[4];
    [SerializeField] private float currentDestination;
    [SerializeField] private float rejectDestination;
    [SerializeField] private float moveChange;

    private void Start()
    {
    }

    private void Update()
    {
        SpawnCar();

        MoveCar();

        StartCoroutine(MoveCarIntoCamp());

        StartCoroutine(MoveCarOutOfCamp());

        //if (_cheker)
        //{
        //    if (gm.transform.position != _endPoint.position)
        //    {
        //        gm.transform.DOLocalMoveX(2.2f, 4f);
        //        //gm.transform.position = Vector3.MoveTowards(gm.transform.position, _endPoint.position, _carSpeed);
        //    }
        //    else
        //    {
        //        _cheker = false;
        //    }
        //}
    }

    private void MoveCar()
    {
        if (isBoostReady)
        {
			currentDestination -= moveChange;
			gm.transform.DOLocalMoveX(currentDestination, 4f);
            isBoostReady= false;
		}
	}

    private void SpawnCar()
    {
        if (isNextCarReady && humanWalk.currentHuman != 6 && humanWalk.humansSincePoliceCall <= 2 && !endingController.IsEndingStarting)
		{
			gm = Instantiate(array[Random.Range(0, 9)], new Vector3(_startPoint.position.x, _startPoint.position.y, _startPoint.position.z), Quaternion.identity) as GameObject;
			gm.transform.Rotate(0, -90, 0);
            humanWalk.leavingSequence = false;
            isNextCarReady = false;
		}
        else if ((humanWalk.humansSincePoliceCall > 2 && spawnSiren && isNextCarReady) || ((humanWalk.wasPoliceCalled || humanWalk.wasPoliceCalledEarly) || humanWalk.wasPoliceCalledInTime) && humanWalk.currentHuman == 8)
        {
            spawnSiren = false;
			gm = Instantiate(policeLights, new Vector3(_startPoint.position.x, _startPoint.position.y, _startPoint.position.z), Quaternion.identity) as GameObject;
			gm.transform.Rotate(0, -90, 0);
            endingController.IsEndingStarting = true;
		}
        else if (humanWalk.currentHuman == 6 && endingController.IsEndingStarting)
        {
            if (humanWalk.didImposterNahuiPoshel)
            {
				endingController.IsEndingStarting = true;
				endingController.endingNumber = 3;
			}
			else if (humanWalk.didImposterGotIn)
			{
				endingController.IsEndingStarting = true;
				endingController.endingNumber = 4;
			}
		}
	}

	private IEnumerator MoveCarIntoCamp()
	{
		if (readyToDepart && !humanWalk.beenRejected)
		{
			currentDestination -= moveChange*3;
			gm.transform.DOLocalMoveX(currentDestination, 12f);
            readyToDepart = false;
			yield return new WaitForSeconds(20);
            Destroy(gm);
            currentDestination = 4.4f;
            isNextCarReady = true;
            isBoostReady = true;
		}
	}

    private IEnumerator MoveCarOutOfCamp()
    {
        if (readyToDepart && humanWalk.beenRejected)
        {
            gm.transform.DOPath(carDepartPath, 10f, PathType.CatmullRom, PathMode.Full3D, 5, Color.green);
            gm.transform.DORotate(new Vector3(0, 180, 0), 4f, RotateMode.LocalAxisAdd);
			readyToDepart = false;
			yield return new WaitForSeconds(20);
			Destroy(gm);
            humanWalk.beenRejected = false;
			currentDestination = 4.4f;
			isNextCarReady = true;
			isBoostReady = true;
		}
    }
}