using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MoveHelperUI : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _joystickView;
    [SerializeField] private GameObject _keyboardView;

    private CanvasGroup _canvasGroup;

    private bool _isKeyboard = false;

    private void OnEnable()
    {
        _playerInput.Stopped += OnPlayerStoped;
        _playerInput.JoystikDriving += OnJoystickDriving;
        _playerInput.KeyboardDriving += OnKeyboardDriving;
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnDisable()
    {

        _playerInput.Stopped -= OnPlayerStoped;
        _playerInput.JoystikDriving -= OnJoystickDriving;
        _playerInput.KeyboardDriving -= OnKeyboardDriving;
    }

    private void OnPlayerStoped()
    {
        _canvasGroup.alpha = 1;

        if (_isKeyboard)
        {
            _keyboardView.SetActive(true);
            _joystickView.SetActive(false);
        }
        else
        {
            _keyboardView.SetActive(false);
            _joystickView.SetActive(true);
        }
    }

    private void OnKeyboardDriving(Vector2 direction)
    {
        _canvasGroup.alpha = 0;
        _isKeyboard = true;
    }

    private void OnJoystickDriving(Vector2 direction)
    {
        _canvasGroup.alpha = 0;
        _isKeyboard = false;
    }
}
