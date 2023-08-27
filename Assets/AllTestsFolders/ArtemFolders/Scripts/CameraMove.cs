using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
	public bool down, mid, left, right;
	public string currentState;
	public GameObject camera;
	public DOTween dOTween;
	[SerializeField] private AudioSource tvAudioSource;
	[SerializeField] private Transform cameraPos;

	private void Start()
	{
		DOTween.Init();
		mid = true;
	}

	private void Update()
	{
		InputCheck();
	}

	private void InputCheck()
	{
		switch (currentState)
		{
			case "down":
				if (Input.GetKeyDown(KeyCode.W) && down == true)
				{
					CameraDirectionChange(new Vector3(0, 180, 0), 0.05f, 0.5f, "mid");
					down = false;
					mid = true;
				}

				if (Input.GetKeyDown(KeyCode.A) && down == true)
				{
					CameraDirectionChange(new Vector3(15, 110, 0), 0.2f, 0.5f, "left");
					down = false;
					left = true;
				}

				if (Input.GetKeyDown(KeyCode.D) && down == true)
				{
					CameraDirectionChange(new Vector3(0, 250, 0), 0.01f, 0.5f, "right");
					down = false;
					right = true;
				}
				break;
			case "mid":
				if (Input.GetKeyDown(KeyCode.A) && mid == true)
				{
					CameraDirectionChange(new Vector3(15, 110, 0), 0.2f, 0.5f, "left");
					mid = false;
					left = true;
				}

				if (Input.GetKeyDown(KeyCode.D) && mid == true)
				{
					CameraDirectionChange(new Vector3(0, 250, 0), 0.01f, 0.5f, "right");
					mid = false;
					right = true;
				}

				if (Input.GetKeyDown(KeyCode.S) && mid == true)
				{
					CameraDirectionChange(new Vector3(55, 180, 0), 0.05f, 0.5f, "down");
					mid = false;
					down = true;
				}
				break;
			case "right":
				if (Input.GetKeyDown(KeyCode.A) && right == true)
				{
					CameraDirectionChange(new Vector3(0, 180, 0), 0.05f, 0.5f, "mid");
					right = false;
					mid = true;
				}

				if (Input.GetKeyDown(KeyCode.S) && right == true)
				{
					CameraDirectionChange(new Vector3(55, 180, 0), 0.05f, 0.5f, "down");
					right = false;
					down = true;
				}
				break;
			case "left":
				if (Input.GetKeyDown(KeyCode.D) && left == true)
				{
					CameraDirectionChange(new Vector3(0, 180, 0), 0.05f, 0.5f, "mid");
					left = false;
					mid = true;
				}

				if (Input.GetKeyDown(KeyCode.S) && left == true)
				{
					CameraDirectionChange(new Vector3(55, 180, 0), 0.05f, 0.5f, "down");
					left = false;
					down = true;
				}
				break;
		}
	}

	public void CameraDirectionChange(Vector3 vector3, float volume, float speed, string currentStateHolder)
	{
		cameraPos.DORotate(vector3, 1f, default);
		DOTweenModuleAudio.DOFade(tvAudioSource, volume, speed);
		currentState = currentStateHolder;
	}

	public IEnumerator ExecuteWithDelay()
	{
		yield return new WaitForSeconds(4);
	}

	//private IEnumerator RotateCamera(Vector3 rotation)
	//{
	//	isRotating = true;
	//	Quaternion startRotation = camera.transform.rotation;
	//	Quaternion endRotation = startRotation * Quaternion.Euler(rotation);
	//	float elapsedTime = 0;
	//	float currentRotationSpeed = initialRotationSpeed;

	//	while (elapsedTime < currentRotationSpeed)
	//	{
	//		camera.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / currentRotationSpeed);
	//		elapsedTime += Time.deltaTime;

	//		currentRotationSpeed = Mathf.Lerp(initialRotationSpeed, endingRotationSpeed, elapsedTime / currentRotationSpeed);

	//		yield return null;
	//	}

	//	camera.transform.rotation = endRotation;
	//	isRotating = false;
	//}
}