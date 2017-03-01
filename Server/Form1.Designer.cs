namespace Server
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
            this.StartServer = new System.Windows.Forms.Button();
            this.StopServer = new System.Windows.Forms.Button();
            this.CheckConnect = new System.Windows.Forms.PictureBox();
            this.IconList = new System.Windows.Forms.ImageList(this.components);
            this.UserBox = new System.Windows.Forms.ListBox();
            this.UserInSession = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.CheckConnect)).BeginInit();
            this.SuspendLayout();
            // 
            // StartServer
            // 
            this.StartServer.Location = new System.Drawing.Point(12, 12);
            this.StartServer.Name = "StartServer";
            this.StartServer.Size = new System.Drawing.Size(75, 23);
            this.StartServer.TabIndex = 0;
            this.StartServer.Text = "StartServer";
            this.StartServer.UseVisualStyleBackColor = true;
            this.StartServer.Click += new System.EventHandler(this.StartServer_Click);
            // 
            // StopServer
            // 
            this.StopServer.Location = new System.Drawing.Point(12, 41);
            this.StopServer.Name = "StopServer";
            this.StopServer.Size = new System.Drawing.Size(75, 23);
            this.StopServer.TabIndex = 1;
            this.StopServer.Text = "Stop Server";
            this.StopServer.UseVisualStyleBackColor = true;
            this.StopServer.Click += new System.EventHandler(this.StopServer_Click);
            // 
            // CheckConnect
            // 
            this.CheckConnect.Location = new System.Drawing.Point(12, 70);
            this.CheckConnect.Name = "CheckConnect";
            this.CheckConnect.Size = new System.Drawing.Size(75, 41);
            this.CheckConnect.TabIndex = 2;
            this.CheckConnect.TabStop = false;
            // 
            // IconList
            // 
            this.IconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconList.ImageStream")));
            this.IconList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconList.Images.SetKeyName(0, "officonds.jpg");
            this.IconList.Images.SetKeyName(1, "onicons.png");
            this.IconList.Images.SetKeyName(2, "glossy-yellow-icon-button-md.png");
            // 
            // UserBox
            // 
            this.UserBox.FormattingEnabled = true;
            this.UserBox.Location = new System.Drawing.Point(93, 16);
            this.UserBox.Name = "UserBox";
            this.UserBox.Size = new System.Drawing.Size(75, 95);
            this.UserBox.TabIndex = 3;
            // 
            // UserInSession
            // 
            this.UserInSession.FormattingEnabled = true;
            this.UserInSession.Location = new System.Drawing.Point(174, 16);
            this.UserInSession.Name = "UserInSession";
            this.UserInSession.Size = new System.Drawing.Size(75, 95);
            this.UserInSession.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(254, 115);
            this.Controls.Add(this.UserInSession);
            this.Controls.Add(this.UserBox);
            this.Controls.Add(this.CheckConnect);
            this.Controls.Add(this.StopServer);
            this.Controls.Add(this.StartServer);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.CheckConnect)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartServer;
        private System.Windows.Forms.Button StopServer;
        private System.Windows.Forms.PictureBox CheckConnect;
        private System.Windows.Forms.ImageList IconList;
        private System.Windows.Forms.ListBox UserBox;
        private System.Windows.Forms.ListBox UserInSession;
    }
}

