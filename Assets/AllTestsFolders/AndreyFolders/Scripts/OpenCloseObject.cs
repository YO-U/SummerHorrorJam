using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class OpenCloseObject : MonoBehaviour
{
    public GameObject currentInteractible;
	private bool windowOpened = false;
    private bool tvAvtivated = false;
    private int currentChannel = 1;
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
    }

	private void MaterialChange(VideoClip clip)
	{
		videoPlayer.clip = clip;
	}

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

    private void GetAndActivateCurrentInteractable()
    {
        switch (cameraMove.currentState)
        {
            case "down":
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

	public void InteractibleActivete()
	{
		if (Input.GetKeyDown(interact)) 
        {
            GetAndActivateCurrentInteractable();
        }
	}
}
