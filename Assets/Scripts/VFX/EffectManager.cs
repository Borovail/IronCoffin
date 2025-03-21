﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public interface IEngineEffect
{
    public void StartEffect();
    public void StopEffect();
}

public interface IShootEffect
{
    public void ShootEffect();
}

public interface IEndGameEffect
{
    public void EndEffect();
}


namespace Assets.Scripts
{
    public class EffectManager : Singleton<EffectManager>
    {
        private IEnumerable<IEngineEffect> _engineEffects;
        private IEnumerable<IShootEffect> _shootEffects;
        private IEndGameEffect _endEffect;

        private void Start()
        {
            _engineEffects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IEngineEffect>();
            _shootEffects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IShootEffect>();
            _endEffect = FindFirstObjectByType<EndGame>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                foreach (var engineEffect in _engineEffects)
                {
                    engineEffect.StartEffect();
                }

            if (Input.GetMouseButtonDown(1))
                foreach (var engineEffect in _engineEffects)
                {
                    engineEffect.StopEffect();
                }

            if (Input.GetKeyDown(KeyCode.RightShift))
                foreach (var shootEffect in _shootEffects)
                {
                    shootEffect.ShootEffect();
                }
            if (Input.GetKeyDown(KeyCode.G))
                _endEffect.EndEffect();
        }
    }
}