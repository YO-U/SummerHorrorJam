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
    private bool didHumanGotScared;
	private Phone _phone;
	private bool seeCallPolice = true;
    private bool smert = true;
    private GateOpening eBtn;
    private bool deletedFirstChar=false;

	string[] possibleAnswers = {
        "Poshol nahui",
        "pidoras",
        "Gandoun",
        "OK",
        "HAHAHAHAH"
    };
    string[] possibleQuestOld = {
        "1Why do you want to go inside?",
        "2Does anyone from camp staff know you are comming?",
        "3Is there anyone else in the car?",
        "4Can i see your ID?",
        "5Why did you decide to arrive at night?",
		"6Are you aware of the recent 'doppelganger?' threat?",
        "7Are you familliar with camp's rules of visiting?",
        "8Do you have children?",
        "9For how long do you plan to stay here?",
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


        if (_phone.call && window.windowOpened && camera.right && !didHumanGotScared && human.currentHuman != human.imposter)
        {
            didHumanGotScared = true;
            humenSpeak.gameObject.SetActive(true);
            objBtns.SetActive(false);
            human.beenRejected = true;
            human.leavingSequence = true;
            smert = false;
            randomAnswer = Random.Range(0, 5);
            switch (randomAnswer)
            {
                case 1:
                    humenSpeak.text = "What?... i-i need to go.";
                    break;
                case 2:
                    humenSpeak.text = "What the hell!? Are you out of your mind!?";
                    break;
                case 3:
                    humenSpeak.text = "Time to get the fuck out i guess.";
                    break;
                case 4:
                    humenSpeak.text = "...";
                    break;
                case 5:
                    humenSpeak.text = "I haven't even done anything!";
                    break;
                default:
                    humenSpeak.text = "Am i THAT suspicious?";
                    break;
            }
			human.beenRejected = true;
            StartCoroutine(human.HumanNahuiPoshel());
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
                        humenSpeak.text = "Thank you!";
                        break;
                    case 2:
                        humenSpeak.text = "Finally.";
                        break;
                    case 3:
                        humenSpeak.text = "...";
                        break;
                    case 4:
                        humenSpeak.text = "Have a good night.";
                        break;
                    case 5:
                        humenSpeak.text = "Can't wait to see my children!";
                        break;
                    default:
                        humenSpeak.text = "Oh... That's it? Goodbye!";
                        break;
                }
        }
        if (camera.mid && human.hCreatedCh && window.windowOpened )
        {
            canvas.SetActive(true);
            if ((smert && eBtn.isGateOpened==false)  || (smert==false && eBtn.isGateOpened))
            {
                if ((Input.GetKeyDown(KeyCode.Q)  && switcher))
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
                    if ((Input.GetKeyDown(KeyCode.Q) || (Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.UpArrow)) && switcher == false)))
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
                        textDisagree.text = "   >Deny access.";
                        if (possibleQuestNew.Length != 0)
                        {
                            if (deletedFirstChar==false)
                            {
                                selectedQuest = possibleQuestNew[randomQuest];
                                Debug.Log( selectedQuest);
                                fistChar = selectedQuest[0];
                                selectedQuest = selectedQuest.Substring(1);
                                Debug.Log( selectedQuest);
                                deletedFirstChar = true;
                            }
                           
                            textQuest.text = selectedQuest;
                        }
                        else
                        {
                            
                            textQuest.text = "";
                        }

                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            deletedFirstChar = false;
                            switcher = true;
                            human.beenRejected = true;
                            human.leavingSequence = true;
                            chekerDisagree = true;
                            randomAnswer = Random.Range(0, 5);
                            switch (randomAnswer)
                            {
                                case 1:
                                    humenSpeak.text = "That's... unfortunate.";
                                    break;
                                case 2:
                                    humenSpeak.text = "I will write a complain.";
                                    break;
                                case 3:
                                    humenSpeak.text = "What!? Do you know who i am!?";
                                    break;
                                case 4:
                                    humenSpeak.text = "Ugh, whatever. Fuck you.";
                                    break;
                                case 5:
                                    humenSpeak.text = "I will come back in morning.";
                                    break;
                                default:
                                    humenSpeak.text = "..."; 
                                    break;
                            }

                            StartCoroutine(human.HumanNahuiPoshel());
                            human.didImposterNahuiPoshel = true;
                        }
                    }
                    else
                    {
                       
                        if (deletedFirstChar==false)
                        {
                            selectedQuest = possibleQuestNew[randomQuest];
                            Debug.Log( selectedQuest);
                            fistChar = selectedQuest[0];
                            selectedQuest = selectedQuest.Substring(1);
                            Debug.Log( selectedQuest);
                            deletedFirstChar = true;
                        }
                        
                        textDisagree.text = "Deny access";
                        textQuest.text = "";
                        textQuest.text = "   >" + selectedQuest;



                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            deletedFirstChar = false;
                            List<string> tempAnswers = new List<string>(possibleQuestNew);
                            tempAnswers.RemoveAt(randomQuest);
                            possibleQuestNew = tempAnswers.ToArray();
                           
                            numberQuest = (int)char.GetNumericValue(fistChar);
                            
                            switcher = true;
                            selectQuest = true;
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
                                                            humenSpeak.text = "I want ot eat... right now.";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "It's none of you concern you worthless worm!";
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
                                                            humenSpeak.text = "...";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "To kidnapp some children, of course.";
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
                                                        humenSpeak.text = "I want to visit my children.";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "Why are you asking? I thought... nevermind. I'm here to take my children home for a couple of days.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "Can i just pass throught? I need to talk to my children urgently.";
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
                                                            humenSpeak.text = "They will soon know... oh they will know!";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "It doesn't matter. Is doesn't matter. It doesn't matter.";
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
                                                            humenSpeak.text = "No, they don't. But don't worry, i'll be gone in a couple of minutes.";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "Surely we can strike a deal, aye?";
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
                                                        humenSpeak.text = "Of course! I planned this visit about a week ago.";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "They do. I you don't believe me, call them. They will remember me.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "Not yet. But please, can i go in? You can call the staff and we will talk. I need to see my children.";
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
                                                            humenSpeak.text = "That someone might not be what you expect. Hehe...";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "There was someone! Now i'm all alone...";
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
                                                            humenSpeak.text = "Umm... No. I'm... alone here.";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "Look, is it that important? Can we just get this over with?";
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
                                                        humenSpeak.text = "No, you can check if you want.";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "Yes. I am here with my spouse.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "I'm here with my family. 4 people in total.";
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
                                                            humenSpeak.text = "What's that?";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "No! It's mine! Mine! MINE!";
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
                                                            humenSpeak.text = "I forgot to take it. Shit. Oh... sorry.";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "If i had it with me i wouldn't give it to you. Sorry, you just seem sketchy.";
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
                                                        humenSpeak.text = "Yes, here it is!";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "Here you go.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "Maybe. Let me check... Oh there it is! Here.";
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
                                                            humenSpeak.text = "They look so good when they sleep... i can't stand it!";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "Smart hunter uses darkness as his weapon. Don't you agree?";
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
                                                            humenSpeak.text = "I didn't have a choice. Look, i'm already in a bad mood. Let me in.";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "I prefer to stay awake at night.";
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
                                                        humenSpeak.text = "I don't have a lot of time in my schedule. Sorry for disturbing you in such time.";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "Just a bad coincidence. I would visit at daytime but here we are.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "Sorry for that. Shit happens.";
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
                                                            humenSpeak.text = "I am most familiar with it. Heh... haha... hahahaha! HAHAHAHA!";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = ":)";
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
                                                            humenSpeak.text = "What? Come again?";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "I... did hear about it. Why... why are you asking?";
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
                                                        humenSpeak.text = "Yes, i am! I wasn't expecting someone like that outside of horror movie. Horrible creature.";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "Kinda. My friend told me in a brief details. Sounds scary.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "Yes and i hope your camp prepared for a threat like that. I'm scared for my children.";
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
                                                            humenSpeak.text = "I don't care for those. Why have rules in the firts place?";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "Does it have something about cannibalism? Just asking...why are you looking at me like that?";
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
                                                            humenSpeak.text = "Oh my god. There were so many! I didn't read through all of them.";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "Ehh... kinda? I mean yes i do.";
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
                                                        humenSpeak.text = "Of course. I haven't red that much rules since the last time i played DarkRP. I mean there was a lot of rules.";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "I know enough. Didn't read throug the whole thing.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "Rules? No i don't. Do you have a list of those with you? I can read it on my to the shack.";
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
                                                            humenSpeak.text = "I want to but they end so fast...";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "I will acquire some.";
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
                                                            humenSpeak.text = "Yes... wh-what kind of question is that!?";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "I don't";
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
                                                        humenSpeak.text = "Of cource i do! Was it necessary to ask a question like that?";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "Actually, i don't. A friend of mine asked me to pick up his children.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "I do! 4 of them in fact!";
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
                                                            humenSpeak.text = "For as long as i please.";
                                                            srangeAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "As long as my needs are satisfied.";
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
                                                            humenSpeak.text = "I don't know.";
                                                            normalAnswer++;
                                                            break;
                                                        case 2:
                                                            humenSpeak.text = "That doesn't matter.";
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
                                                        humenSpeak.text = "For a couple of hours.";
                                                        break;
                                                    case 2:
                                                        humenSpeak.text = "For 20 minutes or so. This might change thoug.";
                                                        break;
                                                    case 3:
                                                        humenSpeak.text = "As far as i'm concerned, i can stay here for about a day so i'm going to do just that.";
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
															humenSpeak.text = "I want ot eat... right now.";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = "It's none of you concern you worthless worm!";
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
														humenSpeak.text = "I want to visit my children.";
														break;
													case 2:
														humenSpeak.text = "Why are you asking? I thought... nevermind. I'm here to take my children home for a couple of days.";
														break;
													case 3:
														humenSpeak.text = "Can i just pass throught? I need to talk to my children urgently.";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "...";
														break;
													case 2:
														humenSpeak.text = "To kidnapp some children, of course.";
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
															humenSpeak.text = "They will soon know... oh they will know!";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = "It doesn't matter. Is doesn't matter. It doesn't matter.";
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
														humenSpeak.text = "Of course! I planned this visit about a week ago.";
														break;
													case 2:
														humenSpeak.text = "They do. I you don't believe me, call them. They will remember me.";
														break;
													case 3:
														humenSpeak.text = "Not yet. But please, can i go in? You can call the staff and we will talk. I need to see my children.";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "No, they don't. But don't worry, i'll be gone in a couple of minutes.";
														break;
													case 2:
														humenSpeak.text = "Surely we can strike a deal, aye?";
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
															humenSpeak.text = "That someone might not be what you expect. Hehe...";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = "There was someone! Now i'm all alone...";
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
														humenSpeak.text = "No, you can check if you want.";
														break;
													case 2:
														humenSpeak.text = "Yes. I am here with my spouse.";
														break;
													case 3:
														humenSpeak.text = "I'm here with my family. 4 people in total.";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "Umm... No. I'm... alone here.";
														break;
													case 2:
														humenSpeak.text = "Look, is it that important? Can we just get this over with?";
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
															humenSpeak.text = "What's that?";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = "No! It's mine! Mine! MINE!";
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
														humenSpeak.text = "I don't have a lot of time in my schedule. Sorry for disturbing you in such time.";
														break;
													case 2:
														humenSpeak.text = "Just a bad coincidence. I would visit at daytime but here we are.";
														break;
													case 3:
														humenSpeak.text = "Sorry for that. Shit happens.";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "I forgot to take it. Shit. Oh... sorry.";
														break;
													case 2:
														humenSpeak.text = "If i had it with me i wouldn't give it to you. Sorry, you just seem sketchy.";
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
															humenSpeak.text = "They look so good when they sleep... i can't stand it!";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = "Smart hunter uses darkness as his weapon. Don't you agree?";
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
														humenSpeak.text = "I don't have a lot of time in my schedule. Sorry for disturbing you in such time.";
														break;
													case 2:
														humenSpeak.text = "Just a bad coincidence. I would visit at daytime but here we are.";
														break;
													case 3:
														humenSpeak.text = "Sorry for that. Shit happens.";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "I didn't have a choice. Look, i'm already in a bad mood. Let me in.";
														break;
													case 2:
														humenSpeak.text = "I prefer to stay awake at night.";
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
															humenSpeak.text = "I am most familiar with it. Heh... haha... hahahaha! HAHAHAHA!";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = ":)";
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
														humenSpeak.text = "Yes, i am! I wasn't expecting someone like that outside of horror movie. Horrible creature.";
														break;
													case 2:
														humenSpeak.text = "Kinda. My friend told me in a brief details. Sounds scary.";
														break;
													case 3:
														humenSpeak.text = "Yes and i hope your camp prepared for a threat like that. I'm scared for my children.";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "What? Come again?";
														break;
													case 2:
														humenSpeak.text = "I... did hear about it. Why... why are you asking?";
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
															humenSpeak.text = "I don't care for those. Why have rules in the firts place?";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = "Does it have something about cannibalism? Just asking...why are you looking at me like that?";
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
														humenSpeak.text = "Of course. I haven't red that much rules since the last time i played DarkRP. I mean there was a lot of rules.";
														break;
													case 2:
														humenSpeak.text = "I know enough. Didn't read throug the whole thing.";
														break;
													case 3:
														humenSpeak.text = "Rules? No i don't. Do you have a list of those with you? I can read it on my to the shack.";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "Oh my god. There were so many! I didn't read through all of them.";
														break;
													case 2:
														humenSpeak.text = "Ehh... kinda? I mean yes i do.";
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
															humenSpeak.text = "I want to but they end so fast...";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = "I will acquire some.";
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
														humenSpeak.text = "Of cource i do! Was it necessary to ask a question like that?";
														break;
													case 2:
														humenSpeak.text = "Actually, i don't. A friend of mine asked me to pick up his children.";
														break;
													case 3:
														humenSpeak.text = "I do! 4 of them in fact!";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "Yes... wh-what kind of question is that!?";
														break;
													case 2:
														humenSpeak.text = "I don't";
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
															humenSpeak.text = "For as long as i please.";
															srangeAnswer++;
															break;
														case 2:
															humenSpeak.text = "As long as my needs are satisfied.";
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
														humenSpeak.text = "For a couple of hours.";
														break;
													case 2:
														humenSpeak.text = "For 20 minutes or so. This might change thoug.";
														break;
													case 3:
														humenSpeak.text = "As far as i'm concerned, i can stay here for about a day so i'm going to do just that.";
														break;
												}

                                                break;
                                            case >= 75 and <= 150:
                                                switch (randansNB)
                                                {
													case 1:
														humenSpeak.text = "I don't know.";
														break;
													case 2:
														humenSpeak.text = "That doesn't matter.";
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
                switcher = true;
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
						humenSpeak.text = "Hi there!";
						break;
					case 2:
						humenSpeak.text = "Hello.";
						break;
					case 3:
						humenSpeak.text = "...";
						break;
					case 4:
						humenSpeak.text = "Can i pass throught?";
						break;
					case 5:
						humenSpeak.text = "Let me in.";
						break;
					default:
						humenSpeak.text = "My name is Yoshikage Kira. I'm 33 years old.";
						break;
				}
				chekerDisagree = false;
                seeCallPolice = false;
                humenSpeak.gameObject.SetActive(true);
                objBtns.SetActive(false);
			}
            canvas.SetActive(false);
        }
    }
        
}
