﻿using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class UnitDisplayHpController : IInitializable, IDisposable
{
    [Inject]
    private UnitModel _model;
    [Inject]
    private StripeBar _hpBar;
    [Inject]
    private CoroutinesManager _coroutimeManager;
    public void Initialize()
    {
        _model.HealthUpdated += OnHpUpdated;
        UpdateHp(_model.hp);
    }

    private void OnHpUpdated(int hp)
    {
        UpdateHp(hp);

        if (hp <= 0)
        {
            _model.HealthUpdated -= OnHpUpdated;
            _coroutimeManager.StartCoroutine(WaitAndHideCoroutine());
        }
    }

    private void UpdateHp(int hp)
    {
        var percent = 100 * hp / _model.maxHp;
        _hpBar.SetPercent(percent);
    }

    private IEnumerator WaitAndHideCoroutine()
    {
        yield return new WaitForSeconds(3f);
        _hpBar.enabled = false;
    }

    public void Dispose()
    {
        _model.HealthUpdated -= OnHpUpdated;
    }
}