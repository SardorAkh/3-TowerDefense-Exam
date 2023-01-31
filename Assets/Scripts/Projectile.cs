using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{

    private Transform _target;
    private float _damage;
    private float _speed;
    private float _rotateSpeed;
    private CapsuleCollider _targetCol;
    [SerializeField] private GameObject _hitVFX;

    public void InitProjectile(Transform target, float damage, float speed, float rotateSpeed)
    {

        _target = target; _damage = damage; _speed = speed; _rotateSpeed = rotateSpeed;
        _targetCol = _target.GetComponent<CapsuleCollider>();
    }
    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 _targetPos = new Vector3(_target.position.x, _target.position.y + _targetCol.height / 2, _target.position.z);
        Vector3 desiredVel = Vector3.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
        transform.position = desiredVel;

        Quaternion desiredAngle = Quaternion.RotateTowards(transform.rotation,
            Quaternion.LookRotation(_targetPos - transform.position),
            _rotateSpeed * Time.deltaTime);
        transform.rotation = desiredAngle;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();
            e.Hit(_damage);
            Vector3 _targetPos = new Vector3(_target.position.x, _target.position.y + _targetCol.height / 2, _target.position.z);
            GameObject v = Instantiate(_hitVFX, _targetPos, Quaternion.identity);

            Destroy(v, 1);
            Destroy(gameObject);
        }
    }
}
