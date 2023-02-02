using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForse;
    [SerializeField] private float _rayCastGroundChecker;
    [SerializeField] private int _maxCountOfJumps;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Rigidbody _rb;
    private Vector3 _direction;

    private int _returnJumpsCounter;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _returnJumpsCounter = _maxCountOfJumps;
    }

    private void Update()
    {
        if (IsGrounded())
        {
            _maxCountOfJumps = _returnJumpsCounter;
        }
        Debug.DrawRay(_rb.transform.position, Vector3.down * _rayCastGroundChecker, Color.red);
    }

    private void FixedUpdate()
    {
        _rb.velocity = new Vector3(_direction.x * _speed, _rb.velocity.y, 0f);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    public void Say()
    {
        Debug.Log("I can sayeing");
    }
    public void Jump()
    {
        if (IsGrounded() || _maxCountOfJumps > 1)
        {
            _rb.AddForce(Vector3.up * _jumpForse, ForceMode.Impulse);
            _maxCountOfJumps--;
            Debug.Log($"Осталось {_maxCountOfJumps} прыжков.");
        }
    }
    private bool IsGrounded()
    {
        var hit = Physics.Raycast(transform.position, Vector3.down , 2.2f, _groundLayer);

        return hit;
    }

    public void PortalCastL()
    {
        var cursorPos = Input.mousePosition;

        var hit = Physics.Raycast(transform.position, cursorPos, out RaycastHit raycastInfo);
        Debug.Log(raycastInfo + "Left mouse button");

    }
    public void PortalCastR()
    {
        var cursorPos = Input.mousePosition;

        var hit = Physics.Raycast(transform.position, cursorPos, out RaycastHit raycastInfo);
        Debug.Log(raycastInfo+ "Right mouse button");
    }
} 