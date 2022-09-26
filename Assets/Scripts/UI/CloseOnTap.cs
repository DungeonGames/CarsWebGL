using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections.Generic;

public class CloseOnTap : MonoBehaviour
{
    [SerializeField] private GameObject _panelWithControlElements;
    [SerializeField] private GameObject _tapToPlay;

    private void Start()
    {
        _panelWithControlElements.SetActive(false);
    }

    public void CloseSelf()
    {
        _panelWithControlElements.gameObject.SetActive(!_panelWithControlElements.gameObject.activeSelf);

        if (_panelWithControlElements.gameObject.activeSelf == false)
        {
            _tapToPlay.gameObject.SetActive(true);
        }
        else
        {
            _tapToPlay.gameObject.SetActive(false);
        }
    }
    

}