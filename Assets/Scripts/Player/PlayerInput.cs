using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;
    [SerializeField] private SwitchToggleInput _toggleInput;

    private Vector2 _direction = new Vector2();
    private bool _enableInput = true;
    private bool _isKeyboardInput;

    public event UnityAction<Vector2> Driving;
    public event UnityAction Stopped;

    private void OnEnable()
    {
        _toggleInput.KeyboardOn += IsKeyboardInputOn;
    }

    private void OnDisable()
    {
        _toggleInput.KeyboardOn -= IsKeyboardInputOn;
    }

    private void FixedUpdate()
    {
        if (_enableInput)
        {
            if (_isKeyboardInput)
            {
                _joystick.gameObject.SetActive(false);

                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");

                _direction.Set(horizontalInput, verticalInput);
                Driving?.Invoke(_direction);

                if (_direction == Vector2.zero)
                    Stopped?.Invoke();
            }
            else
            {
                _joystick.gameObject.SetActive(true);
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

    private void IsKeyboardInputOn(bool state) => _isKeyboardInput = state;
}