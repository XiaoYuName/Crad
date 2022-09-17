using System;
using System.Collections;
using System.Collections.Generic;
using Crad.Assets;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        LoadBundleUI();
    }

    private void LoadBundleUI()
    {
        switch (AssetsBundleManager.Instance.Mode)
        {
            case AssetMode.LocalAsset:
                var locallogineUI = Resources.Load("Prefab/LongineUI");
                var localparent = GameObject.FindWithTag("LoginePoint");
                Instantiate(locallogineUI, localparent.transform);
                break;
            case AssetMode.LocalBundle:
                var logineUI = AssetsBundleManager.Instance.GetAbGameObject("LongineUI");
                var parent = GameObject.FindWithTag("LoginePoint");
                Instantiate(logineUI, parent.transform);
                break;
            case AssetMode.UpdateBundle:
                break;
        }

    }
}
