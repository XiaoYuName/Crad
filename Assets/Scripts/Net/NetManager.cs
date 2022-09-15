using System;

namespace M_Socket
{
    public class NetManager: ManagerBase
    {
        public static NetManager Instance = null;

        private void Awake()
        {
            Instance = this;
            Add(0,this);    
        }

        public override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case 0:
                    client.Send(message as SocketMsg);
                    break;
                default:
                    break;
            }
        }

        private void Start()
        {
            client.Connect();
        }
        //客户端连接对象
        private ClientPeer client = new ClientPeer("127.0.0.1",6666);


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