using System;
using System.Collections;
using System.Collections.Generic;
using M_Socket;
using Protocol.Code;
using Protocol.Dto;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StarPanle : UIBase
{
    private Button LogintButton; //登录输入框
    private Button CloseButton;  //关闭面板button
    private TMP_InputField InputFieldUser; //用户名
    private TMP_InputField InputFieldPass; //密码输入框

    private void Awake()
    {
        Bind(UIEvent.START_PLAYEL_ACTIVE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.START_PLAYEL_ACTIVE:
                setPanelActive((bool)message);
                break;
        }
        
    }

    private void Start()
    {
        LogintButton = transform.GetChild(0).Find("LogintButton").GetComponent<Button>();
        InputFieldUser = transform.Find("Back").transform.Find("InputFieldUser").Find("InputField (TMP)").
            GetComponent<TMP_InputField>();
        InputFieldPass = transform.Find("Back").transform.Find("InputFieldPassWord").Find("InputField (TMP)").
            GetComponent<TMP_InputField>();
        CloseButton = transform.GetChild(0).Find("Exit").GetComponent<Button>();
        LogintButton.onClick.AddListener(LogintClick);
        CloseButton.onClick.AddListener(ColoseButton);
        setPanelActive(false);
    }
    
    private void LogintClick()
    {
        //需要和服务器交互
        if (string.IsNullOrEmpty(InputFieldUser.text)) return;
        if (string.IsNullOrEmpty(InputFieldPass.text)) return;
        
        AccountDto dto = new AccountDto(InputFieldUser.text, InputFieldPass.text);
        SocketMsg socketMsg = new SocketMsg(OpCode.Account, AccountCode.LOGIN, dto);
        Dispatch(AreaCode.NET,0,socketMsg);
    }

    private void ColoseButton()
    {
        setPanelActive(false);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        LogintButton.onClick.RemoveAllListeners();
        CloseButton.onClick.RemoveAllListeners();
    }
}
