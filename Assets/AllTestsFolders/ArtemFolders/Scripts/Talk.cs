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
    public TextMeshProUGUI textDisagree;
    public TextMeshProUGUI textQuest;
    private HumanWalkToWindow human;
    private OpenCloseObject window;
    private CameraMove camera;
    private bool switcher=true;
    private bool switcherbtns=true;
    private bool chekerDisagree=false;
    private int randomAnswer,randomQuest;
    private string selectedAnswer, selectedQuest;
    private char fistChar;
    private int numberQuest;
    private bool selectQuest=false;
    
    string[] possibleAnswers = {
        "Poshol nahui",
        "pidoras",
        "Gandoun",
        "OK",
        "HAHAHAHAH"
    };
    string[] possibleQuestOld = {
        "1Poshol nahui?",
        "2pidoras?",
        "3Gandoun?",
        "4OK?",
        "5HAHAHAHAH?",
        "6Poshol?",
        "7pido?",
        "8Gand?",
        "9O?",
    };
    string[] possibleQuestNew = new string[5];



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
                    if (chekerDisagree == false)
                    {
                        randomQuest = Random.Range(0, possibleQuestNew.Length);
                        
                        switcher = false;
                    }
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
                if (Input.GetKeyDown(KeyCode.Q) && switcher == false )
                {
                    if (possibleQuestNew.Length != 0)
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
                    else
                    {
                        switcherbtns = true;
                    }
                }
                if (switcherbtns)
                {
                    textDisagree.text = "   >Disagree";
                    if (possibleQuestNew.Length != 0)
                    {
                        selectedQuest = possibleQuestNew[randomQuest];
                        textQuest.text = selectedQuest;
                    }
                    else
                    {
                        textQuest.text = "";
                    }

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Array.Resize(ref possibleQuestNew, 5); ;
                        switcher = true;
						human.beenRejected = true;
						human.leavingSequence = true;
                        chekerDisagree = true;
                        randomAnswer = Random.Range(0, 5);
                        switch(randomAnswer)
                        {
                            case 1:
                                humenSpeak.text = "Poshol nahui";
                                break;
                            case 2:
                                humenSpeak.text = "pidoras";
                                break;
                            case 3:
                                humenSpeak.text = "Gandoun";
                                break;
                            case 4:
                                humenSpeak.text = "OK";
                                break;
                            case 5:
                                humenSpeak.text = "HAHAHAHAH";
                                break;
                            default:
                                humenSpeak.text = "choooooo";
                                break;
                        }
						StartCoroutine(human.HumanNahuiPoshel());
                        human.didImposterNahuiPoshel = true;
					}
                }
                else 
                {
                    textDisagree.text = "Disagree";
                    textQuest.text = "";
                    selectedQuest = possibleQuestNew[randomQuest];
                        textQuest.text = "   >" + selectedQuest;
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            List<string> tempAnswers = new List<string>(possibleQuestNew);
                            tempAnswers.RemoveAt(randomQuest);
                            possibleQuestNew = tempAnswers.ToArray();
                            switcher = true;
                            selectQuest = true;
                            
                            fistChar = selectedQuest[0]; 
                            numberQuest = (int)char.GetNumericValue(fistChar); 
                            int randans = Random.Range(0, 100);
                            int randansG = Random.Range(1, 3);
                            int randansNB = Random.Range(1, 2);
                            switch (numberQuest) 
                            { 
                                case 1: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "1G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "1G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "1G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "1N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "1N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "1B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "1B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    
                                    break; 
                                case 2: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "2G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "2G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "2G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "2N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "2N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "2B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "2B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    break; 
                                case 3: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "3G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "3G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "3G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "3N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "3N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "3B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "3B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    
                                    break; 
                                case 4: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "4G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "4G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "4G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "4N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "4N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "4B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "4B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    
                                    break; 
                        case 5: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "5G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "5G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "5G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "5N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "5N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "5B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "5B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    
                                    break; 
                        case 6: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "6G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "6G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "6G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "6N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "6N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "6B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "6B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    
                                    break; 
                       case 7: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "7G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "7G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "7G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "7N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "7N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "7B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "7B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    
                                    break; 
                       case 8: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "8G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "8G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "8G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "8N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "8N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "8B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "8B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    
                                    break; 
                        case 9: 
                                    switch (randans) 
                                    { 
                                        case >= 0 and <= 70:
                                            switch (randansG)
                                            {
                                                case 1:
                                                    humenSpeak.text = "9G1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "9G2";
                                                    break;
                                                case 3:
                                                    humenSpeak.text = "9G3";
                                                    break;
                                            }
                                            break; 
                                        case >= 71 and <= 95: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "9N1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "9N2";
                                                    break;
                                            }
                                            break; 
                                        case >= 96 and <= 100: 
                                            switch (randansNB)
                                            {
                                                case 1:
                                                    humenSpeak.text = "9B1";
                                                    break;
                                                case 2:
                                                    humenSpeak.text = "9B2";
                                                    break;
                                            } 
                                            break; 
                                    }
                                    
                                    break; 
                    }

                    selectQuest = false;
                        }
                }
            }
            
        }
        else
        {
            if (human.hCreatedCh == false)
            {
                List<string> tempList = new List<string>(possibleQuestOld);
                for (int i = 0; i < 5; i++)
                {
                    int randomIndex = Random.Range(0, tempList.Count);
                    possibleQuestNew[i] = tempList[randomIndex];
                    tempList.RemoveAt(randomIndex);
                }
                randomAnswer = Random.Range(0, 5);
                switch (randomAnswer)
                {
                    case 1:
                        humenSpeak.text = "Ku";
                        break;
                    case 2:
                        humenSpeak.text = "zdarova";
                        break;
                    case 3:
                        humenSpeak.text = "Nice balls";
                        break;
                    case 4:
                        humenSpeak.text = "Privet";
                        break;
                    case 5:
                        humenSpeak.text = "HAHAHAHAH";
                        break;
                    default:
                        humenSpeak.text = "OK";
                        break;
                }
            }

            chekerDisagree = false;
            canvas.SetActive(false);
        }
    }
}
