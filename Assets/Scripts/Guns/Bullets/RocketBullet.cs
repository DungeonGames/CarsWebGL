using System.Collections;
using UnityEngine;

public class RocketBullet : Bullet
{
    [SerializeField] private float _radius;
    [SerializeField] private ParticleSystem _explisionParticle;
    [SerializeField] private CapsuleCollider _detectionCollider;
    [SerializeField] private float _power;

    private AudioResources _audioResources;
    private float _delay = 0.1f;
    private float _upForce = 0.001f;
    private float _damage = 0.5f;

    private const string RocketLauncherExplosion = "RocketLauncherExplosion";

    private void Start()
    {
        _audioResources = FindObjectOfType<AudioResources>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Explosion();
    }

    private void Explosion()
    {
        _audioResources.PlaySound(RocketLauncherExplosion);
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);
        Instantiate(_explisionParticle, transform.position, Quaternion.identity);
        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (hit.TryGetComponent(out Enemy enemy))
            {
                if (enemy.IsAlive)
                {
                    enemy.TakeDamage(_damage);
                    enemy.Discard();
                }
            }

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(_power, transform.position, _radius, _upForce, ForceMode.Impulse);
            }
        }

        StartCoroutine(ActivateCollider());
    }

    private IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(_delay);

        Destroy(gameObject);
    }
}
