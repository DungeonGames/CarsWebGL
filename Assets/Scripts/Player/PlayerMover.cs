using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerBag))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _rotationSpeedJoystick;
    [SerializeField] private float _rotationSpeedKeyboard;
    [SerializeField] private float _speed;
    
    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private PlayerBag _playerBag;
    private Vector3 _direction;

    private void Awake()
    {
        _playerBag = GetComponent<PlayerBag>();
    }

    private void OnEnable()
    {
        _playerBag.CarChanged += OnCarChanged;
    }

    public void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput.JoystikDriving += OnJoystickDrive;
        _playerInput.KeyboardDriving += OnKeyboardDrive;
    }

    private void OnDisable()
    {
        _playerBag.CarChanged -= OnCarChanged;
        _playerInput.JoystikDriving -= OnJoystickDrive;
        _playerInput.KeyboardDriving -= OnKeyboardDrive;
    }

    private void OnCarChanged(Car car) => _speed = car.MaxSpeed;

    private void OnJoystickDrive(Vector2 direction)
    {
        _direction.Set(direction.x, 0, direction.y);
        Vector3 offset = _direction.normalized * Time.deltaTime * _speed;
        _rigidbody.linearVelocity = offset;

        if (_direction != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(_direction, Vector3.up);

            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _rotationSpeedJoystick * Time.deltaTime);
        }
    }

    private void OnKeyboardDrive(Vector2 direction)
    {
        direction.y *= direction.y > 0 ? _speed : _speed;
        _rigidbody.linearVelocity = transform.forward.normalized * direction.y * Time.deltaTime;
        float newRotation = direction.x * _rotationSpeedKeyboard * Time.deltaTime;
        transform.Rotate(0, newRotation, 0, Space.Self);
    }

    public void UpgradeSpeed(float percentBonus)
    {
        _speed *= percentBonus;
    }
}