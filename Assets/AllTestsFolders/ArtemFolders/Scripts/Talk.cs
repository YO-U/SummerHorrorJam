using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Talk : MonoBehaviour
{
    public GameObject canvas;
    public TextMeshProUGUI humenSpeak;
    public GameObject objBtns;
    public Button disagree;
    public TextMeshProUGUI textDisagree;
    public Button quest;
    public TextMeshProUGUI textQuest;
    private HumanWalkToWindow human;
    private OpenCloseObject window;
    private CameraMove camera;
    private bool switcher=true;
    private bool switcherbtns=true;

    private void Start()
    {
        canvas.SetActive(false);
        camera = FindObjectOfType<CameraMove>();
        human = FindObjectOfType<HumanWalkToWindow>();
        window = FindObjectOfType<OpenCloseObject>();
    }

    private void Update()
    {
        if (camera.mid && human.hCreatedCh && window.windowOpened)
        { 
            canvas.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Q) && switcher)
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
                humenSpeak.gameObject.SetActive(true);
                objBtns.SetActive(false);
            }
            else
            {
                humenSpeak.gameObject.SetActive(false);//выкл текст
                objBtns.SetActive(true);//вкл кнопки 
                if (Input.GetKeyDown(KeyCode.Q) && switcher == false)
                {
                    if (switcherbtns)
                    {
                        switcherbtns = false;
                    }
                    else
                    {
                        switcherbtns = true;
                    }
                }
                if (switcherbtns)
                {
                    textDisagree.text = "   >Disagree";
                    textQuest.text = "Qwuest";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
						switcher = true;
						human.beenRejected = true;
						human.leavingSequence = true;
						StartCoroutine(human.HumanNahuiPoshel());
					}
                }
                else
                {
                    textDisagree.text = "Disagree";
                    textQuest.text = "   >Qwuest";
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        switcher = true;
                    }
                }
            }
            
        }
        else
        {
            canvas.SetActive(false);
        }
    }
}
