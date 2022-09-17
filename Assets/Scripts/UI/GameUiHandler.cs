using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameUiHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _startUI;
    [SerializeField] private CanvasGroup _helperUI;
    [SerializeField] private CanvasGroup _inGameUI;
    [SerializeField] private CanvasGroup _endGameUI;
    [SerializeField] private CanvasGroup _youLoseUI;
    [SerializeField] private Button _tapToStartButton;
    [SerializeField] private ProgressBar _levelProgressBar;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Car _car;

    private float _deleay = 2f;

    public event UnityAction GameStart;
    public event UnityAction GameEnd;

    private void OnEnable()
    {
        _tapToStartButton.onClick.AddListener(StartGame);
        _levelProgressBar.LevelComplete += OnLevelComplete;
        _playerInput.Stopped += OnPlayerStoped;
        _playerInput.Driving += OnPlayerDriving;
        _car.Died += OnPlayerDied;
    }

    private void OnDisable()
    {
        _tapToStartButton.onClick.RemoveListener(StartGame);
        _levelProgressBar.LevelComplete -= OnLevelComplete;
        _playerInput.Stopped -= OnPlayerStoped;
        _playerInput.Driving += OnPlayerDriving;
    }

    private void StartGame()
    {
        _startUI.gameObject.SetActive(false);
        _inGameUI.gameObject.SetActive(true);
        _helperUI.gameObject.SetActive(true);
        GameStart?.Invoke();
    }

    private void OnLevelComplete()
    {
        StartCoroutine(ShowEndGameUi());
    }

    private IEnumerator ShowEndGameUi()
    {
        GameEnd?.Invoke();

        yield return new WaitForSeconds(_deleay);

        _playerInput.StopMove();
        _inGameUI.gameObject.SetActive(false);
        _endGameUI.gameObject.SetActive(true);
        _helperUI.gameObject.SetActive(false);
    }

    private void OnPlayerStoped() => _helperUI.alpha = 1;

    private void OnPlayerDriving(Vector2 direction) => _helperUI.alpha = 0;

    private void OnPlayerDied()
    {
        GameEnd?.Invoke();
        _playerInput.StopMove();
        _youLoseUI.gameObject.SetActive(true);
        _inGameUI.gameObject.SetActive(false);
        _helperUI.gameObject.SetActive(false);
    }
}
