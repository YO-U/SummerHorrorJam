using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public TextMeshProUGUI play;
    public TextMeshProUGUI guide;
    public TextMeshProUGUI credits;
    public TextMeshProUGUI quit;
    public TextMeshProUGUI start;
    public GameObject canvaseMenu;
    public GameObject canvaseGuide;
    public GameObject canvaseCredits;
    public CanvasGroup canvasStartGame;
    public float fadeInDuration = 1.0f;
    public int speedScrolStartText = 2;
    private int switcher=0;
    private bool chekerCanvase = true;
    private bool txtStart = false;

    private void Start()
    {
        canvasStartGame.alpha = 0f;
    }

    public void Update()
    {
        if (((Input.GetKeyDown(KeyCode.Q) || (Input.GetKeyDown(KeyCode.DownArrow)))&&chekerCanvase))
        {
            switcher++;

            if (switcher == 4)
            {
                switcher = 0;
            }     
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && chekerCanvase)
        {
			switcher--;

			if (switcher == -1)
			{
				switcher = 3;
			}
		}
        if (switcher == 0)
        {
            play.text = "   >Play";
            guide.text = "Guide";
            credits.text = "Credits";
            quit.text = "Quit";
            if (Input.GetKeyDown(KeyCode.E))
            {
                canvaseMenu.SetActive(false);
                canvasStartGame.gameObject.SetActive(true);
                StartCoroutine(FadeInCanvas());
               // if (txtStart == true)
              //  {
                    StartCoroutine(StartTxt());
               // }
            }  
        }
        if (switcher == 1)
        {
            play.text = "Play";
            guide.text = "   >Guide";
            credits.text = "Credits";
            quit.text = "Quit";
            
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                chekerCanvase = false;
                canvaseMenu.SetActive(false);
                canvaseGuide.SetActive(true);
                
            }
             if (Input.GetKeyDown(KeyCode.Escape))
            {
                chekerCanvase = true;
                canvaseMenu.SetActive(true);
                canvaseGuide.SetActive(false);
                
            }
        }
        if (switcher == 2)
        {
            play.text = "Play";
            guide.text = "Guide";
            credits.text = "   >Credits";
            quit.text = "Quit";
            if (Input.GetKeyDown(KeyCode.E))
            {
                chekerCanvase = false;
                canvaseMenu.SetActive(false);
                canvaseCredits.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                chekerCanvase = true;
                canvaseMenu.SetActive(true);
                canvaseCredits.SetActive(false);
            }
        }
        if (switcher == 3)
        {
            play.text = "Play";
            guide.text = "Guide";
            credits.text = "Credits";
            quit.text = "   >Quit";
            if (Input.GetKeyDown(KeyCode.E))
            {
                Application.Quit();
            }
        }
    }
    private IEnumerator FadeInCanvas()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            float normalizedTime = elapsedTime / fadeInDuration;
            canvasStartGame.alpha = Mathf.Lerp(0f, 1f, normalizedTime);

            elapsedTime += Time.deltaTime;
            yield return null;
           // txtStart = true;
        }

        canvasStartGame.alpha = 1f; // Ensure it's fully visible at the end
        
    }

    private IEnumerator StartTxt()
    {
        yield return new WaitForSeconds(1);
        start.gameObject.SetActive(true);
        start.text = "esli";
        yield return new WaitForSeconds(speedScrolStartText);
        start.text = "cho";
        yield return new WaitForSeconds(speedScrolStartText);
        start.text = "ti";
        yield return new WaitForSeconds(speedScrolStartText);
        start.text = "gay";
        yield return new WaitForSeconds(speedScrolStartText);
        SceneManager.LoadScene(1);
		SceneManager.UnloadSceneAsync(0);
    }
}
