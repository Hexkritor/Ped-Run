using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Ped : MonoBehaviour
{
    [Inject]
    private Level _level;

    public TrajectoryDrawer trajectoryDrawer;
    public MovablePlayer movablePlayer;
    public PickupContainer pickupContainer;

    public void Start()
    {
        trajectoryDrawer.OnDrawFinished += movablePlayer.SetLine;
        trajectoryDrawer.OnDrawFinished += (_) => { _level.StartMovement(); } ;
        trajectoryDrawer.OnDrawStarted += _level.ResetLevel;
        _level.AddResettable(movablePlayer);
        _level.AddMovable(movablePlayer);
        _level.OnLevelComplete += movablePlayer.StartDance;
    }
}
