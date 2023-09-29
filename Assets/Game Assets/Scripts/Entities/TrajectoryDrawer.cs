using System;
using UnityEngine;
using Zenject;

public class TrajectoryDrawer : MonoBehaviour
{
    public event Action<LineRenderer> OnDrawFinished = delegate { };
    public event Action OnDrawStarted = delegate { };

    [Inject]
    private InputManager _inputManager;

    [SerializeField]
    private PedType _pedType;
    [SerializeField]
    private LineRenderer _line;
    [SerializeField]
    private Animator _circleAnimator;
    [SerializeField]
    private Rigidbody _cursor;
    [SerializeField]
    private Vector3 _inputOffset;
    [SerializeField]
    private Vector3 _cursorOffset;
    [SerializeField]
    private float _cursorSpeedMult = 20;
    [SerializeField]
    private float _minPointDistance = 0.1f;

    private bool _isSelected = false;

    public void Select()
    {
        _isSelected = true;
        _line.positionCount = 1;
        _line.SetPosition(0, transform.position + _inputOffset);
        _cursor.isKinematic = false;
        OnDrawStarted();
    }

    public bool CheckFinish(Vector3 finishCoords, PedType pedType)
    {
        if (pedType != _pedType)
        {
            return false;
        }
        _line.positionCount += 1;
        _line.SetPosition(_line.positionCount - 1, finishCoords); 
        ResetInputCallback();
        return true;
    }


    private void OnEnable()
    {
        _inputManager.onMouseDown += CheckInputCallback;
        _inputManager.onMouseUp += ResetInputCallback;
    }
    private void OnDisable()
    {
        _inputManager.onMouseDown -= CheckInputCallback;
        _inputManager.onMouseUp -= ResetInputCallback;
    }

    private void ResetInputCallback()
    {
        _cursor.transform.localPosition = _cursorOffset;
        _cursor.isKinematic = true;
        if (_isSelected)
        {
            OnDrawFinished(_line);
        }
        _isSelected = false;
        _circleAnimator.gameObject.SetActive(true);
    }

    private void CheckInputCallback(Ray ray)
    {
        _circleAnimator.gameObject.SetActive(false);
        if (_isSelected == false)
        {
            return;
        }
        var groundTouchPosition = VectorMath.RayToPlanePoint(ray, Vector3.down);
        _cursor.velocity = (groundTouchPosition + _cursorOffset + _inputOffset - _cursor.position) * _cursorSpeedMult;
        _cursor.angularVelocity = Vector3.zero;
        if (_line.positionCount == 0)
        {
            _line.positionCount += 1;
            _line.SetPosition(_line.positionCount - 1, _cursor.position - _cursorOffset);
            return;
        }
        if (Vector3.Distance(_line.GetPosition(_line.positionCount - 1), _cursor.position - _cursorOffset) > _minPointDistance)
        {
            _line.positionCount += 1;
            _line.SetPosition(_line.positionCount - 1, _cursor.position - _cursorOffset);
        }
    }
}
