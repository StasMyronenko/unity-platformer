using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public float _groundChekerRadius;
    public Transform _groundChaker;
    public Transform _transform;
    public LayerMask _whatIsGround;
    public Rigidbody2D _rigidbody;
    public float _speed;
    public float _jumpPower;
    
    private bool _facingRight = true;
    
    void Start()
    {
        // _transform.position = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        var grounded = Physics2D.OverlapCircle(
            _groundChaker.position,
            _groundChekerRadius,
            _whatIsGround);
        float x = Input.GetAxisRaw("Horizontal");

        if (grounded)
        {
            _rigidbody.velocity = new Vector2(_speed * x, _rigidbody.velocity.y);
        }

        if ((x < 0 && _facingRight) || (x > 0 && !_facingRight))
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
        }
    }

    private void Flip()
    {
        _transform.Rotate(0,180,0);
        _facingRight = !_facingRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundChaker.position, _groundChekerRadius);
    }
}
