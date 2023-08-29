using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class HumanWalkToWindow : MonoBehaviour
{
    public Transform[] pointsArray = new Transform[3];
    public GameObject[] humansArray = new GameObject[8];
    [SerializeField] private CameraMove cameraMove;
    public bool leavingSequence = false;
    private MovingCar car;
    private bool humanChecker = true;
    public bool beenRejected;
    public GameObject human;
	private System.Random rng;
	public int imposter;
	public bool didImposterGotIn = false;
	public bool didImposterNahuiPoshel = false;
	public int amountOfHappyHumans = 0;
	public int humansSinceTheImposter = 0;
	public bool wasPoliceCalledInTime = false;
	public bool wasPoliceCalled = false;
	public bool wasPoliceCalledEarly = false;
	public int humansSincePoliceCall = 0;
	public bool wasImposterEncountered = false;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private AudioSource carSoundOpen;
	[SerializeField] private AudioSource carSoundClose;
	[SerializeField] private int currentPointIndex = 0;
    [SerializeField] private Animator humanAnimator;
    [SerializeField] private MovingCar movingCar;
    public int currentHuman=-1;
    public float humanSpeed = 0.01f;
    public bool hCreatedCh = false;

    private void Start()
    {
		rng = new System.Random();
        car = FindObjectOfType<MovingCar>();
		rng.Shuffle(humansArray);
		imposter = Random.Range(3, 9);
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
		yield return new WaitForSeconds(15);
		movingCar.readyToDepart = true;
    }

    private IEnumerator MoveHumanToPoint()
    {
        for (int i = currentPointIndex; i < pointsArray.Length; i++)
        {
			human.transform.LookAt(pointsArray[i].position);
			while (Vector3.Distance(human.transform.position, pointsArray[i].position) > 0.01f)
            {
				//human.transform.position = Vector3.MoveTowards(human.transform.position, pointsArray[i].position, humanSpeed * Time.deltaTime);
				yield return null;
            }
            currentPointIndex = i + 1; // Переходим к следующей точке
        }
		human.GetComponent<AudioSource>().Stop();
		humanAnimator.SetBool("IsWalking", false);
		hCreatedCh = true;
        human.transform.rotation = Quaternion.Euler(0,1,0);
	}
    
    private IEnumerator MoveHumanToCar()
    {

		hCreatedCh = false;

		for (int i = currentPointIndex-1; i != -1; i--)
		{
			human.transform.LookAt(pointsArray[i].position);
			while (Vector3.Distance(human.transform.position, pointsArray[i].position) > 0.01f)
            {
				//human.transform.position = Vector3.MoveTowards(human.transform.position, pointsArray[i].position, humanSpeed * Time.deltaTime);
				yield return null;
			}
			currentPointIndex = i - 1; // Переходим к следующей точке
		}
        currentPointIndex= 0;
		human.GetComponent<AudioSource>().Stop();
		humanAnimator.SetBool("IsWalking", false);
		human.transform.LookAt(cameraPosition);
	}

    private IEnumerator ExecuteWithDelayCreate()
    {
		yield return new WaitForSeconds(2);
		carSoundOpen.Play();
		currentHuman++;
		human = Instantiate(humansArray[currentHuman], pointsArray[0].position, Quaternion.identity) as GameObject;
		if (currentHuman == imposter)
		{
			human.transform.DOScale(new Vector3(0.3f,0.3f,0.3f), 1);
			wasImposterEncountered = true;
		}
		else if (wasImposterEncountered)
		{
			humansSinceTheImposter++;
		}
		if (wasPoliceCalled || wasPoliceCalledEarly || wasPoliceCalledInTime)
		{
			humansSincePoliceCall++;
		}
		humanAnimator = human.GetComponent<Animator>();
		yield return new WaitForSeconds(2);
		human.GetComponent<AudioSource>().Play();
		humanAnimator.SetBool("IsWalking", true);
		StartCoroutine(MoveHumanToPoint());
	}
	private IEnumerator ExecuteWithDelayDelete()
	{
		yield return new WaitForSeconds(3);
		human.GetComponent<AudioSource>().Play();
		humanAnimator.SetBool("IsWalking", true);
		StartCoroutine(MoveHumanToCar());
		yield return new WaitForSeconds(7);
        Destroy(human);
        carSoundClose.Play();
        humanChecker = true;
	}
}
   
