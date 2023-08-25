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
    [SerializeField] private Material news;
	[SerializeField] private Material sports;
	[SerializeField] private Material music;
	[SerializeField] private Material horror;
	[SerializeField] private Material cartoon;
	[SerializeField] private float moveDistance;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private KeyCode interact;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InteractibleActivete();
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

            case "right":
				currentInteractible = GameObject.Find("TV");
                if (!tvAvtivated)
                {
                    switch (currentChannel)
                    {
    
                    }
                }
				break;

            case "left":
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
