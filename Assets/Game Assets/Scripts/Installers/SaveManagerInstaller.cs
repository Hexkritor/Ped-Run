using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SaveManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SaveManager.Load();
    }
}
