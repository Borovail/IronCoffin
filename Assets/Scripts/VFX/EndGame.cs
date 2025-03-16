using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using UnityEngine;

namespace Assets.Scripts
{
    public class EndGame : MonoBehaviour, IEndGameEffect
    {
        private IEndGameEffect _timeSlowEffect;
        private IEndGameEffect _fogEffect;
        private FirstPersonController _controller;

        private void Start()
        {
            _timeSlowEffect = FindFirstObjectByType<TimeSlow>();
            _fogEffect = FindFirstObjectByType<FogEffect>();
            _controller = FindFirstObjectByType<FirstPersonController>();
        }


        public void EndEffect()
        {
            StartCoroutine(EndEffectCoroutine());
        }

        private IEnumerator EndEffectCoroutine()
        {
            _fogEffect.EndEffect();
            yield return StartCoroutine(SoundManager.Instance.ApplyDeafEffect());
            yield return new WaitForSecondsRealtime(2f);

            _timeSlowEffect.EndEffect();
            yield return StartCoroutine(SoundManager.Instance.StartFinalMusic());
            _controller.gameObject.SetActive(false);
        }
    }
}