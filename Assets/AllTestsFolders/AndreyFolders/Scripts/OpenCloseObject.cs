using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

public class OpenCloseObject : MonoBehaviour
{
    public GameObject currentInteractible;
	public bool windowOpened = false;
	public bool bookOpened = false;
    public bool tvAvtivated = false;

    public int currentChannel = 1;
	private int currentPage = 1;
	private TMP_Text textTemp;
	public bool inputEvailable;
	[SerializeField] private bool windowReady;
	[SerializeField] private AudioSource windowCreack;
	[SerializeField] private Light tvLight;
	[SerializeField] private AudioSource tvSoundStatic;
	[SerializeField] private GameObject objectPlaceHolder;
	[SerializeField] private string[] pageContents = new string[8] 
	{"Your job is to oversee the gate", "The button is used to open it", "Don't let any weirdos inside", "You can watch TV on the left", 
		"There is a phone on the right", "You can use it to call the police", "Window is openable, open it to talk", "If it is closed, no one can hear you"};
	[SerializeField] private GameObject tvScreen;
	public VideoPlayer videoPlayer;
    [SerializeField] private VideoClip news;
	[SerializeField] private VideoClip sports;
	[SerializeField] private VideoClip music;
	[SerializeField] private VideoClip horror;
	[SerializeField] private VideoClip cartoon;
	[SerializeField] private Material deactivated;
	[SerializeField] private Material activated;
	[SerializeField] private float moveDistance;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private KeyCode interact;
	[SerializeField] private KeyCode interactSecondary;
	[SerializeField] private MonsterKill monsterKill;
	private bool shift=false;

	// Start is called before the first frame update
	void Start()
    {
		inputEvailable = true;
		tvLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
	    if (shift)
	    {
		    if (inputEvailable) 
		    { 
			    InteractibleActivete(); 
			    TvChannelSwitch(); 
			    BookPageChange(); 
		    } 
	    }
	    if (Input.GetKeyDown(KeyCode.LeftShift)) 
	    {
			shift = true;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			shift = false;
		}
		
    }

	//Меняет видос.

	private void MaterialChange(VideoClip clip)
	{
		videoPlayer.clip = clip;
	}

	private IEnumerator windowCooldown()
	{
		if (!windowReady)
		{
			yield return new WaitForSeconds(1);
			windowReady = true;
		}
	}

	//Меняет текущий видос в зависимости от переменной.

	public void CurrentChannelCheck()
	{
		switch (currentChannel)
		{
			case 1:
				MaterialChange(news);
				break;
			case 2:
				MaterialChange(sports);
				break;
			case 3:
				MaterialChange(music);
				break;
			case 4:
				MaterialChange(horror);
				break;
			case 5:
				MaterialChange(cartoon);
				break;
		}
	}

	//Меняет канал телевизора.

    private void TvChannelSwitch()
    {
        if (Input.GetKeyDown(KeyCode.D) && (cameraMove.currentState == "left") && tvAvtivated)
        {
			videoPlayer.Stop();
			if (currentChannel != 5)
			{
				currentChannel++;
			}
			else
				currentChannel = 1;
			CurrentChannelCheck();
			videoPlayer.Play();
		}
		if (Input.GetKeyDown(KeyCode.A) && (cameraMove.currentState == "left") && tvAvtivated)
		{
			videoPlayer.Stop();
			if (currentChannel != 1)
			{
				currentChannel--;
			}
			else
				currentChannel = 5;
			CurrentChannelCheck();
			videoPlayer.Play();
		}
	}

	//Меняет страницу книги.

	private void BookPageChange()
	{
		if (Input.GetKeyDown(KeyCode.D) && (cameraMove.currentState == "down") && bookOpened)
		{
			if (currentPage != 7)
			{
				currentPage += 2;
			}
			else
				currentPage = 1;
			PageContentChange();
		}
		if (Input.GetKeyDown(KeyCode.A) && (cameraMove.currentState == "down") && bookOpened)
		{
			if (currentPage != 1)
			{
				currentPage -= 2;
			}
			else
				currentPage = 7;
			PageContentChange();
		}
	}

	//Визуал не дает мне редачить объекты без инициализации, поэтому мне пришлось придумать этот костыль. 
	//Он просто берет объект и меняет его статус на противоположный.

	private void placeHolderGameObjectActivation(string gameobjectTitle)
	{
		objectPlaceHolder = currentInteractible.transform.Find(gameobjectTitle).gameObject;
		if (objectPlaceHolder.activeInHierarchy)
		objectPlaceHolder.SetActive(false);
		else
		objectPlaceHolder.SetActive(true);
	}

	//Тоже самое, что сверху, но отдельно для текста страниц.

	private void PageContentChange()
	{
		objectPlaceHolder = GameObject.Find("Page1");
		textTemp = objectPlaceHolder.GetComponent<TMP_Text>();
		textTemp.text = pageContents[currentPage - 1];
		objectPlaceHolder = GameObject.Find("Page2");
		textTemp = objectPlaceHolder.GetComponent<TMP_Text>();
		textTemp.text = pageContents[currentPage];
	}

	//Отвечает за все взаимодействия с предметами (Кроме телефона).

	public void GetAndActivateCurrentInteractable()
    {
        switch (cameraMove.currentState)
        {
            case "down":
				currentInteractible = GameObject.Find("Book");
				if (!bookOpened)
				{
					placeHolderGameObjectActivation("Pages");
					PageContentChange();
					placeHolderGameObjectActivation("PieceOfPaper2");
					placeHolderGameObjectActivation("Title");
					bookOpened = true;
				}
				else
				{
					placeHolderGameObjectActivation("Pages");
					placeHolderGameObjectActivation("PieceOfPaper2");
					placeHolderGameObjectActivation("Title");
					bookOpened = false;
				}
                break;

            case "mid":
                currentInteractible = GameObject.Find("MainWindow");
                if (!windowOpened && windowReady)
                {
					currentInteractible.transform.DOLocalMoveZ(0.001f, 0.7f);
					windowCreack.Play();
					windowReady = false;
					windowOpened = true;
					StartCoroutine(windowCooldown());
				}
                else if (windowReady)
                {
					currentInteractible.transform.DOLocalMoveZ(0.01f, 0.7f);
					windowReady = false;
					windowOpened = false;
					StartCoroutine(windowCooldown());
				}
				break;

            case "left":
				currentInteractible = GameObject.Find("TVScreen");
                if (!tvAvtivated && !monsterKill.MonsterSpawned)
                {
					tvScreen.GetComponent<Renderer>().material = activated;
					videoPlayer.Play();
					CurrentChannelCheck();
					tvLight.enabled = true;
					tvAvtivated = true;
					tvSoundStatic.Play();
                }
                else
                {
					videoPlayer.Stop();
					tvLight.enabled = false;
					tvScreen.GetComponent<Renderer>().material = deactivated;
					tvAvtivated = false;
					tvSoundStatic.Stop();
				}
				break;

            case "right":
				currentInteractible = GameObject.Find("Phone");
				break;
        }
    }

	//Активирует предмет при нажатии клавиши.

	public void InteractibleActivete()
	{
		if (Input.GetKeyDown(interact) || Input.GetKeyDown(KeyCode.Space)) 
        {
            GetAndActivateCurrentInteractable();
        }
	}
}
