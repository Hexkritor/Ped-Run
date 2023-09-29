using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIManagerInstaller : MonoInstaller
{
    public UIManager uiManager;

    public override void InstallBindings()
    {
        Container.Bind<UIManager>().FromInstance(uiManager);
    }
}
