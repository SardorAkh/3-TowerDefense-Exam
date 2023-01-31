using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxHealth;
    private float _currentHealth;
    [SerializeField] private HealthBar _healthBar;
    public Transform[] points;
    [SerializeField] private int _currentPoint = 0;
    [SerializeField] private int _reward;
    [SerializeField] private int _damageToGameHealth;
    [SerializeField] private GameObject _dieVFX;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _dieAudioClip;
    [SerializeField] private AudioClip _hitAudioClip;


    private Animator _anim;
    private Quaternion _originRotation;
    private CapsuleCollider _col;
    void Awake()
    {
        _anim = GetComponent<Animator>();
        _col = GetComponent<CapsuleCollider>();
    }
    void Start()
    {
        _currentHealth = maxHealth;
        _originRotation = transform.rotation;

    }
    void Update()
    {
        RotateToPoint();
        Movement();
        ReachEnd();

    }
    void ReachEnd()
    {
        if (_currentPoint == points.Length)
        {
            GlobalEvent.InvokeOnDecreaseHealth(_damageToGameHealth);
            GlobalEvent.InvokeOnEnemyDestroy();
            Destroy(gameObject);
        }
    }
    void Movement()
    {
        _anim.SetInteger("Velocity", 1);
        transform.position = Vector3.MoveTowards(transform.position,
                points[_currentPoint].position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, points[_currentPoint].position) < 0.2f)
        {
            _currentPoint++;
        }
    }
    void RotateToPoint()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(points[_currentPoint].position - transform.position), rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(_originRotation.eulerAngles.x, transform.rotation.eulerAngles.y, _originRotation.eulerAngles.z);
    }
    public void Hit(float damage)
    {
        _currentHealth -= damage;
        _healthBar.UpdateHealthBar(_currentHealth, maxHealth);
        _audioSource.PlayOneShot(_hitAudioClip);

        if (_currentHealth <= 0)
        {
            GlobalEvent.InvokeOnEnemyDestroy();
            GlobalEvent.InvokeOnIncreaseMoney(_reward);
            _audioSource.PlayOneShot(_dieAudioClip);
            Vector3 t = new Vector3(transform.position.x, transform.position.y + _col.height / 2, transform.position.z);
            GameObject v = Instantiate(_dieVFX, t, Quaternion.identity, transform);

            Destroy(v, 1);
            Destroy(gameObject, 0.46f);
        }
    }
}
