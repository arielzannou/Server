namespace Ass7ArielZannou
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.GamePanel = new System.Windows.Forms.TableLayoutPanel();
            this.Menu = new System.Windows.Forms.GroupBox();
            this.ChooseColumn = new System.Windows.Forms.NumericUpDown();
            this.Column = new System.Windows.Forms.Label();
            this.Send = new System.Windows.Forms.Button();
            this.pictureBoxRequestState = new System.Windows.Forms.PictureBox();
            this.RequestLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.Label();
            this.Connected = new System.Windows.Forms.ListBox();
            this.usernametextbox = new System.Windows.Forms.TextBox();
            this.PictureBoxState = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.TextBoxMessage = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelIP = new System.Windows.Forms.Label();
            this.Chat = new System.Windows.Forms.ListBox();
            this.ClientList = new System.Windows.Forms.ImageList(this.components);
            this.GameImageListe = new System.Windows.Forms.ImageList(this.components);
            this.PeopleInSession = new System.Windows.Forms.ListBox();
            this.WatchGame = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.GamePanel.SuspendLayout();
            this.Menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChooseColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRequestState)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxState)).BeginInit();
            this.SuspendLayout();
            // 
            // GamePanel
            // 
            this.GamePanel.ColumnCount = 1;
            this.GamePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 495F));
            this.GamePanel.Controls.Add(this.Menu, 0, 1);
            this.GamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GamePanel.Location = new System.Drawing.Point(0, 0);
            this.GamePanel.Name = "GamePanel";
            this.GamePanel.RowCount = 2;
            this.GamePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.GamePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GamePanel.Size = new System.Drawing.Size(609, 486);
            this.GamePanel.TabIndex = 0;
            this.GamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.GamePanel_Paint);
            // 
            // Menu
            // 
            this.Menu.Controls.Add(this.PortLabel);
            this.Menu.Controls.Add(this.textBox2);
            this.Menu.Controls.Add(this.WatchGame);
            this.Menu.Controls.Add(this.PeopleInSession);
            this.Menu.Controls.Add(this.ChooseColumn);
            this.Menu.Controls.Add(this.Column);
            this.Menu.Controls.Add(this.Send);
            this.Menu.Controls.Add(this.pictureBoxRequestState);
            this.Menu.Controls.Add(this.RequestLabel);
            this.Menu.Controls.Add(this.label1);
            this.Menu.Controls.Add(this.UserName);
            this.Menu.Controls.Add(this.Connected);
            this.Menu.Controls.Add(this.usernametextbox);
            this.Menu.Controls.Add(this.PictureBoxState);
            this.Menu.Controls.Add(this.button2);
            this.Menu.Controls.Add(this.TextBoxMessage);
            this.Menu.Controls.Add(this.button1);
            this.Menu.Controls.Add(this.textBox1);
            this.Menu.Controls.Add(this.labelIP);
            this.Menu.Controls.Add(this.Chat);
            this.Menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Menu.Location = new System.Drawing.Point(3, 294);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(603, 189);
            this.Menu.TabIndex = 0;
            this.Menu.TabStop = false;
            this.Menu.Text = "Menu";
            // 
            // ChooseColumn
            // 
            this.ChooseColumn.Location = new System.Drawing.Point(478, 25);
            this.ChooseColumn.Name = "ChooseColumn";
            this.ChooseColumn.Size = new System.Drawing.Size(48, 20);
            this.ChooseColumn.TabIndex = 27;
            // 
            // Column
            // 
            this.Column.AutoSize = true;
            this.Column.Location = new System.Drawing.Point(435, 25);
            this.Column.Name = "Column";
            this.Column.Size = new System.Drawing.Size(42, 13);
            this.Column.TabIndex = 26;
            this.Column.Text = "Column";
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(248, 148);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(114, 23);
            this.Send.TabIndex = 21;
            this.Send.Text = "Send Message P2P";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // pictureBoxRequestState
            // 
            this.pictureBoxRequestState.Location = new System.Drawing.Point(428, 164);
            this.pictureBoxRequestState.Name = "pictureBoxRequestState";
            this.pictureBoxRequestState.Size = new System.Drawing.Size(37, 25);
            this.pictureBoxRequestState.TabIndex = 20;
            this.pictureBoxRequestState.TabStop = false;
            // 
            // RequestLabel
            // 
            this.RequestLabel.AutoSize = true;
            this.RequestLabel.Location = new System.Drawing.Point(357, 170);
            this.RequestLabel.Name = "RequestLabel";
            this.RequestLabel.Size = new System.Drawing.Size(75, 13);
            this.RequestLabel.TabIndex = 19;
            this.RequestLabel.Text = "Request State";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(461, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Connection To Server";
            // 
            // UserName
            // 
            this.UserName.AutoSize = true;
            this.UserName.Location = new System.Drawing.Point(157, 25);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(60, 13);
            this.UserName.TabIndex = 10;
            this.UserName.Text = "User Name";
            // 
            // Connected
            // 
            this.Connected.FormattingEnabled = true;
            this.Connected.Location = new System.Drawing.Point(351, 49);
            this.Connected.Name = "Connected";
            this.Connected.Size = new System.Drawing.Size(78, 95);
            this.Connected.TabIndex = 9;
            this.Connected.DoubleClick += new System.EventHandler(this.SendRequestGameSelect_Click);
            // 
            // usernametextbox
            // 
            this.usernametextbox.Location = new System.Drawing.Point(223, 22);
            this.usernametextbox.Name = "usernametextbox";
            this.usernametextbox.Size = new System.Drawing.Size(121, 20);
            this.usernametextbox.TabIndex = 8;
            // 
            // PictureBoxState
            // 
            this.PictureBoxState.Location = new System.Drawing.Point(569, 164);
            this.PictureBoxState.Name = "PictureBoxState";
            this.PictureBoxState.Size = new System.Drawing.Size(34, 24);
            this.PictureBoxState.TabIndex = 7;
            this.PictureBoxState.TabStop = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(537, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Send";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TextBoxMessage
            // 
            this.TextBoxMessage.Location = new System.Drawing.Point(6, 148);
            this.TextBoxMessage.Name = "TextBoxMessage";
            this.TextBoxMessage.Size = new System.Drawing.Size(236, 20);
            this.TextBoxMessage.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(351, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(29, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 20);
            this.textBox1.TabIndex = 2;
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(6, 25);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(17, 13);
            this.labelIP.TabIndex = 1;
            this.labelIP.Text = "IP";
            // 
            // Chat
            // 
            this.Chat.FormattingEnabled = true;
            this.Chat.Location = new System.Drawing.Point(0, 48);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(344, 95);
            this.Chat.TabIndex = 0;
            // 
            // ClientList
            // 
            this.ClientList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ClientList.ImageStream")));
            this.ClientList.TransparentColor = System.Drawing.Color.Transparent;
            this.ClientList.Images.SetKeyName(0, "officonds.jpg");
            this.ClientList.Images.SetKeyName(1, "onicons.png");
            this.ClientList.Images.SetKeyName(2, "glossy-yellow-icon-button-md.png");
            // 
            // GameImageListe
            // 
            this.GameImageListe.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("GameImageListe.ImageStream")));
            this.GameImageListe.TransparentColor = System.Drawing.Color.Transparent;
            this.GameImageListe.Images.SetKeyName(0, "red.png");
            this.GameImageListe.Images.SetKeyName(1, "yellow.png");
            // 
            // PeopleInSession
            // 
            this.PeopleInSession.FormattingEnabled = true;
            this.PeopleInSession.Location = new System.Drawing.Point(435, 48);
            this.PeopleInSession.Name = "PeopleInSession";
            this.PeopleInSession.Size = new System.Drawing.Size(77, 95);
            this.PeopleInSession.TabIndex = 28;
            // 
            // WatchGame
            // 
            this.WatchGame.Location = new System.Drawing.Point(518, 107);
            this.WatchGame.Name = "WatchGame";
            this.WatchGame.Size = new System.Drawing.Size(75, 23);
            this.WatchGame.TabIndex = 29;
            this.WatchGame.Text = "Watch Game";
            this.WatchGame.UseVisualStyleBackColor = true;
            this.WatchGame.Click += new System.EventHandler(this.WatchGame_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(520, 71);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(74, 20);
            this.textBox2.TabIndex = 30;
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(518, 55);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(26, 13);
            this.PortLabel.TabIndex = 31;
            this.PortLabel.Text = "Port";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 486);
            this.Controls.Add(this.GamePanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GamePanel.ResumeLayout(false);
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChooseColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRequestState)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxState)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel GamePanel;
        private System.Windows.Forms.GroupBox Menu;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.ListBox Chat;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox PictureBoxState;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox TextBoxMessage;
        private System.Windows.Forms.ImageList ClientList;
        private System.Windows.Forms.TextBox usernametextbox;
        private System.Windows.Forms.ListBox Connected;
        private System.Windows.Forms.Label UserName;
        private System.Windows.Forms.PictureBox pictureBoxRequestState;
        private System.Windows.Forms.Label RequestLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.ImageList GameImageListe;
        private System.Windows.Forms.Label Column;
        private System.Windows.Forms.NumericUpDown ChooseColumn;
        private System.Windows.Forms.Button WatchGame;
        private System.Windows.Forms.ListBox PeopleInSession;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.TextBox textBox2;
    }
}

