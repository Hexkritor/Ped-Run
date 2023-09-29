using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using System;
using Zenject;

public class InputManager : MonoBehaviour
{
    public event Action<Ray> onMouseDown = delegate {};
    public event Action onMouseUp = delegate { };

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private Cinemachine.CinemachineVirtualCamera _virtualCamera;

    Ray _clickRay;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        GetCamera();
        GetBaseClickRay();
        Input.multiTouchEnabled = false;
    }

    private void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.GetMouseButton(0))
        {
            onMouseDown?.Invoke(GetInputRay());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            onMouseUp?.Invoke();
        }
    }

    private void GetBaseClickRay()
    {
        var screenCenter = new Vector2(0.5f, 0.5f);
        _clickRay = _camera.ViewportPointToRay(screenCenter);
    }

    public Ray GetInputRay()
    {
        GetCamera();
        return _camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.transform.position.z));
    }

    public Ray GetCenterPointRay()
    {
        GetCamera();
        return _camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, _camera.transform.position.z));
    }

    private void GetCamera()
    {
        if (!_camera)
        {
            _camera = Camera.main;
        }
        if (!_virtualCamera)
        {
            _virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();
        }
    }
}