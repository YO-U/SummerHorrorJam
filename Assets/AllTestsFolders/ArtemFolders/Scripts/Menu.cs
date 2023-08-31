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
    public GameObject canvaseMenu;
    public GameObject canvaseGuide;
    public GameObject canvaseCredits;
    private int switcher=0;
    private bool chekerCanvase = true;

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || (Input.GetKeyDown(KeyCode.DownArrow))&&chekerCanvase))
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

			if (switcher == 0)
			{
				switcher = 4;
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
                SceneManager.LoadScene(1);
				SceneManager.UnloadSceneAsync(0);
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
                canvaseMenu.SetActive(false);
                canvaseGuide.SetActive(true);
                chekerCanvase = false;
            }
             if (Input.GetKeyDown(KeyCode.Escape))
            {
                canvaseMenu.SetActive(true);
                canvaseGuide.SetActive(false);
                chekerCanvase = true;
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
                canvaseMenu.SetActive(false);
                canvaseCredits.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                canvaseMenu.SetActive(true);
                canvaseCredits.SetActive(false);
                chekerCanvase = true;
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
}
