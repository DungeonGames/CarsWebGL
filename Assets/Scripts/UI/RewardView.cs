using UnityEngine;
using TMPro;

[RequireComponent(typeof(LevelReward))]
public class RewardView : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _gemText;

    private LevelReward _levelReward;

    private void Awake()
    {
        _levelReward = GetComponent<LevelReward>();
    }

    private void OnEnable()
    {
        _levelReward.CurrentItem += ShowProgressBar;
        _levelReward.CoinRewardChange += OnCoinRewardChenged;
    }

    private void Start()
    {
        _coinText.text = _levelReward.CoinsReward.ToString();
        _gemText.text = _levelReward.GemsReward.ToString();
    }

    private void OnDisable()
    {
        _levelReward.CurrentItem -= ShowProgressBar;
        _levelReward.CoinRewardChange -= OnCoinRewardChenged;
    }

    private void ShowProgressBar(UnlockableItem item, int currentQuanity)
    {
        if (item != null)
        {
            item.gameObject.SetActive(true);
            item.ShowProgress(currentQuanity);
        }
    }

    private void OnCoinRewardChenged() => _coinText.text = _levelReward.CoinsReward.ToString();
}
