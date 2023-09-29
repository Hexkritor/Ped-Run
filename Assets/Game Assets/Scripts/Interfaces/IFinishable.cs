using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFinishable 
{
    public void AddActionOnFinish (Action action);
    public void RemoveActionOnFinish(Action action);
    public bool HasFinished();
}
