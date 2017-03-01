using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class LoginData
    {
        public NetworkStream ConnStream;
        public Socket TheSocket;
        public bool Connected;
        public string user_id;
        public IPAddress user_ip;
        public int position;
    }
 
}
