using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    public int endingNumber = 0;
    public bool IsEndingStarting;
    public bool IsEndingStart = false;
    private bool HasSceneStarted = false;
    [SerializeField] private Canvas blackScreen;
    [SerializeField] private OpenCloseObject openCloseObject;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private MovingCar movingCar;
    [SerializeField] private HumanWalkToWindow humanWalk;

    public CanvasGroup canvas;
    public TextMeshProUGUI endText;
    public float fadeInDuration = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas.alpha = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartEnding());
        endText.gameObject.SetActive(false);
    }

    private void EndingSelector()
    {
	    StartCoroutine(FadeInCanvas());
        switch (endingNumber)
        {
	        case 0:
	            endText.text = "0";
                break; 
            case 1:
	            endText.text = "1";
                break; 
            case 2:
	            endText.text = "2";
                break;
            case 3:
	            endText.text = "3";
                break;
        }
    }
    private IEnumerator FadeInCanvas()
    {
	    float elapsedTime = 0f;

	    while (elapsedTime < fadeInDuration)
	    {
		    float normalizedTime = elapsedTime / fadeInDuration;
		    canvas.alpha = Mathf.Lerp(0f, 1f, normalizedTime);

		    elapsedTime += Time.deltaTime;
		    yield return null;
	    }
	    endText.gameObject.SetActive(false);
	    canvas.alpha = 1f; // Ensure it's fully visible at the end
    }

    private IEnumerator StartEnding()
    {
        if (IsEndingStarting && !IsEndingStart)
        {
            IsEndingStart = true;
			openCloseObject.inputEvailable = false;
			blackScreen.GetComponent<Image>().DOColor(Color.black, 5);
            yield return new WaitForSeconds(5);
			cameraMove.CameraDirectionChange(new Vector3(15, 110, 0), 0.2f, 0.5f, "left");
			cameraMove.currentState = "left";
			cameraMove.mid = false;
			cameraMove.left = true;
			cameraMove.right = false;
			cameraMove.down = false;
			if (!openCloseObject.tvAvtivated) openCloseObject.GetAndActivateCurrentInteractable();
            else
            {
				openCloseObject.GetAndActivateCurrentInteractable();
				openCloseObject.GetAndActivateCurrentInteractable();
			}
			openCloseObject.currentChannel = 1;
			openCloseObject.CurrentChannelCheck();
			openCloseObject.videoPlayer.Play();
			yield return new WaitForSeconds(5);
			DOTweenModuleAudio.DOFade(cameraMove.tvAudioSource, 0.01f, 3);
			EndingSelector();
			blackScreen.GetComponent<Image>().DOColor(Color.clear, 5);
			yield return new WaitForSeconds(5);
            if (!HasSceneStarted)
            SceneManager.LoadScene(0);
            HasSceneStarted = true;
		}
    }
}
