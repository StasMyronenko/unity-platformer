using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMover : MonoBehaviour
{
    [Header("Stats")] [SerializeField] private int _maxHp;
    private int _currentHp;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _hpText;

    [Header("Movements")]
    [SerializeField] public float _groundChekerRadius;
    [SerializeField] public Transform _groundChaker;
    [SerializeField] public Transform _transform;
    [SerializeField] public int _diePositionY;
    [SerializeField] public LayerMask _whatIsGround;
    [SerializeField] public Rigidbody2D _rigidbody;
    [SerializeField] public float _speed;
    [SerializeField] public float _jumpPower;

    [SerializeField] public Collider2D _headCillider;
    [SerializeField] public Transform _cellChecker;
    [SerializeField] public float _cellCheckerRadius;

    
    [Header("Animations")]
    public Animator _animator;
    public string _runAnimationKey;
    public string _crouchAnimationKey;
    public string _jumpAnimationKey;
    
    private bool _facingRight = true;
    private static readonly int Speed = Animator.StringToHash("Speed");

    void Start()
    {
        // _transform.position = Vector2.zero;
        _currentHp = _maxHp;
        _slider.maxValue = _maxHp;
        _slider.value = _currentHp;
        _hpText.text = _currentHp.ToString() + "/" + _maxHp.ToString();
    }

    public void TakeDamage(int damage)
    {
        _currentHp -= damage;
        _slider.value = _currentHp;
        _hpText.text = _currentHp.ToString() + "/" + _maxHp.ToString();
        if (_currentHp <= 0)
        {
            Die();
        }
    }
    
    public void TakeHeal(int heal)
    {
        _currentHp = _currentHp + heal > _maxHp ? _maxHp : _currentHp + heal;
        _slider.value = _currentHp;
        _hpText.text = _currentHp.ToString() + "/" + _maxHp.ToString();
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // SceneManager.LoadScene("FirstScene");
    }

    // Update is called once per frame
    void Update()
    {
        var grounded = Physics2D.OverlapCircle(
            _groundChaker.position,
            _groundChekerRadius,
            _whatIsGround);

        float direction = Input.GetAxisRaw("Horizontal");
        _animator.SetInteger(_runAnimationKey, Convert.ToInt16(direction));
        
        _animator.SetBool(_crouchAnimationKey, !_headCillider.enabled);
        
        _animator.SetBool(_jumpAnimationKey, !Convert.ToBoolean(grounded));
        
        if (grounded)
        {
            _rigidbody.velocity = new Vector2(_speed * direction, _rigidbody.velocity.y);  // Моментально задаємо швидкість
            // _transform.position = new Vector2(_transform.position.x + _speed * x, _transform.position.y);  // Проходить через стіни при великій швидкості)))
            // _transform.Translate(new Vector2(_speed * x, 0), Space.World);  // Додаємо до поточного значення
            // _rigidbody.AddForce(new Vector2(_speed * x, 0));  // Плавно додаємо і зменшуємо швидкість
            // _rigidbody.AddRelativeForce(new Vector2(_speed * x, 0)); // Плавно додаємо і зменшуємо швидкість, але в локальному просторі

        }

        if ((direction < 0 && _facingRight) || (direction > 0 && !_facingRight))
        {
            Flip();
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpPower);
        }

        bool cellAbove = Physics2D.OverlapCircle(
            _cellChecker.position,
            _cellCheckerRadius,
            _whatIsGround);
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _headCillider.enabled = false;
        }
        else if (!cellAbove)
        {
            _headCillider.enabled = true;
        }

        if (_transform.position.y < _diePositionY)
        {
            Die();
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
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_cellChecker.position, _cellCheckerRadius);
    }
}
