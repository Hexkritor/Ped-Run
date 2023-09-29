using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    public Level level;

    public override void InstallBindings()
    {
        Container.Bind<Level>().FromInstance(level);
    }
}
