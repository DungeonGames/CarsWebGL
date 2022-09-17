using UnityEngine;
using TMPro;

public class RewardView : MonoBehaviour
{
    [SerializeField] private LevelReward _levelReward;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _gemText;

    private void Start()
    {
        _coinText.text = _levelReward.CoinsReward.ToString();
        _gemText.text = _levelReward.GemsReward.ToString();
    }
}
