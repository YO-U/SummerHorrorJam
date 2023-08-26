using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Talk : MonoBehaviour
{
    private HumanWalkToWindow humans;
    private OpenCloseObject opClWindow;
    public CanvasGroup cnvs;
    private bool cnvsChecker=false;
    public GameObject txtHum;
    public GameObject quest;
    public GameObject txtPlayer;
    private string startTxt = "Здрасте пропустите?";
    public string[,] allHumTxt = new string[10,4]
    {
        {"Зачем?",
            "Да так ","По приколу","Без прикола"},
        
        {"Почему?",
            "прикол","чо","ж"},
        
        {"Для чего?",
            "па","авп","вяпывап"},
        
        {"Схерали?",
            "яавпяа","пявап","яапяап"},
        
        {"С какого?",
            "тспртспр","яавпяавп","стпртср"},
        
        {"А чо?",
            "тспртс","тспртс","тсрптс"},
        
        {"А не чо?",
            "тспртсрт","тспртсрпт","тсрптс"},
        
        {"А еще чо?",
            "тсрптспрт","тсрптсрт","тспртрпьтоь"},
        
        {"Друг?",
            "яавпявап","стспртсрптр","явапяавпяа"},
        
        {"Враг?",
            "срьсрьо","явапрьвпр","явпитср"}
    };

    private void Start()
    {
        cnvs.alpha = 0f;
        humans = FindObjectOfType<HumanWalkToWindow>();
        opClWindow = FindObjectOfType<OpenCloseObject>();
       
      //  a = Random.Range(1, 10);
      //  b = Random.Range(2, 3);
    }

    private void Update()
    {
        if (humans.hCreatedCh == true && opClWindow.windowOpened == true)
        {
           
            cnvs.alpha = 1f;
            cnvsChecker = true;
            txtHum.GetComponent<TextMeshPro>().text = startTxt;
            int i = 1;
            if(i<12 && 0 != i%2)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                   
                    txtHum.SetActive(false);
                    txtPlayer.SetActive(true);
               //     quest.GetComponent<TextMeshPro>().text = allHumTxt[a, 1];
                    i++;
                }
            }
            if(i<12 && 0 == i%2)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    
                    txtHum.SetActive(true);
                 //   txtHum.GetComponent<TextMeshPro>().text = allHumTxt[a, b]; 
                    txtPlayer.SetActive(false);
                    i++;
                }
            }
        }
        else
        {
            cnvs.alpha = 0f;
            cnvsChecker = false;
        }
        
    }
}
