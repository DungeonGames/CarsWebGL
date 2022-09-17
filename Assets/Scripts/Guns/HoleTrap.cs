using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HoleTrap : MonoBehaviour
{
    [SerializeField] private Transform _target;

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
        }

        if (_isActivated)
            if (other.TryGetComponent(out Boid enemy))
                enemy.HoleTrapCath(_target);
    }
}
