using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OpenCloseObject : MonoBehaviour
{
    public GameObject currentInteractible;
	public bool windowOpened = false;
	public bool bookOpened = false;
    public bool tvAvtivated = false;
    private int currentChannel = 1;
	private int currentPage = 1;
	private TMP_Text textTemp;
	[SerializeField] private GameObject objectPlaceHolder;
	[SerializeField] private string[] pageContents = new string[8] 
	{"Your job is to oversee the gate", "The button is used to open it", "Don't let any weirdos inside", "You can watch TV on the left", 
		"There is a phone on the right", "You can use it to call the police", "Window is openable, open it to talk", "If it is closed, no one can hear you"};
	[SerializeField] private GameObject tvScreen;
	[SerializeField] private VideoPlayer videoPlayer;
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

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InteractibleActivete();
		TvChannelSwitch();
		BookPageChange();
    }

	//������ �����.

	private void MaterialChange(VideoClip clip)
	{
		videoPlayer.clip = clip;
	}

	//������ ������� ����� � ����������� �� ����������.

	private void CurrentChannelCheck()
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

	//������ ����� ����������.

    private void TvChannelSwitch()
    {
        if (Input.GetKeyDown(interactSecondary) && (cameraMove.currentState == "left") && tvAvtivated)
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
    }

	//������ �������� �����.

	private void BookPageChange()
	{
		if (Input.GetKeyDown(interactSecondary) && (cameraMove.currentState == "down") && bookOpened)
		{
			if (currentPage != 7)
			{
				currentPage += 2;
			}
			else
				currentPage = 1;
			PageContentChange();
		}
	}

	//������ �� ���� ��� �������� ������� ��� �������������, ������� ��� �������� ��������� ���� �������. 
	//�� ������ ����� ������ � ������ ��� ������ �� ���������������.

	private void placeHolderGameObjectActivation(string gameobjectTitle)
	{
		objectPlaceHolder = currentInteractible.transform.Find(gameobjectTitle).gameObject;
		if (objectPlaceHolder.activeInHierarchy)
		objectPlaceHolder.SetActive(false);
		else
		objectPlaceHolder.SetActive(true);
	}

	//���� �����, ��� ������, �� �������� ��� ������ �������.

	private void PageContentChange()
	{
		objectPlaceHolder = GameObject.Find("Page1");
		textTemp = objectPlaceHolder.GetComponent<TMP_Text>();
		textTemp.text = pageContents[currentPage - 1];
		objectPlaceHolder = GameObject.Find("Page2");
		textTemp = objectPlaceHolder.GetComponent<TMP_Text>();
		textTemp.text = pageContents[currentPage];
	}

	//�������� �� ��� �������������� � ����������.

	private void GetAndActivateCurrentInteractable()
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
                if (!windowOpened)
                {
					currentInteractible.transform.Translate(new Vector3(0, 0, -moveDistance));
					windowOpened = true;
				}
                else
                {
					currentInteractible.transform.Translate(new Vector3(0, 0, moveDistance));
                    windowOpened = false;
				}
				break;

            case "left":
				currentInteractible = GameObject.Find("TVScreen");
                if (!tvAvtivated)
                {
					videoPlayer.Play();
					CurrentChannelCheck();
					tvScreen.GetComponent<Renderer>().material = activated;
					tvAvtivated = true;
                }
                else
                {
					videoPlayer.Stop();
					tvScreen.GetComponent<Renderer>().material = deactivated;
					tvAvtivated = false;
				}
				break;

            case "right":
				currentInteractible = GameObject.Find("Phone");
				break;
        }
    }

	//���������� ������� ��� ������� �������.

	public void InteractibleActivete()
	{
		if (Input.GetKeyDown(interact)) 
        {
            GetAndActivateCurrentInteractable();
        }
	}
}
