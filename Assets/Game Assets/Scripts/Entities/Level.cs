using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    public event Action OnLevelComplete = delegate { };

    [Inject]
    private UIManager _uiManager;

    private List<IResettable> _resettables = new();
    private List<IMovable> _movables = new();
    private List<IFinishable> _finishables = new();
    private List<IResourcePickupHandler> _pickups = new();

    public void AddMovable(IMovable movable)
    {
        _movables.Add(movable);
    }

    public void AddResettable(IResettable resettable)
    {
        _resettables.Add(resettable);
    }

    public void AddPickupHander(IResourcePickupHandler handler)
    {
        _pickups.Add(handler);
    }

    public void ResetLevel()
    {
        foreach (var resettable in _resettables)
        {
            resettable.ResetObject();
        }
    }

    public void AddFinishable(IFinishable finishable)
    {
        _finishables.Add(finishable);
        finishable.AddActionOnFinish(CheckFinish);
    }

    public void StartMovement()
    {
        foreach (var movable in _movables)
        {
            movable.StartMove();
        }
    }

    private void Start()
    {
        OnLevelComplete += _uiManager.ShowLevelCompletion;
    }

    private void CheckFinish()
    {
        int finished = 0;
        foreach (var finishable in _finishables)
        {
            finished += (finishable.HasFinished()) ? 1 : 0;
        }
        if (finished == _finishables.Count)
        {
            CompleteLevel();
            CollectResources();
        }
    }

    private void CollectResources()
    {
        int coinsCollected = 0;
        foreach (var pickup in _pickups)
        {
            var resources = pickup.GetPickups();
            foreach (var resource in resources)
            {
                coinsCollected += resource.amount;
            }
        }
        _uiManager.ShowCoinsCollected(coinsCollected);
        SaveManager.Data.coins += coinsCollected;
        SaveManager.Save();
    }

    private void CompleteLevel()
    {
        SaveManager.Data.level++;
        SaveManager.Save();
        OnLevelComplete();
    }
}
