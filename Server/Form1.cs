using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
//using System.Net.Sockets;
namespace Server
{
    public partial class Form1 : Form
    {
        private delegate void kdelegate(object temp);

        private TcpListener listener;
        private Socket connection;
        private Thread WFCThread = null;
        private static BinaryFormatter formatter = new BinaryFormatter();
        Thread[] AllThreads;
        LoginData[] AllSockets;
        Stack<int> openLocation = new Stack<int>();
        int NextLocation=0;
        //CPacket.WatchingRequest[] Session;
        //int ListNumber = 0;
        public Form1()
        {
            InitializeComponent();
            CheckConnect.Image = IconList.Images[0];
            AllThreads = new Thread[5];  // creates array
            AllSockets = new LoginData[5];   // creates array
           // Session = new CPacket.WatchingRequest[5];
            
        }

        private void StartServer_Click(object sender, EventArgs e)
        {
            if (WFCThread == null || !WFCThread.IsAlive)
            {
                
                WFCThread = new Thread(new ThreadStart(WFCProcedure));
                WFCThread.Start();
                CheckConnect.Image = IconList.Images[2];

            }
        }//Start Button
       public void AddToList(object temp)
        {
            if (temp is string)
            {
                string user = temp as string;
               
                UserBox.Items.Add(user);
            }
            if (temp is CPacket.LogOutUser)
            {
                CPacket.LogOutUser user = new CPacket.LogOutUser();
                user = temp as CPacket.LogOutUser;
                UserBox.Items.Remove(user.UsersDisconnected);
            }
            if (temp is CPacket.AcceptRequestGame)
            {
                CPacket.AcceptRequestGame list = new CPacket.AcceptRequestGame();
                list = temp as CPacket.AcceptRequestGame;
                CPacket.UserInSession listInSession = new CPacket.UserInSession();
                listInSession.user1 = list.requested_ID;
                listInSession.user2 = list.requester_ID;
                listInSession.hostIp = list.requester_IP;
                UserBox.Items.Remove(list.requested_ID);
                UserBox.Items.Remove(list.requester_ID);
                //Session[ListNumber].hostIp = list.requester_IP;
                //Session[ListNumber].HostPlayer = list.requester_ID;
                
               UserInSession.Items.Add(list.requester_ID + "-----> " + list.requested_ID);
                for(int x=0;x<NextLocation;x++)
                {
                   //if (AllSockets[x].user_id != list.requested_ID && AllSockets[x].user_id != list.requester_ID)
                   //{
                        formatter.Serialize(AllSockets[x].ConnStream, listInSession);
                   //}
                    
                }
                //ListNumber++;

            }
           
        }
        public void WFCProcedure()
        {

            int y;
            for (int x = 4; x>=0;x--)
            {
                openLocation.Push(x);
            }
            NetworkStream tempstream = null;
            Int32 port = 3005;
            LoginData User = new LoginData();
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            object temp = null;
            try
            {
                while (true)
                {

                    connection = listener.AcceptSocket();    // blocking call
                    CheckConnect.Image = IconList.Images[1];//change the icon on server form
                    tempstream = new NetworkStream(connection);//temporary Networkstream 
                    temp = formatter.Deserialize(tempstream);

                    if (temp is CPacket.LoginPacket)
                    {
                        User.user_id = ((CPacket.LoginPacket)temp).user_id;
                        User.user_ip = ((IPEndPoint)connection.RemoteEndPoint).Address;

                    }


                
                    if (openLocation.Count == 0)
                    {
                        CPacket.LogOut LogOut = new CPacket.LogOut();
                        formatter.Serialize(tempstream, LogOut);

                    }
                  else
                    {
                        openLocation.Pop();
                        AllSockets[NextLocation] = new LoginData();
                        AllSockets[NextLocation].TheSocket = connection;
                        AllSockets[NextLocation].ConnStream = tempstream;
                        AllSockets[NextLocation].Connected = true;
                        AllSockets[NextLocation].user_id = User.user_id;
                        AllSockets[NextLocation].user_ip = User.user_ip;
                        //MessageBox.Show(User.user_id + " is connected");
                        AllSockets[NextLocation].position = NextLocation; ;
        
                        CheckConnect.Image = IconList.Images[1];
                      
                        CPacket.LoginPacket UserConnected = new CPacket.LoginPacket();
                       
                            if (UserBox.InvokeRequired)
                            {
                                BeginInvoke(new kdelegate(AddToList), User.user_id);
                            }
                        
                       
                        for (int x = 0; x < NextLocation; x++)
                        {
                            if (AllSockets[x] != null && AllSockets[x].user_id != User.user_id)
                            {
                              UserConnected.user_id = AllSockets[x].user_id;
                              formatter.Serialize(AllSockets[x].ConnStream, temp);//send old users new user
                              formatter.Serialize(tempstream, UserConnected);// send new user old users
                           }
                           
                        }
                        AllThreads[NextLocation] = new Thread(new ParameterizedThreadStart(AreYouTalkingtome));
                        AllThreads[NextLocation].Start(AllSockets[NextLocation]);
                       
                        NextLocation++;
                    }
                }
            }
            catch (SocketException e) { MessageBox.Show("Server Shutting down!"); }
        }//Wait for connection


        public void AreYouTalkingtome(object obj)
        {//Are You Talking to me
            LoginData who = null;
            object temp;
            CPacket.RequestGame TempPacket;
            if (obj is LoginData)
            {
                who = (LoginData)obj;
            }
            //.Items[i].BackColor = Color.Green;
            try
            {
                while (true)
                {
                    
                    temp = formatter.Deserialize(who.ConnStream);//Waiting for one of the client to takl
                    if (temp is CPacket.RequestGame)//What i'm Getting
                    {
                        try
                        {
                            TempPacket = temp as CPacket.RequestGame;
                            TempPacket.requestedusers = ((CPacket.RequestGame)temp).requestedusers;
                            //MessageBox.Show("Requeste Received");

                            for (int x = 0; x <= NextLocation; x++)
                            {
                                if (AllSockets[x] != null && TempPacket.requestedusers == AllSockets[x].user_id)
                                {
                                    //MessageBox.Show("User Found");
                                    CPacket.RequestGame NewTempPacket = new CPacket.RequestGame();
                                    NewTempPacket.requestedusers = who.user_id;
                                    NewTempPacket.requestedusers_ip = who.user_ip;

                                    formatter.Serialize(AllSockets[x].ConnStream, NewTempPacket);


                                }
                            }
                        }
                     catch(System.IO.IOException e)
                        {
                            //MessageBox.Show()
                        }
                        //if()
                        //System.IO.IOException
                    }
                    if(temp is CPacket.AcceptRequestGame)
                    {
                        CPacket.AcceptRequestGame ServerRequestAccepted = new CPacket.AcceptRequestGame();
                        ServerRequestAccepted = temp as CPacket.AcceptRequestGame;
                        if(ServerRequestAccepted.Response == true)
                        {
                            //MessageBox.Show(ServerRequestAccepted.requester_ID + " is asking " + ServerRequestAccepted.requested_ID);
                            CPacket.StartGame StartGame = new CPacket.StartGame();
                            
                            StartGame.Client_ID = ServerRequestAccepted.requested_ID;
                            StartGame.Host_ID = ServerRequestAccepted.requester_ID;
                            StartGame.Client_IP = ServerRequestAccepted.requested_IP;
                            StartGame.Host_IP = ServerRequestAccepted.requester_IP;
                           // StartGame.session = who.position;
                            //MessageBox.Show(Convert.ToString(StartGame.session));
                            //Send to Host
                            for (int x = 0; x < NextLocation; x++)
                            {
                                if (AllSockets[x] != null && StartGame.Host_ID == AllSockets[x].user_id)
                                {
                                    StartGame.Which = true;
                                    //MessageBox.Show(StartGame.Host_ID + " is the Host ");
                                    formatter.Serialize(AllSockets[x].ConnStream, StartGame);
                                }
                            }
                            //Send To Client 
                            for (int x=0;x<NextLocation;x++)
                            {
                                if(AllSockets[x] != null && StartGame.Client_ID == AllSockets[x].user_id)
                                {
                                    StartGame.Which = false;
                                    //MessageBox.Show(StartGame.Client_ID + " is the client ");
                                    formatter.Serialize(AllSockets[x].ConnStream, StartGame);
                                }
                            }

                            if (UserBox.InvokeRequired)
                            {
                                BeginInvoke(new kdelegate(AddToList), ServerRequestAccepted);
                            }

                        }
                        if (ServerRequestAccepted.Response == false)
                        {

                            MessageBox.Show(" Don't Create a peer to peer ");

                        }


                    }
                  
                    if (temp is CPacket.LogOut)
                    {
                       
                        try
                        {
                            CPacket.LogOutUser UsersLogedOutList = new CPacket.LogOutUser();
                            NextLocation--;
                            //UsersLogedOutList = temp as CPacket.LogOutUser;
                            UsersLogedOutList.UsersDisconnected = who.user_id;
                            for (int x = 0; x < NextLocation; x++)
                            {
                                if (AllSockets[x] != null)
                                {
                                    formatter.Serialize(AllSockets[x].ConnStream, UsersLogedOutList);
                                }
                                //System.IO.IOException
                            }
                            if (UserBox.InvokeRequired)
                            {
                                BeginInvoke(new kdelegate(AddToList), UsersLogedOutList);
                            }

                            AllSockets[who.position].ConnStream.Close();

                            AllSockets[who.position].ConnStream = null;

                            AllSockets[who.position] = null;
                            openLocation.Push(who.position);
                        }
                        catch (System.IO.IOException e)
                        {

                        }

                        if(temp is CPacket.WatchingRequest)
                        {
                            CPacket.WatchingRequest SessionRequested = new CPacket.WatchingRequest();
                            SessionRequested = temp as CPacket.WatchingRequest;

                        }
                       
                   
                     



                    }
                    break;
                }
                //WFCThread.Start();
            }
            catch (System.Runtime.Serialization.SerializationException e ) { MessageBox.Show("Client is OFF"); }

        }//Are You Talking to ME

        private void StopServer_Click(object sender, EventArgs e)
        {
            if (WFCThread != null && WFCThread.IsAlive)
            {
                listener.Stop();   // stops the blocking call listener.AcceptSocket();
                CheckConnect.Image = IconList.Images[0];
                NextLocation = 0;
            }

            for (int x = 0; x < 5; x++)
            {
                if (AllSockets[x] != null && AllThreads[x] != null && AllThreads[x].IsAlive)
                {
                    AllSockets[x].TheSocket.Close();
                    AllThreads[x].Join();
                    AllThreads[x] = null;
                    AllSockets[x] = null;
                }
            }
        }
    }
}
