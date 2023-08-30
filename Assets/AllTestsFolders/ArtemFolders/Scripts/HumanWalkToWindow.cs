using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class HumanWalkToWindow : MonoBehaviour
{
    public Transform[] pointsArray = new Transform[3];
    public GameObject[] humansArray = new GameObject[8];
    [SerializeField] private CameraMove cameraMove;
    public bool leavingSequence = false;
    private MovingCar car;
    private OpenCloseObject window;
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
	public string impostorTag;
	[SerializeField] private OpenCloseObject openCloseObject;
	[SerializeField] private Transform cameraPosition;
    [SerializeField] private AudioSource carSoundOpen;
	[SerializeField] private AudioSource carSoundClose;
	[SerializeField] private int currentPointIndex = 0;
    [SerializeField] private Animator humanAnimator;
    [SerializeField] private MovingCar movingCar;
    public int currentHuman = -1;
    public float humanSpeed = 0.01f;
    public bool hCreatedCh = false;
    public TextMeshProUGUI NewsTxt;
    private bool PoshelNaherClosedWindow=false;
    private float timer;
    public float timerMax = 20f;

    private void Start()
    {
	    timer = timerMax;
	    NewsTxt.gameObject.SetActive(false);
		rng = new System.Random();
		window = FindObjectOfType<OpenCloseObject>();
        car = FindObjectOfType<MovingCar>();
		rng.Shuffle(humansArray);
		imposter = Random.Range(0, 1);
		impostorTag = humansArray[imposter].gameObject.tag;
    }
    
    private void Update()
    {
        if (!movingCar.isNextCarReady) CarPositionCheck();
        if (hCreatedCh)
        {
	        TimerClosedWindow();
        }
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

    private void TimerClosedWindow()
    {
	    if (timer >0 && window.windowOpened == false && hCreatedCh && PoshelNaherClosedWindow ==false)
	    {
		    timer -= Time.deltaTime;
	    }else if (timer <= 0  && window.windowOpened == false && hCreatedCh && PoshelNaherClosedWindow ==false) 
	    {
		    StartCoroutine(HumanNahuiPoshel());
		    didImposterNahuiPoshel = true;
		    beenRejected = true;
		    PoshelNaherClosedWindow = true;
		    timer = timerMax;
	    }
	    if (window.windowOpened)
	    {
		    timer = timerMax;
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
		if (currentHuman == 1)
		{
			openCloseObject.inputEvailable = false;
			yield return new WaitForSeconds(3);
			cameraMove.CameraDirectionChange(new Vector3(15, 110, 0), 0.2f, 0.5f, "left");
			cameraMove.currentState = "left";
			cameraMove.mid = false;
			cameraMove.left = true;
			cameraMove.right = false;
			cameraMove.down = false;
			yield return new WaitForSeconds(1);
			if (!openCloseObject.tvAvtivated) openCloseObject.GetAndActivateCurrentInteractable();
			yield return new WaitForSeconds(1);
			openCloseObject.videoPlayer.Pause();
			openCloseObject.currentChannel = 1;
			openCloseObject.CurrentChannelCheck();
			openCloseObject.videoPlayer.Play();
			yield return new WaitForSeconds(12);
			openCloseObject.videoPlayer.Pause();
			NewsTxt.gameObject.SetActive(true);
			NewsTxt.text = "Breaking news! One of the recently discovered 'doppelgangers' was spotted in local area!.";
			yield return new WaitForSeconds(4);
			switch (impostorTag)
			{
				case "grandpa":
					NewsTxt.text = "It seems like creature 'transformed' to the form of eldery person. Other characteristics are unknown.";
					break;
				
				case "afro":
					NewsTxt.text = "It seems like creature wears some kind of formal clothes. Other characteristics are unknown.";
					break;
				
				case "grandma":
					NewsTxt.text = "It seems like creature 'transformed' to the form of eldery person. Other characteristics are unknown.";
					break;
				
				case "man":
					NewsTxt.text = "It seems like creature 'transformed' to the form of human male. Other characteristics are unknown.";
					break;
				
				case "asianwoman":
					NewsTxt.text = "It seems like creature 'transformed' to the young female. Other characteristics are unknown.";
					break;
				
				case "womanhairblack":
					NewsTxt.text = "It seems like creature 'transformed' to the young female. Other characteristics are unknown.";
					break;
				
				case "womanhairwhite":
					NewsTxt.text = "It seems like creature wears some kind of formal clothes. Other characteristics are unknown.";
					break;
				
				case "womanblueshirt":
					NewsTxt.text = "It seems like creature 'transformed' to the human female. Other characteristics are unknown.";
					break;
				
				case "manyoung":
					NewsTxt.text = "It seems like creature 'transformed' to the form of young male. Other characteristics are unknown.";
					break;
			}
			yield return new WaitForSeconds(4);
			movingCar.readyToDepart = true;
			NewsTxt.text = "Please notify the police if you see someone who fits the description and acts strange.";
			yield return new WaitForSeconds(4);
			NewsTxt.text = "Keep in mind that those creatures are extremely violent. Keep yourself safe. End of broadcast.";
			yield return new WaitForSeconds(5);
			NewsTxt.gameObject.SetActive(false);
			openCloseObject.videoPlayer.Play();
			openCloseObject.inputEvailable = true;
		}
		yield return new WaitForSeconds(14);
		if (!movingCar.readyToDepart) movingCar.readyToDepart = true;
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
		PoshelNaherClosedWindow = false;

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
   
