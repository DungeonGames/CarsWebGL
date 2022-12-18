using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private GameUiHandler _gameHandler;
    [SerializeField] private Joystick _joystick;

    private Vector2 _direction = new Vector2();
    private bool _enableInput = false;
    private bool _isKeyboardInput;

    public event UnityAction<Vector2> JoystikDriving;
    public event UnityAction<Vector2> KeyboardDriving;
    public event UnityAction Stopped;

    private void OnEnable()
    {
        _gameHandler.GameStart += StartMove;
        _gameHandler.GameEnd += StopMove;
    }

    private void FixedUpdate()
    {
        if (_enableInput)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _isKeyboardInput = true;
            }
            if (_joystick.IsTouch)
            {
                _isKeyboardInput = false;
            }

            if (_isKeyboardInput)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                _direction.Set(horizontalInput, verticalInput);
                KeyboardDriving?.Invoke(_direction);
            }
            else
            {
                _joystick.gameObject.SetActive(true);
                _direction.Set(_joystick.Horizontal, _joystick.Vertical);
                JoystikDriving?.Invoke(_direction);
            }

            if (_direction == Vector2.zero)
            {
                Stopped?.Invoke();
            }
        }
        else
        {
            _direction = Vector2.zero;
            JoystikDriving?.Invoke(_direction);
            KeyboardDriving?.Invoke(_direction);
        }
    }

    private void OnDisable()
    {
        _gameHandler.GameStart -= StartMove;
        _gameHandler.GameEnd -= StopMove;
    }

    private void StartMove() => _enableInput = true;

    private void StopMove() => _enableInput = false;
}