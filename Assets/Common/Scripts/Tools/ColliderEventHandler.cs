using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEventHandler : MonoBehaviour
{
    public LayerMask filterByLayer;
    [Tooltip("Empty string - no filter")]
    public string filterByTag;
    public UnityEvent<Collision> onCollisionEnter;
    public UnityEvent<Collision> onCollisionStay;
    public UnityEvent<Collision> onCollisionExit;
    public UnityEvent<Collider> onTriggerEnter;
    public UnityEvent<Collider> onTriggerStay;
    public UnityEvent<Collider> onTriggerExit;

    public void OnCollisionEnter(Collision collision)
    {
        if (CollisionFilterCheck(collision.gameObject))
           onCollisionEnter?.Invoke(collision);
    }
    public void OnCollisionStay(Collision collision)
    {
        if (CollisionFilterCheck(collision.gameObject))
            onCollisionStay?.Invoke(collision);
    }
    public void OnCollisionExit(Collision collision)
    {
        if (CollisionFilterCheck(collision.gameObject))
            onCollisionExit?.Invoke(collision);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (CollisionFilterCheck(other.gameObject))
            onTriggerEnter?.Invoke(other);
    }
    public void OnTriggerStay(Collider other)
    {
        if (CollisionFilterCheck(other.gameObject))
            onTriggerStay?.Invoke(other);
    }
    public void OnTriggerExit(Collider other)
    {
        if (CollisionFilterCheck(other.gameObject))
            onTriggerExit?.Invoke(other);
    }

    private bool CollisionFilterCheck(GameObject target)
    {
        return ((1 << target.gameObject.layer) & filterByLayer.value) > 0 &&
            (filterByTag == string.Empty || filterByTag == target.gameObject.tag);
    }
}
