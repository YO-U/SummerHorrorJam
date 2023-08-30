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
    [SerializeField] private string[] firstEndingText;
	[SerializeField] private string[] secondEndingText;
	[SerializeField] private string[] thirdEndingText;
	[SerializeField] private string[] fourEndingText;
	[SerializeField] private string[] fifthEndingText;
	[SerializeField] private string[] sixEndingText;
	[SerializeField] private string[] sevenEndingText;
	[SerializeField] private Canvas blackScreen;
    [SerializeField] private OpenCloseObject openCloseObject;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private MovingCar movingCar;
    [SerializeField] private HumanWalkToWindow humanWalk;

    public Canvas canvas;
    public TextMeshProUGUI endText;
    public float fadeInDuration = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartEnding());
    }

    private void EndingSelector()
    {
        switch (endingNumber)
        {
	        case 0:
                StartCoroutine(EndingText(firstEndingText));
                break; 
            case 1:
				StartCoroutine(EndingText(secondEndingText));
				break; 
            case 2:
				StartCoroutine(EndingText(thirdEndingText));
				break;
            case 3:
				StartCoroutine(EndingText(fourEndingText));
				break;
			case 4:
				StartCoroutine(EndingText(fifthEndingText));
				break;
			case 5:
				StartCoroutine(EndingText(sixEndingText));
				break;
			case 6:
				StartCoroutine(EndingText(sevenEndingText));
				break;
		}
    }

    private IEnumerator EndingText(string[] stringArray)
    {
        for (int i = 0; i <= stringArray.Length-1; i++)
        {
            endText.text = stringArray[i];
            yield return new WaitForSeconds(5);
        }
    }

    private IEnumerator StartEnding()
    {
        if (IsEndingStarting && !IsEndingStart)
        {
            IsEndingStart = true;
			openCloseObject.inputEvailable = false;
			blackScreen.GetComponent<Image>().DOColor(Color.black, 3);
			yield return new WaitForSeconds(3);
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
			DOTweenModuleAudio.DOFade(cameraMove.tvAudioSource, 0.02f, 0.1f);
			openCloseObject.currentChannel = 4;
			openCloseObject.CurrentChannelCheck();
			openCloseObject.currentChannel = 1;
			openCloseObject.CurrentChannelCheck();
            openCloseObject.videoPlayer.Play();
			yield return new WaitForSeconds(1);
			DOTweenModuleAudio.DOFade(cameraMove.tvAudioSource, 0.1f, 3f);
			blackScreen.GetComponent<Image>().DOColor(Color.clear, 3);
			yield return new WaitForSeconds(12);
			DOTweenModuleAudio.DOFade(cameraMove.tvAudioSource, 0.02f, 3);
			endText.gameObject.SetActive(true);
			EndingSelector();
			yield return new WaitForSeconds(25);
			blackScreen.GetComponent<Image>().DOColor(Color.black, 3);
			yield return new WaitForSeconds(3);
			SceneManager.UnloadSceneAsync(1);
			SceneManager.LoadScene(0);
		}
    }
}
