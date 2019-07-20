﻿using UnityEngine;
using Zenject;

public class RefundForDestroyLogic : MonoBehaviour
{
    [Inject]
    private GameData _gameData;
    [Inject]
    private SignalBus _signalBus;

    void Start()
    {
        _signalBus.Subscribe<UnitDestroyedBySignal>(OnUnitDestroyedBy);
    }

    private void OnUnitDestroyedBy(UnitDestroyedBySignal signal)
    {
        if (signal.striker.teamId != signal.hitUnit.teamId)
        {
            _gameData.TryChangePlayerMoney(signal.striker.teamId, signal.hitUnit.cost / 2);
        }
    }

    void Stop()
    {
        _signalBus.Unsubscribe<UnitDestroyedBySignal>(OnUnitDestroyedBy);
    }
}
