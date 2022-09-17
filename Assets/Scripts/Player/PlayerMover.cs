using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody _rigidbody;
    private PlayerInput _playerInput;
    private Vector3 _direction;

    public void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _playerInput.Driving += Drive;
    }

    private void OnDisable()
    {
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
}