using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    private CameraMove cm;
    [SerializeField] private EndingController endingController;
	[SerializeField] private OpenCloseObject openCloseObject;
    private HumanWalkToWindow humans;
    public TextMeshProUGUI textToFade;
    public GameObject btnsEmp;
    public Button button1;
    public TextMeshProUGUI txtBtn1;
    public Button button2;
    public TextMeshProUGUI txtBtn2;
    public KeyCode switchKey = KeyCode.W;
    public KeyCode selectKey = KeyCode.Space;
    public bool call = false;
    private int currentButtonIndex = 0;
    private bool chekerCanvas = false;
	[SerializeField] private MonsterKill monsterKill;

    private void Start()
    {
        cm = FindObjectOfType<CameraMove>();
        humans = FindObjectOfType<HumanWalkToWindow>();

    }

    private void Update()
    {
		if (cm.right && humans.hCreatedCh == false && Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(FadeInOutText());
		}
		if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentButtonIndex = 1 - currentButtonIndex; // Меняем индекс на противоположный

			if (currentButtonIndex == 1)
			{
				txtBtn1.text = "   >Cancel";
				txtBtn2.text = "Call police";
			}
			else
			{
				txtBtn1.text = "Cancel";
				txtBtn2.text = "   >Call police";
			}
		}

		// Выбор текущей кнопки
		if (Input.GetKeyDown(KeyCode.Space) && cm.right)
		{
			if (currentButtonIndex == 0 && humans.wasImposterEncountered)
			{
				if (humans.humansSinceTheImposter < 1 && !openCloseObject.windowOpened)
				{
					humans.wasPoliceCalledInTime = true;
					endingController.endingNumber = 0;
					call = true;
				}
				else if (humans.currentHuman == humans.imposter && openCloseObject.windowOpened)
				{
					humans.wasPoliceCalledInTime = true;
					endingController.endingNumber = 5;
					call = true;
					StartCoroutine(monsterKill.SpawnMonsterRemoveHuman());
				}
				else
				{
					humans.wasPoliceCalled = true;
					endingController.endingNumber = 1;
					call = true;
				}
			}
			else if (currentButtonIndex == 0 && !humans.wasImposterEncountered)
			{
				humans.wasPoliceCalledEarly = true;
				endingController.endingNumber = 2;
				call = true;
			}
			else if (currentButtonIndex == 1)
			{
				btnsEmp.SetActive(false);
			}
		}
		if (cm.right && humans.hCreatedCh && Input.GetKeyDown(KeyCode.E))
		{
			btnsEmp.SetActive(true);
		}

		if (cm.right == false)
		{
			btnsEmp.SetActive(false);
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
