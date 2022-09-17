using System;

namespace M_Socket
{
    public class NetManager: Singleton<NetManager>
    {
        //客户端连接对象
        private ClientPeer client = new ClientPeer("127.0.0.1",6666);
        
        private void Start()
        {
            client.Connect();
        }
        
        public void Execute(object message)
        {
            client.Send(message as SocketMsg);
        }
        
        private void Update()
        {
            if (client == null) return;
            while (client.SocketMsgQueue.Count >0)
            {
                SocketMsg msg = client.SocketMsgQueue.Dequeue();
                //TODO: 操作这个msg 
            }
        }
    }
}