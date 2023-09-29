using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLocker : MonoBehaviour
{
    [SerializeField]
    private static int _targetFPS = 60;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void SetFPS()
    {
        Application.targetFrameRate = _targetFPS;
        print(Application.targetFrameRate);
    }
}
