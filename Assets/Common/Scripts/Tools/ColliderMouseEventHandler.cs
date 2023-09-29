using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderMouseEventHandler : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onMouseDown;
    [SerializeField]
    private UnityEvent onMouseUp;
    [SerializeField]
    private UnityEvent onMouseUpAsButton;
    [SerializeField]
    private UnityEvent onMouseEnter;
    [SerializeField]
    private UnityEvent onMouseExit;
    [SerializeField]
    private UnityEvent onMouseOver;
    [SerializeField]
    private UnityEvent onMouseDrag;

    private void OnMouseDown()
    {
        onMouseDown?.Invoke();
    }

    private void OnMouseUp()
    {
        onMouseUp?.Invoke();
    }

    private void OnMouseUpAsButton()
    {
        onMouseUpAsButton?.Invoke();
    }

    private void OnMouseEnter()
    {
        onMouseEnter?.Invoke();
    }

    private void OnMouseExit()
    {
        onMouseExit?.Invoke();
    }

    private void OnMouseOver()
    {
        onMouseOver?.Invoke();
    }

    private void OnMouseDrag()
    {
        onMouseDrag?.Invoke();
    }
}
