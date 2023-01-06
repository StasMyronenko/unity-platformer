using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] public int _heal;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    
    private PlayerMover _playerMover;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _playerMover = collider.GetComponent<PlayerMover>();
        if (_playerMover != null)
        {
            _playerMover.TakeHeal(_heal);
            Destroy(_spriteRenderer);
            _heal = 0;
        }
    }
}
