using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private bool down, mid, left, right, isRotating = false;
    public GameObject camera;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    public float rotationSpeed = 2.0f;

    private void Start()
    {
        mid = true;
        initialRotation = camera.transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && mid == true && !isRotating)
        {
           
            StartCoroutine(RotateCamera(Vector3.right * 45));
            mid = false;
            down = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && down == true && !isRotating)
        {
            StartCoroutine(RotateCamera(Vector3.right * -45));
            down = false;
            mid = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && mid == true && !isRotating)
        {
            StartCoroutine(RotateCamera(Vector3.up * -90));
            mid = false;
            left = true;
        }

        if (Input.GetKeyDown(KeyCode.D) && mid == true && !isRotating)
        {
            StartCoroutine(RotateCamera(Vector3.up * 90));
            mid = false;
            right = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && right == true && !isRotating)
        {
            StartCoroutine(RotateCamera(Vector3.up * -90));
            right = false;
            mid = true;
        }
        if (Input.GetKeyDown(KeyCode.D) && left == true && !isRotating)
        {
            StartCoroutine(RotateCamera(Vector3.up * 90));
            left = false;
            mid = true;
        }
    }

    private IEnumerator RotateCamera(Vector3 rotation)
    {
        isRotating = true;
        Quaternion startRotation = camera.transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(rotation);
        float elapsedTime = 0;

        while (elapsedTime < rotationSpeed)
        {
            camera.transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / rotationSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        camera.transform.rotation = endRotation;
        isRotating = false;
    }
}
