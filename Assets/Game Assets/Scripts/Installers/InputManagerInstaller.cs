using Zenject;

public class InputManagerInstaller : MonoInstaller
{
    public InputManager inputManager;

    public override void InstallBindings()
    {
        Container.Bind<InputManager>().FromInstance(inputManager);
    }
}
