using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanle : UIBase
{
    private Button StartButton;
    private Button registButton;

    private void Start()
    {
        StartButton = transform.Find("StarGame").GetComponent<Button>();
        registButton = transform.Find("RegistGame").GetComponent<Button>();
        StartButton.onClick.AddListener(StarClick);
        registButton.onClick.AddListener(RegistClick);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        StartButton.onClick.RemoveAllListeners();
        registButton.onClick.RemoveAllListeners();
    }

    private void StarClick()
    {
        Dispatch(AreaCode.UI,UIEvent.START_PLAYEL_ACTIVE,true);
    }

    private void RegistClick()
    {
        Dispatch(AreaCode.UI,UIEvent.REGLST_PLAYEL_ACTIVE,true);
    }
}
