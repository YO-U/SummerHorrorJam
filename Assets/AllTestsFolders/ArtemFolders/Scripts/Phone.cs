using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    private CameraMove cm;
    private HumanWalkToWindow humans;
    public TextMeshProUGUI textToFade;
    public GameObject btnsEmp;
    public Button button1;
    public Button button2;
    public KeyCode switchKey = KeyCode.W;
    public KeyCode selectKey = KeyCode.Space;
    private int currentButtonIndex = 0;
    private bool chekerCanvas = false;

    private void Start()
    {
        cm = FindObjectOfType<CameraMove>();
        humans = FindObjectOfType<HumanWalkToWindow>();

    }

    private void Update()
    {
        if (cm.right && humans.hCreatedCh == false && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(FadeInOutText());
        }
        
            if (Input.GetKeyDown(switchKey))
            {
                currentButtonIndex = 1 - currentButtonIndex; // Меняем индекс на противоположный

                // Включаем/выключаем Image в зависимости от текущего индекса
               // button1.image.gameObject.SetActive(currentButtonIndex == 0);
                button1.GetComponent<Image>().GameObject().SetActive(currentButtonIndex == 0);
                button2.GetComponent<Image>().GameObject().SetActive(currentButtonIndex == 1);
               // button2.image.gameObject.SetActive(currentButtonIndex == 1);
            }
            
            // Выбор текущей кнопки
            if (Input.GetKeyDown(selectKey))
            {
                if (currentButtonIndex == 0)
                {
                    
                }
                else if (currentButtonIndex == 1)
                {
                    btnsEmp.SetActive(false);
                    
                }
            } 
            if (cm.right && humans.hCreatedCh && Input.GetKeyDown(KeyCode.Space) && chekerCanvas ==false)
            {
                btnsEmp.SetActive(true);
                chekerCanvas = true;
            }
    }
    private IEnumerator FadeInOutText()
    {
        Color initialColor = textToFade.color;
        initialColor.a = 0f; // Устанавливаем начальную прозрачность на 0

        textToFade.color = initialColor;
        textToFade.gameObject.SetActive(true);

        // Плавное появление текста
        float duration = 1f; // Длительность анимации (в секундах)
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);
            Color newColor = textToFade.color;
            newColor.a = alpha;
            textToFade.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ожидание 5 секунд
        yield return new WaitForSeconds(5f);

        // Плавное исчезновение текста
        elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            Color newColor = textToFade.color;
            newColor.a = alpha;
            textToFade.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textToFade.gameObject.SetActive(false); // Отключаем объект после исчезновения
    }
}
