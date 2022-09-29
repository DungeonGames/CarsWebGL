using Agava.YandexGames;
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
    [SerializeField] private CanvasGroup _rewardUI;
    [SerializeField] private CanvasGroup _youLoseUI;
    [SerializeField] private Button _tapToStartButton;
    [SerializeField] private PlayerBag _playerBag;

    private float _deleay = 1f;
    private ProgressBar _levelProgressBar;

    private Car _car;
    public event UnityAction GameStart;
    public event UnityAction GameEnd;

    private void Awake()
    {
        Time.timeScale = 1;
        _levelProgressBar = GetComponent<ProgressBar>();
    }

    private void OnEnable()
    {
        _playerBag.CarChanged += OnCarChanged;
        _tapToStartButton.onClick.AddListener(StartGame);
        _levelProgressBar.LevelComplete += OnLevelComplete;        
    }

    private void OnDisable()
    {
        _playerBag.CarChanged -= OnCarChanged;
        _tapToStartButton.onClick.RemoveListener(StartGame);
        _levelProgressBar.LevelComplete -= OnLevelComplete;
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

        InterstitialAd.Show();
        Time.timeScale = 0;
        _inGameUI.gameObject.SetActive(false);
        _helperUI.gameObject.SetActive(false);
        _rewardUI.alpha = 1;
        _rewardUI.blocksRaycasts = true;
    }

    

    private void OnPlayerDied()
    {
        GameEnd?.Invoke();
        _youLoseUI.gameObject.SetActive(true);
        _inGameUI.gameObject.SetActive(false);
        _helperUI.gameObject.SetActive(false);
    }
}
