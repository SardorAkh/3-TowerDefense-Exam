using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tower : MonoBehaviour
{
    public Cell _cell;
    private SphereCollider _col;
    private List<GameObject> _enemies;
    private Transform _target;


    [Header("Tower settings")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private LayerMask enemyLayerMask;

    public int price;

    private Quaternion _originRotateTower;


    [Header("Shooting settings")]
    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform[] projectileRespawnPoint;
    [SerializeField] private float _damage;
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private float _projectileRotateSpeed;
    [SerializeField] private float _fireRate;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private AudioSource _audioSource;
    private float _fireTimeCounter;
    private bool _readyToFire;
    void Awake()
    {
        _col = GetComponent<SphereCollider>();
    }
    private void Start()
    {
        _originRotateTower = transform.rotation;
        _enemies = new List<GameObject>();
    }
    void Update()
    {
        FindTarget();
        if (_target == null)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _originRotateTower, rotateSpeed * Time.deltaTime);
        }
        RotateTowardsEnemy();
        Shoot();
    }
    void RotateTowardsEnemy()
    {
        if (_target == null)
            return;

        transform.rotation = Quaternion.RotateTowards(transform.rotation,
            Quaternion.LookRotation(_target.position - transform.position),
            rotateSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(_originRotateTower.eulerAngles.x,
            transform.rotation.eulerAngles.y, _originRotateTower.eulerAngles.z);
    }
    void FindTarget()
    {

        for (int i = _enemies.Count - 1; i >= 0; i--)
        {
            if (_enemies[i] == null)
            {
                _enemies.RemoveAt(i);
            }
        }
        if (_enemies.Count == 0)
        {
            _target = null;
            return;
        }
        _target = _enemies[0].transform;
    }
    void Shoot()
    {
        if (!_readyToFire)
        {
            _fireTimeCounter += Time.deltaTime;
            if (_fireTimeCounter >= _fireRate)
            {
                _readyToFire = true;
                _fireTimeCounter = 0;
            }
        }
        if (_target == null)
            return;

        if (_readyToFire && IsEnemyInFront())
        {
            for (int i = 0; i < projectileRespawnPoint.Length; i++)
            {

                Projectile p = Instantiate(projectile, projectileRespawnPoint[i].position, transform.rotation);
                p.InitProjectile(_target, _damage, _projectileSpeed, _projectileRotateSpeed);
            }
            _audioSource.PlayOneShot(_audioClip);
            _readyToFire = false;

        }
    }
    bool IsEnemyInFront()
    {
        return Physics.Raycast(transform.position, (_target.position - transform.position).normalized, _col.radius * 2, enemyLayerMask);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemies.Add(other.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && !_enemies.Contains(other.gameObject))
        {
            _enemies.Add(other.gameObject);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && _enemies.Contains(other.gameObject))
        {
            _enemies.Remove(other.gameObject);
        }
    }

}