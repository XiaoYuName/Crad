using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanle : MonoBehaviour
{
    private Button StartButton;
    private Button registButton;

    private LongineUI _longineUI => transform.parent.GetComponent<LongineUI>();
    private void Start()
    {
        StartButton = transform.Find("StarGame").GetComponent<Button>();
        registButton = transform.Find("RegistGame").GetComponent<Button>();
        StartButton.onClick.AddListener(StarClick);
        registButton.onClick.AddListener(RegistClick);
    }

    public  void OnDestroy()
    {
        StartButton.onClick.RemoveAllListeners();
        registButton.onClick.RemoveAllListeners();
    }

    private void StarClick()
    {
        _longineUI.ShowStarPanel();
    }

    private void RegistClick()
    {
        _longineUI.ShowRegistPanel();
    }
}
