using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePickup : MonoBehaviour
{
    [SerializeField]
    private ResourceType _resourceType;
    [SerializeField]
    private int _amount = 1;

    public ResourceType resourceType => _resourceType;
    public int amount => _amount;

    public void Pickup(Collider collider)
    {
        IResourcePickupHandler pickupHandler;
        if (collider.gameObject.TryGetComponent(out pickupHandler) == false)
        {
            return;
        }
        pickupHandler.Pickup(this);
    }
}
