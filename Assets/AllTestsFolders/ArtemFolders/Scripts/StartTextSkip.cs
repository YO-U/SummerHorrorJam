using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextSkip : MonoBehaviour
{
  public CanvasGroup canvasGroup;
  public float fadeDuration = 2.0f; // Длительность затухания в секундах

  private float timer = 0f;
  private bool isFading = false;

  private void Start()
  {
    // Получаем компонент CanvasGroup, если он не назначен
    if (canvasGroup == null)
    {
      canvasGroup = GetComponent<CanvasGroup>();
    }

    // Вызываем метод для начала затухания
    StartFade();
  }

  private void Update()
  {
    // Проверяем, идет ли затухание
    if (isFading)
    {
      // Увеличиваем таймер
      timer += Time.deltaTime;

      // Вычисляем текущий альфа-канал на основе времени
      float alpha = 1.0f - Mathf.Clamp01(timer / fadeDuration);

      // Устанавливаем альфа-канал CanvasGroup
      canvasGroup.alpha = alpha;

      // Если затухание завершено, отключаем объект Canvas и останавливаем затухание
      if (alpha <= 0.0f)
      {
        canvasGroup.gameObject.SetActive(false);
        isFading = false;
      }
    }
  }

  public void StartFade()
  {
    // Включаем объект Canvas и начинаем затухание
    canvasGroup.gameObject.SetActive(true);
    canvasGroup.alpha = 1.0f; // Устанавливаем начальный альфа-канал

    timer = 0f;
    isFading = true;
  }
}
