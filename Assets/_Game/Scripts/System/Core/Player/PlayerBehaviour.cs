using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerBehaviour : MonoBehaviour
{
    PlayerMovement _playerMovement;
    PlayerInput _playerInput;

    bool _triggerFirstMove = false;

    Vector3 lastCollidePosition;

    public event Action OnFirstJump;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInput = GetComponent<PlayerInput>();
    }

    public void FirstJump()
    {
        if (_triggerFirstMove) return;

        _triggerFirstMove = true;
        OnFirstJump?.Invoke();
        _playerMovement.Jump();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_triggerFirstMove) return;
        Vector3 contactNormal = collision.GetContact(0).normal;
        if (Mathf.Abs(contactNormal.x) == 1f) return;

        lastCollidePosition = transform.position;
        _playerMovement.Jump();

    }

    public void OnGameOver()
    {
        gameObject.SetActive(false);
    }

}
