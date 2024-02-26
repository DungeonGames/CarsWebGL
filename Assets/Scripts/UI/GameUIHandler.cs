using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ProgressBar))]
public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject _soundBitton;
    [SerializeField] private GameObject _healtBar;
    [SerializeField] private CanvasGroup _startUI;
    [SerializeField] private CanvasGroup _helperUI;
    [SerializeField] private CanvasGroup _inGameUI;
    [SerializeField] private CanvasGroup _youLoseUI;
    [SerializeField] private Button _tapToStartButton;
    [SerializeField] private PlayerBag _playerBag;
    
    private Car _car;

    public event UnityAction GameStart;
    public event UnityAction GameEnd;
    
    private void Awake()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        _playerBag.CarChanged += OnCarChanged;
        _tapToStartButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _playerBag.CarChanged -= OnCarChanged;
        _tapToStartButton.onClick.RemoveListener(StartGame);
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
        _healtBar.SetActive(true);
        GameStart?.Invoke();
    }

    private void OnPlayerDied()
    {
        GameEnd?.Invoke();
        _youLoseUI.gameObject.SetActive(true);
        _inGameUI.gameObject.SetActive(false);
        _helperUI.gameObject.SetActive(false);
        _soundBitton.SetActive(false);
        _healtBar.SetActive(false);
    }
}
