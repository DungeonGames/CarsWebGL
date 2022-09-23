using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerBag))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private float _speed;
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private PlayerBag _playerBag;
    private Vector3 _direction;

    private void Awake()
    {
        _playerBag = GetComponent<PlayerBag>();
    }

    public void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput.Driving += Drive;
    }

    private void OnEnable()
    {
        _playerBag.CarChanged += OnCarChanged;
    }

    private void OnDisable()
    {
        _playerBag.CarChanged -= OnCarChanged;
        _playerInput.Driving -= Drive;
    }

    private void Drive(Vector2 direction)
    {
        _direction.Set(direction.x, 0, direction.y);
        Vector3 offset = _direction.normalized * Time.deltaTime * _speed;
        _rigidbody.velocity = offset;

        if (_direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(_direction, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCarChanged(Car car) => _speed = car.MaxSpeed;
}