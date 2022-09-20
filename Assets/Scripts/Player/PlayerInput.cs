using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    private Vector2 _direction = new Vector2();
    private bool _enableInput = true;
    private bool _isKeyboardInput = true;

    public event UnityAction<Vector2> Driving;
    public event UnityAction Stopped;

    private void FixedUpdate()
    {
        if (_enableInput)
        {
            if (_isKeyboardInput)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                _direction.Set(horizontalInput, verticalInput);
                Driving?.Invoke(_direction);

                if (_direction == Vector2.zero)
                    Stopped?.Invoke();
            }
            else
            {
                _direction.Set(_joystick.Horizontal, _joystick.Vertical);
                Driving?.Invoke(_direction);

                if (_direction == Vector2.zero)
                    Stopped?.Invoke();
            }
        }
        else
        {
            _direction = Vector2.zero;
            Driving?.Invoke(_direction);
        }

    }

    public void StopMove() => _enableInput = false;
}