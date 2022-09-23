using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ProgressBar))]
public class GameUiHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _startUI;
    [SerializeField] private CanvasGroup _helperUI;
    [SerializeField] private CanvasGroup _inGameUI;
    [SerializeField] private CanvasGroup _endGameUI;
    [SerializeField] private CanvasGroup _youLoseUI;
    [SerializeField] private Button _tapToStartButton;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private PlayerBag _playerBag;

    private float _deleay = 2f;
    private ProgressBar _levelProgressBar;

    private Car _car;
    public event UnityAction GameStart;
    public event UnityAction GameEnd;

    private void Awake()
    {
        _levelProgressBar = GetComponent<ProgressBar>();
    }

    private void OnEnable()
    {
        _playerBag.CarChanged += OnCarChanged;
        _tapToStartButton.onClick.AddListener(StartGame);
        _levelProgressBar.LevelComplete += OnLevelComplete;
        _playerInput.Stopped += OnPlayerStoped;
        _playerInput.Driving += OnPlayerDriving;
        
    }

    private void OnDisable()
    {
        _playerBag.CarChanged -= OnCarChanged;
        _tapToStartButton.onClick.RemoveListener(StartGame);
        _levelProgressBar.LevelComplete -= OnLevelComplete;
        _playerInput.Stopped -= OnPlayerStoped;
        _playerInput.Driving -= OnPlayerDriving;
        _car.Died -= OnPlayerDied;
    }

    private void OnCarChanged(Car car)
    {
        _car = car;
        _car.Died += OnPlayerDied;
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

        _inGameUI.gameObject.SetActive(false);
        _helperUI.gameObject.SetActive(false);
        _endGameUI.alpha = 1;
        _endGameUI.blocksRaycasts = true;
    }

    private void OnPlayerStoped() => _helperUI.alpha = 1;

    private void OnPlayerDriving(Vector2 direction) => _helperUI.alpha = 0;

    private void OnPlayerDied()
    {
        GameEnd?.Invoke();
        _youLoseUI.gameObject.SetActive(true);
        _inGameUI.gameObject.SetActive(false);
        _helperUI.gameObject.SetActive(false);
    }
}
