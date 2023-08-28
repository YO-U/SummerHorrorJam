using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvasPauseMenu;
    public TextMeshProUGUI BackMenu;
    public TextMeshProUGUI resume;
    public GameObject scripts;
    private bool isTimePaused = false;
    private bool switcher= false;
    private bool chekerCanvase = true;
    private void Start()
    {
        canvasPauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            scripts.SetActive(false);
            chekerCanvase = true;
            PauseTime();
            canvasPauseMenu.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
                {
                    if (switcher)
                    {
                        switcher = false;
                    }
                    else
                    {
                        switcher = true;
                    }
                }

                if (switcher)
                {
                    BackMenu.text = "   >Back to the menu";
                    resume.text = "Resume";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ResumeTime();
                        SceneManager.LoadScene(0);
                    }
                }
                else
                {
                    BackMenu.text = "Back to the menu";
                    resume.text = "   >Resume";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        ResumeTime();
                        scripts.SetActive(true);
                        canvasPauseMenu.SetActive(false);
                    }
                }
            
        
    }
    void PauseTime()
    {
        Time.timeScale = 0f; // Остановка времени
        isTimePaused = true;
    }

    void ResumeTime()
    {
        Time.timeScale = 1f; // Возобновление времени
        isTimePaused = false;
    }
}
