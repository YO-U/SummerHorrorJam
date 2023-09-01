using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool ch1=false;
    private void Update()
    {
        
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ch1 =true;
            StartCoroutine(StartEnding());
        }

        if (Input.GetKeyDown(KeyCode.Escape) && ch1)
        {
            SceneManager.LoadScene(0);
			SceneManager.UnloadSceneAsync(1);
		}
    }

    private IEnumerator StartEnding()
    {
        yield return new WaitForSeconds(1);
        ch1 = false;
    }
}
