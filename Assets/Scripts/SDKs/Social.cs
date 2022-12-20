using System;
using UnityEngine;

public class Social : MonoBehaviour
{
    private const string JoinGroup = "JoinGroup";

    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _inviteButton;
    [SerializeField] private GameObject _leaderboardButton;
    [SerializeField] private Wallet _wallet;

    private int _rewardOnJoinGroup = 2000;
    private int _isJoinGroup = 0;

    public event Action JoinSucces;

    private void Start()
    {
#if !VK_GAMES
        _inviteButton.SetActive(false);
#endif

#if VK_GAMES
        _leaderboardButton.SetActive(false);
        _isJoinGroup = PlayerPrefs.GetInt(JoinGroup);
#endif

#if ITCHIO_GAMES
        _leaderboardButton.SetActive(false);
        _inviteButton.SetActive(false);
#endif
    }

    private void OnEnable()
    {
        JoinSucces += OnJoinGroup;
    }

    private void OnDisable()
    {
        JoinSucces-= OnJoinGroup;
    }

    public void OnSocialButtonClick()
    {
        _panel.SetActive(true);
    }

    public void OnCloseButtonClick()
    {
        _panel.SetActive(false);
    }

    public void OnJoinButtonCkick()
    {
        Agava.VKGames.Community.InviteToIJuniorGroup(JoinSucces);
    }

    public void OnInviteButtonClick()
    {
        Agava.VKGames.SocialInteraction.InviteFriends();
    }

    private void OnJoinGroup()
    {
        if (_isJoinGroup == 0)
        {
            _wallet.AddRewardOnJoinGroup(_rewardOnJoinGroup);
            _isJoinGroup = 1;
            PlayerPrefs.SetInt(JoinGroup, _isJoinGroup);
        }
    }
}
