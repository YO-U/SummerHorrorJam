using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
	private bool down, mid, left, right, isRotating = false;
	public string currentState;
	public GameObject camera;
	private Quaternion initialRotation;
	private Quaternion targetRotation;
	public float initialRotationSpeed = 5.0f; // Начальная скорость поворота
	public float endingRotationSpeed = 1.0f;  // Замедление скорости поворота

	private void Start()
	{
		mid = true;
		initialRotation = camera.transform.rotation;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.S) && mid == true && !isRotating)
		{
			StartCoroutine(RotateCamera(Vector3.right * 55));
			mid = false;
			down = true;
			currentState = "down";
		}

		if (Input.GetKeyDown(KeyCode.W) && down == true && !isRotating)
		{
			StartCoroutine(RotateCamera(Vector3.right * -55));
			down = false;
			mid = true;
			currentState = "mid";
		}

		if (Input.GetKeyDown(KeyCode.A) && mid == true && !isRotating)
		{
			StartCoroutine(RotateCamera(Vector3.up * -70));
			mid = false;
			left = true;
			currentState = "left";
		}

		if (Input.GetKeyDown(KeyCode.D) && mid == true && !isRotating)
		{
			StartCoroutine(RotateCamera(Vector3.up * 70));
			mid = false;
			right = true;
			currentState = "right";
		}

		if (Input.GetKeyDown(KeyCode.A) && right == true && !isRotating)
		{
			StartCoroutine(RotateCamera(Vector3.up * -70));
			right = false;
			mid = true;
			currentState = "mid";
		}
		if (Input.GetKeyDown(KeyCode.D) && left == true && !isRotating)
		{
			StartCoroutine(RotateCamera(Vector3.up * 70));
			left = false;
			mid = true;
			currentState = "mid";
		}
	}

	private IEnumerator RotateCamera(Vector3 rotation)
	{
		isRotating = true;
		Quaternion startRotation = camera.transform.rotation;
		Quaternion endRotation = startRotation * Quaternion.Euler(rotation);
		float elapsedTime = 0;
		float currentRotationSpeed = initialRotationSpeed;

		while (elapsedTime < currentRotationSpeed)
		{
			camera.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / currentRotationSpeed);
			elapsedTime += Time.deltaTime;

			currentRotationSpeed = Mathf.Lerp(initialRotationSpeed, endingRotationSpeed, elapsedTime / currentRotationSpeed);

			yield return null;
		}

		camera.transform.rotation = endRotation;
		isRotating = false;
	}
}