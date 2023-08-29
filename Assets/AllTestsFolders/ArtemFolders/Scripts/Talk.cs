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
    private bool impost = false;
    [SerializeField] private int normalAnswer,srangeAnswer=0;
    private int BadRandom1;
    private int BadRandom2;
    private Phone _phone;
	private bool seeCallPolice = true;
    private bool smert = true;
    private GateOpening eBtn;

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
 int BadRandom1 = Random.Range(1,5);
 int BadRandom2 = Random.Range(1,5);
        canvas.SetActive(false);
        camera = FindObjectOfType<CameraMove>();
        human = FindObjectOfType<HumanWalkToWindow>();
        window = FindObjectOfType<OpenCloseObject>();
        _phone = FindObjectOfType<Phone>();
        eBtn = FindObjectOfType<GateOpening>();
    }

    private void Update()
    {
        do
        {
            BadRandom1 = Random.Range(1, 5);
        } while (BadRandom1 == BadRandom2);
        
        if (human.imposter == human.currentHuman)
        {
            impost = true;
        }
        else
        {
            impost = false;
        }


        if (_phone.call && window.windowOpened && camera.right)
        {
            humenSpeak.gameObject.SetActive(true);
            objBtns.SetActive(false);
            human.beenRejected = true;
            human.leavingSequence = true;
            smert = false;
            randomAnswer = Random.Range(0, 5);
            switch (randomAnswer)
            {
                case 1:
                    humenSpeak.text = "Thx";
                    break;
                case 2:
                    humenSpeak.text = ":)";
                    break;
                case 3:
                    humenSpeak.text = "XD";
                    break;
                case 4:
                    humenSpeak.text = "OK";
                    break;
                case 5:
                    humenSpeak.text = "zaebok";
                    break;
                default:
                    humenSpeak.text = "uraaaa";
                    break;
            }
        }

        if (eBtn.isGateOpened && camera.down)
            {
                humenSpeak.gameObject.SetActive(true);
                objBtns.SetActive(false);
                human.leavingSequence = true;
                randomAnswer = Random.Range(0, 5);
                switch(randomAnswer)
                {
                    case 1:
                        humenSpeak.text = "Thx";
                        break;
                    case 2:
                        humenSpeak.text = ":)";
                        break;
                    case 3:
                        humenSpeak.text = "XD";
                        break;
                    case 4:
                        humenSpeak.text = "OK";
                        break;
                    case 5:
                        humenSpeak.text = "zaebok";
                        break;
                    default:
                        humenSpeak.text = "uraaaa";
                        break;
                }
        }
        if (camera.mid && human.hCreatedCh && window.windowOpened )
        {
            canvas.SetActive(true);
            if ((smert && eBtn.isGateOpened==false)  || (smert==false && eBtn.isGateOpened))
            {
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
                    humenSpeak.gameObject.SetActive(false); //выкл текст
                    objBtns.SetActive(true); //вкл кнопки 
                    if (Input.GetKeyDown(KeyCode.Q) && switcher == false)
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
                            switcher = true;
                            human.beenRejected = true;
                            human.leavingSequence = true;
                            chekerDisagree = true;
                            randomAnswer = Random.Range(0, 5);
                            switch (randomAnswer)
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
                            int randans = Random.Range(0, 200);
                            int randansG = Random.Range(1, 3);
                            int randansNB = Random.Range(1, 2);


                            if (impost != true)
                            {
                                switch (numberQuest)
                                {
                                    case 1:

                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "1B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "1B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "1N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "1N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;

                                    case 2:
                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "2B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "2B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "2N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "2N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;
                                    case 3:
                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "3B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "3B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "3N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "3N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;
                                    case 4:
                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "4B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "4B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "4N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "4N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;
                                    case 5:
                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "5B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "5B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "5N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "5N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;
                                    case 6:
                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "6B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "6B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "6N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "6N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;
                                    case 7:
                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "7B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "7B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "7N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "7N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;
                                    case 8:
                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "8B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "8B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "8N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "8N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;
                                    case 9:
                                        switch (randans)
                                        {
                                            case >= 190 and <= 200:
                                                if (srangeAnswer < 1)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "9B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "9B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 130 and <= 190:
                                                if (normalAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "9N1";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "9N2";
                                                            normalAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = 5;
                                                }

                                                break;

                                            case >= 0 and <= 130:
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
                                        }

                                        break;
                                }
                            }
                            else
                            {
                                if (numberQuest == BadRandom2)
                                {
                                    randans = 199;
                                }

                                switch (numberQuest)
                                {
                                    case 1:

                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "1B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "1B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                    case 2:
                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "2B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "2B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                    case 3:
                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "3B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "3B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                    case 4:
                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "4B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "4B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                    case 5:
                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "5B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "5B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                    case 6:
                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "6B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "6B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                    case 7:
                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "7B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "7B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                    case 8:
                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "8B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "8B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                    case 9:
                                        switch (randans)
                                        {
                                            case >= 150 and <= 200:
                                                if (srangeAnswer < 2)
                                                {
                                                    switch (randansNB)
                                                    {
                                                        case 1:
                                                            humenSpeak.text = "9B1";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "9B2";
                                                            srangeAnswer++;
                                                            break;
                                                    }
                                                }
                                                else
                                                {
                                                    randans = Random.Range(0, 149);
                                                }

                                                break;

                                            case >= 0 and <= 75:
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
                                            case >= 75 and <= 150:
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

                                        }

                                        break;
                                }
                            }

                            selectQuest = false;
                        }
                    }
                }
            }
        }
        else
        {
            
            if (human.hCreatedCh == false)
            {
				Array.Resize(ref possibleQuestNew, 5);
				srangeAnswer = 0;
                normalAnswer = 0;
                BadRandom2 = Random.Range(1, 5);
                BadRandom1 = Random.Range(1, 5);
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
				chekerDisagree = false;
                seeCallPolice = false;
			}
            canvas.SetActive(false);
        }
    }
        
}
