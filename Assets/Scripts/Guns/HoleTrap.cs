using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HoleTrap : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private SphereCollider _playerWall;

    private Animator _animator;
    private bool _isActivated = false;
    private const string OpenGate = "OpenGate";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            _animator.SetBool(OpenGate, true);
            _isActivated = true;
            _playerWall.enabled = true;
        }

        if (_isActivated)
            if (other.TryGetComponent(out Boid enemy))
                enemy.HoleTrapCath(_target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActivated)
            if (collision.gameObject.TryGetComponent(out Boid enemy))
                enemy.HoleTrapCath(_target);
    }
}
