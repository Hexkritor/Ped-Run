using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PickupContainer : MonoBehaviour, IResourcePickupHandler, IResettable
{
    [Inject]
    private Level _level;

    private List<ResourcePickup> _pickedResources = new();

    public void OnEnable()
    {
        _level.AddResettable(this);
        _level.AddPickupHander(this);
    }

    public void ResetObject()
    {
        foreach (var resource in _pickedResources)
        {
            resource.gameObject.SetActive(true);
        }
        _pickedResources.Clear();
    }

    public void Pickup(ResourcePickup resourcePickup)
    {
        _pickedResources.Add(resourcePickup);
        resourcePickup.gameObject.SetActive(false);
    }

    public ResourcePickup[] GetPickups()
    {
        return _pickedResources.ToArray();
    }
}
