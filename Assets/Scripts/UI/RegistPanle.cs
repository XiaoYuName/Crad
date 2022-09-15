using System.Collections;
using System.Collections.Generic;
using M_Socket;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Protocol.Code;
using Protocol.Dto;

public class RegistPanle : UIBase
{
    private Button LogintButton; //登录输入框
    private Button CloseButton;  //关闭面板button
    
    private TMP_InputField InputFieldUser; //用户名
    private TMP_InputField InputFieldPass; //密码输入框
    private TMP_InputField InputRepeat; //再次确认

    private void Awake()
    {
        Bind(UIEvent.REGLST_PLAYEL_ACTIVE);
    }

    public override void Execute(int eventCode, object message)
    {
        switch (eventCode)
        {
            case UIEvent.REGLST_PLAYEL_ACTIVE:
                setPanelActive((bool)message);
                break;
        }
        
    }

    private void Start()
    {
        LogintButton = transform.GetChild(0).Find("ReigistButton").GetComponent<Button>();
        InputFieldUser = transform.Find("Back").transform.Find("InputFieldUser").Find("InputField (TMP)").
            GetComponent<TMP_InputField>();
        InputFieldPass = transform.Find("Back").transform.Find("InputFieldPassWord").Find("InputField (TMP)").
            GetComponent<TMP_InputField>();
        InputRepeat = transform.Find("Back").transform.Find("InputFieldRepeat").Find("InputField (TMP)").
            GetComponent<TMP_InputField>();
        CloseButton = transform.GetChild(0).Find("Exit").GetComponent<Button>();
        LogintButton.onClick.AddListener(ReigistClick);
        CloseButton.onClick.AddListener(ColoseButton);
        setPanelActive(false);
    }
    
    private void ReigistClick()
    {
        //需要和服务器交互
        if (string.IsNullOrEmpty(InputFieldUser.text)) return;
        if (string.IsNullOrEmpty(InputFieldPass.text)) return;
        if (string.IsNullOrEmpty(InputRepeat.text)) return;
        if (InputFieldPass.text != InputRepeat.text) return;
        
        //TODO: 给服务器发送验证请求
        Debug.Log("正在注册.............");
        AccountDto dto = new AccountDto(InputFieldUser.text, InputFieldPass.text);
        SocketMsg socketMsg = new SocketMsg(OpCode.Account, AccountCode.REGIST_CREQ, dto);
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
