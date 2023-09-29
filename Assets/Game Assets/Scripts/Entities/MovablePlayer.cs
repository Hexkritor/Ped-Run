using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlayer : MonoBehaviour, IResettable, IMovable
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private PedType _pedType;
    [SerializeField]
    private float _baseMoveSpeed;
    [SerializeField]
    private float _doubleSpeedTime = 15;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private LineRenderer _line;
    private float _distancePassed;
    private float _timePassed;
    private int _nextLineIndex;
    private bool _isMoving;

    private const string MoveSpeed = "MoveSpeed";
    private const string Dance = "Dance";

    public void Start()
    {
        _startPosition = _rigidbody.position;
        _startRotation = _rigidbody.rotation;
    }

    public void StartMove()
    {
        if (_line == null)
        {
            return;
        }
        _distancePassed = 0;
        _timePassed = 0;
        _nextLineIndex = 1;
        _isMoving = true;
    }

    public void SetLine(LineRenderer line)
    {
        if (line == null)
        {
            return;
        }
        _line = line;
    }

    public void ResetMove()
    {
        _isMoving = false;
        _rigidbody.position = _startPosition;
        _rigidbody.rotation = _startRotation;
        _rigidbody.isKinematic = true;
        _rigidbody.isKinematic = false;
        _animator.SetFloat(MoveSpeed, 0);
    }

    public void ResetObject()
    {
        ResetMove();
    }

    public void StartDance() 
    {
        _animator.SetTrigger(Dance);
        _isMoving = false;
    }

    public void Crash()
    {
        ResetMove();
    }

    private void FixedUpdate()
    {
        if (_isMoving == false)
        {
            return;
        }

        Move();
    }

    private void Move()
    {
        if (_line == null)
        {
            _isMoving = false;
            return;
        }
        if (_nextLineIndex >= _line.positionCount)
        {
            _isMoving = false;
            _animator.SetFloat(MoveSpeed, 0);
            return;
        }
        _timePassed += Time.fixedDeltaTime;
        float distanceToMove = _baseMoveSpeed * ((_timePassed >= _doubleSpeedTime) ? 2 : 1) * Time.fixedDeltaTime;
        _distancePassed += distanceToMove;
        float segmentDistance = Vector3.Distance(_line.GetPosition(_nextLineIndex - 1), _line.GetPosition(_nextLineIndex));
        if (_distancePassed >= segmentDistance)
        {
            _distancePassed -= segmentDistance;
            _nextLineIndex++;
            if (_nextLineIndex >= _line.positionCount)
            {
                _isMoving = false;
                _animator.SetFloat(MoveSpeed, 0);
                transform.rotation = Quaternion.Euler(Vector3.Scale(transform.rotation.eulerAngles, new Vector3(0, 1, 1)));
                return;
            }
            segmentDistance = Vector3.Distance(_line.GetPosition(_nextLineIndex - 1), _line.GetPosition(_nextLineIndex));
        }
        _rigidbody.MovePosition(
            Vector3.Lerp(_line.GetPosition(_nextLineIndex - 1), _line.GetPosition(_nextLineIndex), _distancePassed / segmentDistance));
        transform.LookAt(_line.GetPosition(_nextLineIndex));
        _animator.SetFloat(MoveSpeed, 1);
    }
}
