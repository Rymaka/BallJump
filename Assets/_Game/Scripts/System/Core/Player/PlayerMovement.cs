using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _jumpTime = 1f;
    [SerializeField] private float _jumpDistance = 8f;
    [SerializeField] private float _maxPosX = 3f;
    [SerializeField] private DistanceController _distanceController;
    private float[] _distanceList;
    private int _jumpCount = 0;

    [Space]
    [Range(0, 1)] [SerializeField] private float _smoothHorizontalTime = 0.2f;

    private float _xVelocity;
    private float _yVelocity;
    private float _yGravity;
    private float _jumpHeight = 5f;

    private float _elapsedTime = 0;
    private float _startVal, _endVal;

    private Vector3 _playerPos;

    private Rigidbody _rb;

    private float GetJumpHalfTime => _jumpTime * .5f;

    private void Awake()
    {
        _distanceList = _distanceController.distanceList;
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        PlayerInput.OnPointerDrag += HorizontalMovement;
    }
    private void OnDisable()
    {
        PlayerInput.OnPointerDrag -= HorizontalMovement;
    }

    private void Start()
    {
        // calculate vertical force
        _yVelocity = (_jumpHeight / GetJumpHalfTime) * 2;

        // calculate & apply gravity
        _yGravity = -_yVelocity / GetJumpHalfTime;
        Physics.gravity = Vector3.up * _yGravity;
    }

    private void Update()
    {
        // calculate percentage
        _elapsedTime += Time.deltaTime;
        float timePercentage = _elapsedTime / _jumpTime;

        Move(timePercentage);
    }

    public void Jump()
    {
        _jumpDistance = _distanceList[_jumpCount];
        _elapsedTime = 0;
        _startVal = transform.position.z;
        _endVal += _jumpDistance;
 
        // apply jump
        _rb.velocity = new Vector3(_rb.velocity.x, _yVelocity, _rb.velocity.z);
        if(_jumpCount > _distanceList.Length)
        {
            _jumpCount = 0;
        }
        else
        {
            _jumpCount++;
        }

    }
    public void HorizontalMovement(float xMovement)
    {
        _playerPos.x = xMovement * _maxPosX;
    }

    
    private void Move(float percentage)
    {
        if (transform.position.z == _endVal) return;

        _playerPos.z = Mathf.Lerp(_startVal, _endVal, percentage);
        float xPos = Mathf.SmoothDamp(transform.position.x, _playerPos.x, ref _xVelocity, _smoothHorizontalTime);

        transform.position = new Vector3(xPos, transform.position.y, _playerPos.z);
    }
}
