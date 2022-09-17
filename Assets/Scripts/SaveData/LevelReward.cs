using UnityEngine;

public class LevelReward : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private int _coinsReward;
    [SerializeField] private int _gemsReward;

    public int CoinsReward => _coinsReward;
    public int GemsReward => _gemsReward;

    public void OnClaimRewardButtonClick()
    {
        _wallet.AddReward(_coinsReward, _gemsReward);
    }
}
