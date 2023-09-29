using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSquare : MonoBehaviour
{
    [SerializeField]
    private Animator _emptySpriteAnimator;
    [SerializeField]
    private string _finishAnimationName;
    [SerializeField]
    private string _idleAnimationName;

    public void ShowFinishStatus(bool isPlayerFinished)
    {
        _emptySpriteAnimator.Play((isPlayerFinished == true) ? _finishAnimationName : _idleAnimationName);
    }
}
