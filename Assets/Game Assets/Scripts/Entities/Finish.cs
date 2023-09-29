using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Finish : MonoBehaviour, IResettable, IFinishable
{ 
    public event Action OnFinish = delegate { };

    [Inject]
    private Level _level;

    [SerializeField]
    private PedType _pedType;
    [SerializeField]
    private FinishSquare _finishSquare;

    public bool IsFinished { get; protected set; }


    public void CheckCursor(Collider collider)
    {
        var drawer = collider.gameObject.GetComponentInParent<TrajectoryDrawer>();
        if (drawer == null)
        {
            return;
        }
        drawer.CheckFinish(transform.position,_pedType);
    }

    public void CheckPlayer(Collider collider)
    {
        var drawer = collider.gameObject.GetComponentInParent<TrajectoryDrawer>();
        if (drawer == null)
        {
            return;
        }
        IsFinished = drawer.CheckFinish(transform.position, _pedType);
        _finishSquare.ShowFinishStatus(IsFinished);
        if (IsFinished)
        {
            OnFinish();
        }
    }

    public void ResetObject()
    {
        _finishSquare.ShowFinishStatus(false);
        IsFinished = false;
    }

    private void Start()
    {
        _level.AddResettable(this);
        _level.AddFinishable(this);
    }

    public void AddActionOnFinish(Action action)
    {
        OnFinish += action;
    }

    public void RemoveActionOnFinish(Action action)
    {
        OnFinish -= action;
    }

    public bool HasFinished()
    {
        return IsFinished;
    }
}
