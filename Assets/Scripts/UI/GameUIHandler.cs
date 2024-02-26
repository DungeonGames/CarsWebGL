using Agava.YandexGames;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(ProgressBar))]
public class GameUIHandler : MonoBehaviour
{
    //[SerializeField] private GameObject _coinsAndGemsText;
    [SerializeField] private GameObject _soundBitton;
    [SerializeField] private GameObject _healtBar;
    [SerializeField] private CanvasGroup _startUI;
    [SerializeField] private CanvasGroup _helperUI;
    [SerializeField] private CanvasGroup _inGameUI;
    //[SerializeField] private CanvasGroup _rewardUI;
    [SerializeField] private CanvasGroup _youLoseUI;
    [SerializeField] private Button _tapToStartButton;
    //[SerializeField] private Button _rewardButton;
    //[SerializeField] private PlayerBag _playerBag;
    [SerializeField] private AudioResources _audioResources;

    private float _deleay = 1f;
    private ProgressBar _levelProgressBar;
    private Car _car;

    public event UnityAction GameStart;
    public event UnityAction GameEnd;

    private Action _adOpen;
    private Action _adOffline;
    private Action<bool> _adClose;
    private Action<string> _adError;
    
    private void Awake()
    {
        Time.timeScale = 1;
        _levelProgressBar = GetComponent<ProgressBar>();
    }

    private void OnEnable()
    {
        //_playerBag.CarChanged += OnCarChanged;
        _tapToStartButton.onClick.AddListener(StartGame);
        _levelProgressBar.LevelComplete += OnLevelComplete;
        _adOpen += OnOpenVideo;
        _adOffline += OnAdOffline;
        _adClose += OnClose;
        _adError += OnError;
    }

    private void OnDisable()
    {
        //_playerBag.CarChanged -= OnCarChanged;
        _tapToStartButton.onClick.RemoveListener(StartGame);
        _levelProgressBar.LevelComplete -= OnLevelComplete;
        //_car.Died -= OnPlayerDied;
        _adOpen -= OnOpenVideo;
        _adOffline -= OnAdOffline;
        _adClose -= OnClose;
        _adError -= OnError;
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

    private void OnLevelComplete()
    {
        StartCoroutine(ShowEndGameUi());
    }

    private IEnumerator ShowEndGameUi()
    {
        GameEnd?.Invoke();

        yield return new WaitForSeconds(_deleay);

#if YANDEX_GAMES
        Agava.YandexGames.InterstitialAd.Show(_adOpen, _adClose, _adError, _adOffline);
#endif

#if VK_GAMES
        Agava.VKGames.Interstitial.Show();
#endif
        Time.timeScale = 0;
        _inGameUI.gameObject.SetActive(false);
        _helperUI.gameObject.SetActive(false);
        //_coinsAndGemsText.SetActive(false);
        _soundBitton.SetActive(false);
        _healtBar.SetActive(false);

#if ITCHIO_GAMES
        _rewardButton.gameObject.SetActive(false);
#endif
        //_rewardUI.alpha = 1;
        //_rewardUI.blocksRaycasts = true;
    }

    private void OnPlayerDied()
    {
        GameEnd?.Invoke();
        _youLoseUI.gameObject.SetActive(true);
        _inGameUI.gameObject.SetActive(false);
        _helperUI.gameObject.SetActive(false);
        //_coinsAndGemsText.SetActive(false);
        _soundBitton.SetActive(false);
        _healtBar.SetActive(false);
    }

    private void OnOpenVideo()
    {
        //_audioResources.Mute();
        AudioListener.pause = true;
        AudioListener.volume = 0f;
    }

    private void OnClose(bool state)
    {
        if (state == true)
        {
            //_audioResources.UnMute();
            AudioListener.pause = false;
            AudioListener.volume = 1f;
        }
    }

    private void OnError(string obj)
    {
        //_audioResources.UnMute();
        AudioListener.pause = false;
        AudioListener.volume = 1f;
    }


    private void OnAdOffline()
    {
        AudioListener.pause = false;
        AudioListener.volume = 1f;
    }
}
