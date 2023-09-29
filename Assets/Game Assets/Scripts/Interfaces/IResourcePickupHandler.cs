public interface IResourcePickupHandler
{
    public abstract void Pickup(ResourcePickup resourcePickup);

    public abstract ResourcePickup[] GetPickups();
}