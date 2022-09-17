using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Crad.UIManager;
using Protocol.Code;
using Protocol.Dto;
using Protocol.Exception;
using UnityEngine;

namespace M_Socket
{
    /// <summary>
    /// 客户端Socket的封装
    /// </summary>
    public class ClientPeer                     
    {
        private Socket Socket;
        private string ip;
        private int port;
        
        private byte[] receiveBuffer = new byte[1024]; //接收数据的默认缓存区
        private List<byte> dataCache = new List<byte>();
        private bool isProcessReceive = false;
        /// <summary>
        /// 消息列表
        /// </summary>
        public Queue<SocketMsg> SocketMsgQueue = new Queue<SocketMsg>();

        /// <summary>
        /// 构造Socket对象,需要IP地址与端口号
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        public ClientPeer(string ip,int port)
        {
            try
            {
                Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.ip = ip;
                this.port = port;
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError(e.Message);
#endif
            }
        }

        public void Connect()
        {
            try
            {
                Socket.Connect(ip,port);
                Debug.Log("连接服务器成功");
                //开始异步接收数据
                StarReceive();
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                throw;
            }
            
        }

        #region 接收数据
        
        /// <summary>
        /// 开始接收数据
        /// </summary>
        private void StarReceive()
        {
            if (Socket == null && Socket.Connected == false)
            {
#if UNITY_EDITOR
                Debug.LogError("没有连接成功,无法发送数据");
#endif
                return;
            } //如果Socket为空,或者没有连接时直接退出

            Socket.BeginReceive(receiveBuffer, 0, 1024, SocketFlags.None,
                ReceiveCallBack, Socket);
        }

        /// <summary>
        /// 收到消息回调
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveCallBack(IAsyncResult ar)
        {
            try
            {
               int length = Socket.EndReceive(ar);
               byte[] tempByteArray = new byte[length];
               Buffer.BlockCopy(receiveBuffer,0,tempByteArray,0,length);
               //处理收到的数据
               dataCache.AddRange(tempByteArray);
               if(!isProcessReceive) //如果没有在处理,那么就开始进行处理
                ProcessReceive();
               
               StarReceive();
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.Log(e.Message);
#endif
            }
        }

        /// <summary>
        /// 处理收到的数据
        /// </summary>
        private void ProcessReceive()
        {
            isProcessReceive = true;
            byte[] data = EncodeTool.DecodePacket(ref dataCache);
            if (data == null)
            {
                isProcessReceive = false;
                return;
            }

            SocketMsg msg = EncodeTool.DecodeMsg(data);
            Loom.Initialize();
            //存储数据等待处理
            SocketMsgQueue.Enqueue(msg);
            switch (msg.OpCode)
            {
                case OpCode.Account:
                    AccountException exception = (AccountException)msg.value;
                    switch (exception)
                    {
                        case AccountException.AccountExist:
                            Loom.QueueOnMainThread(b =>
                            {
                                DialogManager.Instance.OpDialog("注册","账号已存在","确定");
                            },null);
                            
                            break;
                        case AccountException.AccountisNull:
                            Loom.QueueOnMainThread(b =>
                            {
                                DialogManager.Instance.OpDialog("注册","账号为空","确定");
                            },null);
                            
                            break;
                        case AccountException.AccountPasswordNull:
                            Loom.QueueOnMainThread(b =>
                            {
                                DialogManager.Instance.OpDialog("注册","密码不合法","确定");
                            },null);
                            break;
                        case AccountException.Regist:
                            Loom.QueueOnMainThread(b =>
                            {
                                DialogManager.Instance.OpDialog("注册","注册成功","确定");
                            },null);
                            
                            break;
                        case AccountException.LoginExist:
                            Loom.QueueOnMainThread(a =>
                            {
                                DialogManager.Instance.OpDialog("登录","没有该账号","确定");
                            }, null);
                            
                            break;
                        case AccountException.LoginOnline:
                            Loom.QueueOnMainThread(b =>
                            {
                                DialogManager.Instance.OpDialog("登录","该账号已在登录中","确定");
                            },null);
                            
                            break;
                        case AccountException.LoginMatch:
                            Loom.QueueOnMainThread(b =>
                            {
                                DialogManager.Instance.OpDialog("登录","账号密码不匹配","确定");
                            },null);
                            
                            break;
                        case AccountException.Login:
                            Loom.QueueOnMainThread(b =>
                            {
                                DialogManager.Instance.OpDialog("登录成功","登录成功","确定");
                            },null);
                            break;
                    }
                    break;
            }
            
            ProcessReceive();
        }

        #endregion

        #region 发送数据

        public void Send(int OpCode, int subCode, object value)
        {
            SocketMsg msg = new SocketMsg(OpCode,subCode,value);
            Send(msg);

        }

        public void Send(SocketMsg msg)
        {
            byte[] data = EncodeTool.EncodeMsg(msg);
            byte[] packet = EncodeTool.EncodePacket(data);
            try
            {
                //TODO: 如果出现数据发送卡段,则更换为异步
                Socket.Send(packet);
                
            }
            catch (Exception e)
            {
#if UNITY_EDITOR
                Debug.LogError(e.Message);
#endif
            }
        }

        #endregion
    }
}

