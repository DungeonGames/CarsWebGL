using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MoveHelperUI : MonoBehaviour
{
    [SerializeField] private SwitchToggleInput _inputToggle;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _joystickView;
    [SerializeField] private GameObject _keyboardView;

    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _inputToggle.KeyboardOn += OnInputChanged;
        _playerInput.Stopped += OnPlayerStoped;
        _playerInput.JoystikDriving += OnPlayerDriving;
        _playerInput.KeyboardDriving += OnPlayerDriving;
    }

    private void OnDisable()
    {
        _inputToggle.KeyboardOn -= OnInputChanged;
        _playerInput.Stopped -= OnPlayerStoped;
        _playerInput.JoystikDriving -= OnPlayerDriving;
        _playerInput.KeyboardDriving -= OnPlayerDriving;
    }

    private void OnInputChanged(bool isKeyboard)
    {
        if (isKeyboard)
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

    private void OnPlayerStoped() => _canvasGroup.alpha = 1;

    private void OnPlayerDriving(Vector2 direction) => _canvasGroup.alpha = 0;
}
