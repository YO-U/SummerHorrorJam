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
    private GameObject gm;

    private void Start()
    {
        gm = Instantiate(array[Random.Range(0,5)],new Vector3(_startPoint.position.x,_startPoint.position.y,_startPoint.position.z) ,Quaternion.identity) as GameObject;
        gm.transform.Rotate(0, -90, 0);
    }

    private void Update()
    {
        if (_cheker)
        {
            if (gm.transform.position != _endPoint.position)
            {
                gm.transform.position = Vector3.MoveTowards(gm.transform.position, _endPoint.position, _carSpeed);
            }
            else
            {
                _cheker = false;
            }
        }
    }
}