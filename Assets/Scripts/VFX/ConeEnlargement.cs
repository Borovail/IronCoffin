using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class ConeEnlargement : MonoBehaviour,IEndGameEffect
    {
        public float growthSpeed = 1f; // Скорость роста
        public float maxSize = 2f; // Максимальный размер


        private IEnumerator GrowConeCoroutine()
        {
            float elapsedTime = 0f; // Время прошедшее с начала роста
            Vector3 startSize = transform.localScale; // Начальный размер (исходный размер)

            // Плавный рост до максимального размера
            while (elapsedTime < 1f)
            {
                elapsedTime += Time.unscaledDeltaTime * growthSpeed; // Увеличиваем время
                transform.localScale = Vector3.Lerp(startSize, new Vector3(10,10,10), elapsedTime);
                yield return null; // Ждём следующий кадр
            }

            // После окончания корутины устанавливаем точный максимальный размер
            transform.localScale = startSize * maxSize;
        }


        public void EndEffect()
        {
            StartCoroutine(GrowConeCoroutine());
        }
    }
}