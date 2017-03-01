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


namespace Ass7ArielZannou
{
    public partial class Form1 : Form
    {
       // private delegate void kdelegate(object temp);
        private delegate void kdelegate(string temp, int type);
        private delegate void kdelegate1(object temp1);
        private delegate void kdelegate2(int row, int column, int chipColor);
        private Thread TalkThread = null;
        NetworkStream Connectstream = null;
        private static BinaryFormatter formatter = new BinaryFormatter();
        CPacket.LoginPacket login = new CPacket.LoginPacket();
        /*HOST-CLIENT*/
        CPacket.StartGame hosting;
        private Thread P2P = null;
        private NetworkStream P2PConnectStream;
        private TcpListener HostListener;
        private Socket Hostconnection;
        private static BinaryFormatter Newformater = new BinaryFormatter();
        private TableLayoutPanel tableLayoutPanelGame;
        private Connect4PictureBox[,] GamePicBox;
        private int[] heights = { 0, 0, 0, 0, 0, 0, 0};
        int OldHeight;
        int OldWidth;
        bool which;
        IPAddress[] hostInSession;
        CPacket.GameData NewPack = new CPacket.GameData();
        AData[] AllConnection;
        
        int next = 0;
        public Form1()
        {
            InitializeComponent();
            PictureBoxState.Image = ClientList.Images[0];
            pictureBoxRequestState.Image = ClientList.Images[2];
            OldHeight = Height;
            OldWidth = Width;
            this.GameDesign();
            ChooseColumn.Minimum = 0;
            ChooseColumn.Maximum = 6;
            AllConnection = new AData[2];
            hostInSession = new IPAddress[5];

        }
        private void Form1_ResizeEnd(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TcpClient client;
            login.user_id = usernametextbox.Text;
           client = new TcpClient();
            try
            {
                //textBox1.Text = "127.0.0.1";
                client.Connect(textBox1.Text, 3005);
                
                Connectstream = client.GetStream();

                if (TalkThread == null || !TalkThread.IsAlive)
                {
                    TalkThread = new Thread(new ThreadStart(ListenProcedure));
                    TalkThread.Start();
                    PictureBoxState.Image = ClientList.Images[1];
                    formatter.Serialize(Connectstream, login);
               }
            }
            catch (SocketException error) 
            { 
                MessageBox.Show("Server Must be down");
                PictureBoxState.Image = ClientList.Images[0];//change icons
            }
           

        }
        public void AddToList(string id, int type)
        {
            if(type == 1)
            {
                Connected.Items.Add(id);
            }
            if (type == 4)
            {
                Connected.Items.Remove(id);
            }
            if (type == 2)
            {
                Chat.Items.Add(id);
            }
            
        }
        public void RemovefromToList(object temp)
        {
            if(temp is CPacket.UserInSession)
            {
                CPacket.UserInSession usertoremove = new CPacket.UserInSession();
                usertoremove = temp as CPacket.UserInSession;
                hostInSession[next] = usertoremove.hostIp;
                //if(usertoremove.user1 == login.user_id || usertoremove.user2 == login.user_id)
                //{ 
                Connected.Items.Remove(usertoremove.user1);
                Connected.Items.Remove(usertoremove.user2);
                //}
                //else 
                //{
                  PeopleInSession.Items.Add(usertoremove.user2 + "--->" + usertoremove.user1);
                //}
                next++;
            }
        }
        public void ListenProcedure()
        {
            object temp;
            //object UserList;
            try
            {
                while (true)
                {
                    temp = formatter.Deserialize(Connectstream);
                    PictureBoxState.Image = ClientList.Images[1];//change icons
                    if (temp is CPacket.LoginPacket)
                    {
                        if (Connected.InvokeRequired)
                        {
                            BeginInvoke(new kdelegate(AddToList),((CPacket.LoginPacket)temp).user_id, 1);
                        }
                        
                    }
                    //string user = ((CPacket.LoginPacket)temp).user_id;
                    if (temp is CPacket.LogOut)
                    {
                        MessageBox.Show("Server Full");
                        PictureBoxState.Image = ClientList.Images[0];
                        Connectstream.Close();
                    }
                    if (temp is CPacket.LogOutUser)
                    {
                        if (Connected.InvokeRequired)
                        {
                            BeginInvoke(new kdelegate(AddToList),((CPacket.LogOutUser)temp).UsersDisconnected, 4);
                        }

                    }
                    if(temp is CPacket.UserInSession)
                    {
                        CPacket.UserInSession list = new CPacket.UserInSession();
                        list = temp as CPacket.UserInSession;
                        if (Connected.InvokeRequired)
                        {
                            BeginInvoke(new kdelegate1(RemovefromToList), list);
                        }
                    }
                    if (temp is CPacket.RequestGame)
                    {
                        CPacket.RequestGame whoasked;
                        whoasked = temp as CPacket.RequestGame;
                        RequestForm requestform = new RequestForm();
                        if (requestform.ShowDialog() == DialogResult.Yes)
                        {
                            CPacket.AcceptRequestGame RequestAccept = new CPacket.AcceptRequestGame();
                            RequestAccept.Response = true;
                            RequestAccept.requested_ID = login.user_id;
                            RequestAccept.requester_ID = whoasked.requestedusers;
                            RequestAccept.requester_IP = whoasked.requestedusers_ip;
                            formatter.Serialize(Connectstream, RequestAccept);
                        }
                        else
                        {
                            CPacket.AcceptRequestGame RequestAccept = new CPacket.AcceptRequestGame();
                            RequestAccept.Response = false;
                            formatter.Serialize(Connectstream, RequestAccept);
                        }
                    }
                        if (temp is CPacket.StartGame)
                        {
                            hosting = new CPacket.StartGame();
                            hosting = temp as CPacket.StartGame;
                            if(hosting.Which == true)
                            {
                              
                            //HostMode
                            Int32 port = 3006;
                            which = true;
                            //NewPack = new CPacket.GameData();
                            HostListener = new TcpListener(hosting.Host_IP, port);
                             HostListener.Start();
                               
                               
                               try
                               { 

                                Hostconnection = HostListener.AcceptSocket();
                                P2PConnectStream = new NetworkStream(Hostconnection);//temporary Networkstream 
                                pictureBoxRequestState.Image = ClientList.Images[1];
                                P2P = new Thread(new ThreadStart(PeerToPeerConnectionHost));
                                P2P.Start();
                                //AllThreads[NextLocation] = new Thread(new ParameterizedThreadStart(AreYouTalkingtome));
                                //AllThreads[NextLocation].Start(AllSockets[NextLocation]);
                                }
                                catch (SocketException e) { MessageBox.Show("No Communication"); }

                            }
                            else
                            {
                            //ClientMode
                                which = false;
                            //MessageBox.Show("Made it to peer to peer start game");
                            TcpClient Peer2Peer;
                                Peer2Peer = new TcpClient();
                                try
                                {
                                    Peer2Peer.Connect(hosting.Host_IP, 3006);
                                    P2PConnectStream = Peer2Peer.GetStream();
                                    if (P2P == null || !P2P.IsAlive)
                                    {
                                    pictureBoxRequestState.Image = ClientList.Images[1];
                                    P2P = new Thread(new ThreadStart(PeerToPeerConnectionClient));
                                        P2P.Start();
                                    }

                                }
                                catch (SocketException error)
                                {

                                }
                              
                            }
                         

                        }


                    }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(" Lost Connection Client");
                //PictureBoxState.Image = ClientList.Images[0];
            }

        }
        /*-----------------------------Peer---To---Peer-------*/
        private void PeerToPeerConnectionHost()
        {
            object HostTemp;
            //NewPack.ChipColor = true;
            try
            {
                while (true)
                {
                    HostTemp = formatter.Deserialize(P2PConnectStream);
                    if (HostTemp is CPacket.Message)
                    {
                        CPacket.Message receptionMessage = new CPacket.Message();
                        receptionMessage = HostTemp as CPacket.Message;
                        if (Chat.InvokeRequired)
                        {
                            BeginInvoke(new kdelegate(AddToList), ((CPacket.Message)HostTemp).m_message, 2);
                        }
                        //MessageBox.Show(receptionMessage.m_message);
                    }
                    if (HostTemp is CPacket.LogOut)
                    {
                        CPacket.LogOut LogOut = new CPacket.LogOut();
                        LogOut = HostTemp as CPacket.LogOut;
                        MessageBox.Show(LogOut.UsersDisconnected + " disconnected");
                        if(P2PConnectStream != null || !P2P.IsAlive)
                        {
                            if(which == true)//hostmode
                            {
                                HostListener.Stop();
                                P2PConnectStream.Close();
                                P2PConnectStream = null;
                            }
                            if(which == false)
                            {
                                P2PConnectStream.Close();
                                P2PConnectStream = null;

                            }
                           
                            //P2PConnectStream = null;
                            break;//makes the thread die
                        }
                   }
                    if(HostTemp is CPacket.GameData)
                    {
                       
                        NewPack = HostTemp as CPacket.GameData;
                        UpdateBoard(NewPack);
                     
                    }
                    if(HostTemp is CPacket.LoginPacket)
                    {
                        CPacket.LoginPacket Viewer = new CPacket.LoginPacket();
                        Viewer = HostTemp as CPacket.LoginPacket;
                        //formatter.Serialize()

                    }
                    if (HostTemp is CPacket.Again)
                    {
                       // MessageBox.Show("You Lost");
                        
                        //CPacket.Again againReceived = new CPacket.Again();
                        //againReceived = HostTemp as CPacket.Again;
                        ////if (againReceived.response == true)
                        //{

                           // this.EraseBoard();
                            //.GameDesign();

                        //}
                    }
                    if (HostTemp is CPacket.Losing)
                    {
                        MessageBox.Show("You Lost");

                        //CPacket.Again againReceived = new CPacket.Again();
                        //againReceived = HostTemp as CPacket.Again;
                        ////if (againReceived.response == true)
                        //{

                        // this.EraseBoard();
                        //.GameDesign();

                        //}
                    }
                }
            }
            catch(System.IO.IOException P2Perror)
            {
                //System.Runtime.Serialization.SerializationException
            }

        }
        private void PeerToPeerConnectionClient()
        {
            object HostTemp;
            //NewPack.ChipColor = true;
            try
            {
                while (true)
                {
                    HostTemp = formatter.Deserialize(P2PConnectStream);
                    if (HostTemp is CPacket.Message)
                    {
                        CPacket.Message receptionMessage = new CPacket.Message();
                        receptionMessage = HostTemp as CPacket.Message;
                        if (Chat.InvokeRequired)
                        {
                            BeginInvoke(new kdelegate(AddToList), ((CPacket.Message)HostTemp).m_message, 2);
                        }
                        //MessageBox.Show(receptionMessage.m_message);
                    }
                    if (HostTemp is CPacket.LogOut)
                    {
                        CPacket.LogOut LogOut = new CPacket.LogOut();
                        LogOut = HostTemp as CPacket.LogOut;
                        MessageBox.Show(LogOut.UsersDisconnected + " disconnected");
                        P2PConnectStream.Close();
                        P2PConnectStream = null;


                    }
                    if (HostTemp is CPacket.GameData)
                    {
                        //CPacket.GameData ReceivedPacket = new CPacket.GameData();
                       NewPack = HostTemp as CPacket.GameData;
                     
                       UpdateBoard(NewPack);
                      
                    }
                    if (HostTemp is CPacket.Again)
                    {
                        //MessageBox.Show("You Lost");
                        //this.EraseBoard();
                        //CPacket.Again againReceived = new CPacket.Again();
                        //againReceived = HostTemp as CPacket.Again;
                       // if (againReceived.response == true)
                       // {
                           // this.GameDesign();

                        //}
                    }
                    if (HostTemp is CPacket.Losing)
                    {
                        MessageBox.Show("You Lost");

                    }
                }
            }
            catch (System.IO.IOException P2Perror)
            {

            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if (P2PConnectStream != null || !P2P.IsAlive)
            {
                CPacket.Message NewMessage = new CPacket.Message();
                NewMessage.m_message = TextBoxMessage.Text;
                Chat.Items.Add(usernametextbox.Text + TextBoxMessage.Text);
                formatter.Serialize(P2PConnectStream, NewMessage);
            }
            else
            {
                MessageBox.Show("No connection P2P available");
            }
           
        }
        /*-----------------------------Peer---To---Peer-------*/

        private void button2_Click(object sender, EventArgs e)
        {
            
            if(P2PConnectStream != null && P2P.IsAlive)
            {
                
                    NewPack.column = Convert.ToInt32(ChooseColumn.Value);
                    NewPack.ChipColor = Convert.ToInt32(which);
                    formatter.Serialize(P2PConnectStream, NewPack);
                    UpdateBoard(NewPack);
              
                if(mycheck() == 0)
                {
                   //MessageBox.Show("No wins");
                }
                else if(mycheck() == 1)
                    {
                       // MessageBox.Show(login.user_id+" wins");
                    MessageBox.Show(" You Won");
                    CPacket.Losing Lose = new CPacket.Losing();
                    formatter.Serialize(P2PConnectStream, Lose);
                    AgainForm againform = new AgainForm();
                    //if (againform.ShowDialog() == DialogResult.Yes)
                   //{
                      // CPacket.Again again = new CPacket.Again();
                      // again.response = true;
                       // formatter.Serialize(P2PConnectStream, again);

                   // }
                }
            }
          else
            {
                MessageBox.Show("No Peer to Peer Connection established");
            }
           
        }

        //private void SendRequestGame_Click(object sender, EventArgs e)
        //{
        //    CPacket.RequestGame RequestGame = new CPacket.RequestGame();
        //    RequestGame.requestedusers = requestUsers.Text;
        //    RequestGame.requestedusers_ip = null;
        //    formatter.Serialize(Connectstream, RequestGame);
           
        //}
        public int mycheck()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 6 - 3; j++)
                {
                    //checks horizontal win
                    if (GamePicBox[i,j].ocupied == true  && GamePicBox[i,j].whichChip == GamePicBox[i, j+1].whichChip && GamePicBox[i, j].whichChip == GamePicBox[i, j+2].whichChip && GamePicBox[i, j].whichChip == GamePicBox[i, j+3].whichChip)
                        return 1;
                }
            }

            for (int i = 0; i < 7 - 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    //checks vertical win
                    if (GamePicBox[i, j].ocupied == true && GamePicBox[i, j].whichChip == GamePicBox[i+1, j].whichChip && GamePicBox[i, j].whichChip == GamePicBox[i+2, j].whichChip && GamePicBox[i, j].whichChip == GamePicBox[i+3, j].whichChip)
                        return 1;
                }
            }
            // Check for both diagonals ...
            //check for right diagonal
            for (int i = 0; i < 7 - 3; i++)
            {
                for (int j = 0; j < 6 - 3; j++)
                {
                    if (GamePicBox[i, j].ocupied == true && GamePicBox[i, j].whichChip == GamePicBox[i + 1, j + 1].whichChip && GamePicBox[i, j].whichChip == GamePicBox[i + 2, j + 2].whichChip
                          && GamePicBox[i, j].whichChip == GamePicBox[i + 3, j + 3].whichChip)
                        return 1;
                }
                    
            }
            //Check for left diagonal
            for (int i = 0; i < 7 - 3; i++)
            {
                for (int j = 3; j < 6; j++)
                {
                    ///if (board[i][j] != 0 && board[i][j] == board[i + 1][j - 1] && board[i][j] == board[i + 2][j - 2] && board[i][j] == board[i + 3][j - 3])
                    /// 
                    if (GamePicBox[i, j].ocupied == true && GamePicBox[i, j].whichChip == GamePicBox[i + 1, j - 1].whichChip &&
                    GamePicBox[i, j].whichChip == GamePicBox[i + 2, j - 2].whichChip && GamePicBox[i, j].whichChip == GamePicBox[i + 3, j - 3].whichChip)
                         return 1;
                }

                   
            }
               


            return 0;
        }

        private void SendRequestGameSelect_Click(object sender, EventArgs e)
        {
            try
            {
                CPacket.RequestGame RequestGame = new CPacket.RequestGame();
                RequestGame.requestedusers = Connected.SelectedItem.ToString();
                RequestGame.requestedusers_ip = null;
                formatter.Serialize(Connectstream, RequestGame);
            }
            catch(System.ArgumentNullException q)
            {

            }
           

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (TalkThread.IsAlive && Connectstream != null)
                {
                    try
                    {
                        PictureBoxState.Image = ClientList.Images[0];
                        CPacket.LogOut LogOut = new CPacket.LogOut();
                        LogOut.UsersDisconnected = login.user_id;
                        formatter.Serialize(Connectstream, LogOut);
                        TalkThread.Abort();
                        Thread.Sleep(1000);
                        Connectstream.Close();

                    }
                    catch (System.IO.IOException) { MessageBox.Show("Bye");
                    }
                if(P2PConnectStream != null && P2P.IsAlive)
                {
                    CPacket.LogOut LogOut = new CPacket.LogOut();
                    LogOut.UsersDisconnected = login.user_id;
                    formatter.Serialize(P2PConnectStream, LogOut);
                    P2PConnectStream.Close();
                    P2P.Abort();
                    P2PConnectStream = null;
                   
                }
                }
                else
                {
                    TalkThread.Abort();
                    Connectstream.Close();
                }
            }catch(System.NullReferenceException)
            {
                MessageBox.Show("No connection");
            }
            
            


        }

        private void GamePanel_Paint(object sender, PaintEventArgs e)
        {

        }
        private void GameDesign()
        {
            tableLayoutPanelGame = new TableLayoutPanel();
            tableLayoutPanelGame.ColumnCount=0;
            tableLayoutPanelGame.RowCount = 0;
            tableLayoutPanelGame.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelGame.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelGame.Name = "Game";
            tableLayoutPanelGame.Width = GamePanel.Width;
            tableLayoutPanelGame.Height = GamePanel.Height / 2;
           GamePicBox = new Connect4PictureBox[7,6];
            for (int row=0;row<7;row++)
            {
               
                for (int column = 0; column < 6; column++)
                {
                    GamePicBox[row, column] = new Connect4PictureBox();
                    
                        GamePicBox[row, column] = new Connect4PictureBox();
                        GamePicBox[row, column].BackColor = System.Drawing.SystemColors.ActiveCaptionText;
                        GamePicBox[row, column].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                        GamePicBox[row, column].SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                        GamePicBox[row, column].Dock = System.Windows.Forms.DockStyle.Fill;
                        GamePicBox[row, column].Margin = new System.Windows.Forms.Padding(0);
                        ((System.ComponentModel.ISupportInitialize)(GamePicBox[row, column])).BeginInit();
                        GamePicBox[row, column].SuspendLayout();
                        GamePicBox[row, column].Width = tableLayoutPanelGame.Width / 7;
                        GamePicBox[row, column].Height = tableLayoutPanelGame.Height / 6;
                        GamePicBox[row, column].Name = "(" + row + " : " + column + ")";
                        GamePicBox[row, column].TabStop = false;
                        GamePicBox[row, column].CreateGraphics();
                        GamePicBox[row, column].row = row;
                        GamePicBox[row, column].column = column;
                        GamePicBox[row, column].click = false;
                        GamePicBox[row, column].ocupied = false;
                        //GamePicBox[row, column].Click += new System.EventHandler(GamePicBox_Click);
                        tableLayoutPanelGame.Controls.Add(GamePicBox[row, column], row, column);
                            
                            
                }
            }
            tableLayoutPanelGame.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, tableLayoutPanelGame.Width / 7));
            tableLayoutPanelGame.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, tableLayoutPanelGame.Height / 6));
            tableLayoutPanelGame.Dock = System.Windows.Forms.DockStyle.Fill;
            GamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            GamePanel.Controls.Add(tableLayoutPanelGame, 0, 0);
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show(GamePanel.Width + ":" + GamePanel.Height);
        }
      

       public void UpdateBoard(CPacket.GameData temp)
        {//UPDATE BOARD
            int row = heights[temp.column];
            heights[temp.column]++;
            GamePicBox[temp.column, 5 - row].ocupied = true;
            if(temp.ChipColor == 0)
            {
                GamePicBox[temp.column, 5 - row].whichChip = 1;
                GamePicBox[temp.column, 5 - row].Image = GameImageListe.Images[0];
            }
            else
            {
                GamePicBox[temp.column, 5 - row].whichChip = 2;
                GamePicBox[temp.column, 5 - row].Image = GameImageListe.Images[1];
            }
          

        }//UPDATE BOARD
        public void EraseBoard()
        {
            for (int row = 0; row < 7; row++)
            {
                for (int column = 0; column < 6; column++)
                {

                    this.Controls.Remove(GamePicBox[row, column]);
                    GamePicBox[row, column] = null;
                }
            }
            GamePanel.Controls.Remove(this.tableLayoutPanelGame);

            tableLayoutPanelGame = null;
        }
        private void WatchGame_Click(object sender, EventArgs e)
       {
          // TcpClient Watherclient;
           //CPacket.WatchingRequest WatchRequest = new CPacket.WatchingRequest();
           //WatchRequest.watcher = usernametextbox.Text;
           //Int32 port = Convert.ToInt32(PortLabel.Text);
          // Watherclient.Connect(textBox1.Text,port);
           //Connectstream = client.GetStream();
       }
        }
        }

    


