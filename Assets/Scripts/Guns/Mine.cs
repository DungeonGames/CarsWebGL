using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Mine : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _power;
    [SerializeField] private SphereCollider _detectionCollider;
    [SerializeField] private MineMaterialSeter _mineMaterialSeter;
    [SerializeField] private ParticleSystem _explisionParticle;

    private int _damage = 2;
    private Vector3 _startPositionParticle;
    private const float _startPositionParticleY = 0.1f;
    private float _delay = 0.1f;
    private float _upForce = 0.01f;

    private void Start()
    {
        _startPositionParticle = transform.position;
        _startPositionParticle.y = _startPositionParticleY;
    }

    private void OnEnable()
    {
        _mineMaterialSeter.Ready += Explosion;
    }

    private void OnDisable()
    {
        _mineMaterialSeter.Ready -= Explosion;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy.IsAlive)
            {
                enemy.TakeDamage(_damage);
            }
        }
    }

    private void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        Instantiate(_explisionParticle, _startPositionParticle, Quaternion.identity);
        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_power, transform.position, _radius, _upForce, ForceMode.Impulse);
            }

            StartCoroutine(ActivateCollider());
        }
    }

    private IEnumerator ActivateCollider()
    {
        _detectionCollider.enabled = true;
        yield return new WaitForSeconds(_delay);

        Destroy(gameObject);
    }
}
