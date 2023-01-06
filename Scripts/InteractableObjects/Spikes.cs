using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _damageDelay;

    private PlayerMover _playerMover;
    
    private float _lastDamageTime;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        _playerMover = collider.GetComponent<PlayerMover>();
        if (_playerMover != null)
        {
            _playerMover.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }

    private void Update()
    {
        if (Time.time - _lastDamageTime > _damageDelay && _playerMover != null)
        {
            _playerMover.TakeDamage(_damage);
            _lastDamageTime = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
         PlayerMover playerMover = collider.GetComponent<PlayerMover>();
        _playerMover = collider.GetComponent<PlayerMover>();
        if (_playerMover == playerMover)
        {
            _playerMover = null;
        }
    }
}
