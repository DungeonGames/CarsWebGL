using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Joystick _joystick;

    private Vector2 _direction = new Vector2();
    private bool _enableInput = true;

    public event UnityAction<Vector2> Driving;
    public event UnityAction Stopped;

    private void FixedUpdate()
    {
        if (_enableInput)
        {
            _direction.Set(_joystick.Horizontal, _joystick.Vertical);
            Driving?.Invoke(_direction);

            if (_direction == Vector2.zero)
                Stopped?.Invoke();
        }
        else
        {
            _direction = Vector2.zero;
            Driving?.Invoke(_direction);
        }
        
    }

    public void StopMove() => _enableInput = false;
}