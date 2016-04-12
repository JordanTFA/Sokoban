//*******************************************************************
//* Copyright: J. van den Bergh (2004)
//* Email: jasper@arnhemnet.nl
//* URL: http://www.jabbah.nl/sokobanpro
//*
//* Do with the code whatever you want. If you want to use this code
//* for any commercial purposes, contact me.
//*******************************************************************

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SokobanPro
{
	/// <summary>
	/// Lets the user pick a player or create a new player
	/// </summary>
	public class FormPlayers : System.Windows.Forms.Form
	{
        private System.Windows.Forms.GroupBox grpNewPlayer;
        private System.Windows.Forms.GroupBox grpSelectPlayer;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.ListBox lstPlayers;
        private System.Windows.Forms.CheckBox chkContinuePrev;
        private System.Windows.Forms.Button btnStartExisting;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Button btnStartNew;
		private System.ComponentModel.Container components = null;

        private string playerName = string.Empty;
        
        // Indicate if we want to continue an existing game or start a new one
        private bool continueExistingGame = true;
        
        // Indicate if we've picked an existing player or create a new one
        private bool newPlayer = false;
        

        /// <summary>
        /// Constructor
        /// </summary>
		public FormPlayers()
		{
			InitializeComponent();
			
			ArrayList players = PlayerData.GetPlayers();
			
			// List player names in listbox, if any
			if (players.Count == 0)
			    btnStartExisting.Enabled = false;
			else
			    lstPlayers.DataSource = players;
		}


        /// <summary>
        /// Executed when we select an existing player. Also indicate if we're
        /// continuing the previous game or not.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartExisting_Click(object sender, System.EventArgs e)
        {
            playerName = lstPlayers.SelectedItem.ToString();
            continueExistingGame = chkContinuePrev.Checked ? true : false;
            
            this.Close();
        }


        /// <summary>
        /// Executed when we create a new player.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartNew_Click(object sender, System.EventArgs e)
        {
            playerName = txtPlayerName.Text;
            continueExistingGame = false;
            
            if (txtPlayerName.Text == "")
                MessageBox.Show("Please enter a player name.", "Whoops!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                newPlayer = true;
                this.Close();
            }
        }
		
		
		// Properties
		
        public string PlayerName
        {
            get { return playerName; }
        }
		
        public bool ContinueExistingGame
        {
            get { return continueExistingGame; }
        }
        
        public bool NewPlayer
        {
            get { return newPlayer; }
        }
        

		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.grpNewPlayer = new System.Windows.Forms.GroupBox();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.btnStartNew = new System.Windows.Forms.Button();
            this.grpSelectPlayer = new System.Windows.Forms.GroupBox();
            this.btnStartExisting = new System.Windows.Forms.Button();
            this.chkContinuePrev = new System.Windows.Forms.CheckBox();
            this.lstPlayers = new System.Windows.Forms.ListBox();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.grpNewPlayer.SuspendLayout();
            this.grpSelectPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNewPlayer
            // 
            this.grpNewPlayer.Controls.Add(this.txtPlayerName);
            this.grpNewPlayer.Controls.Add(this.lblPlayerName);
            this.grpNewPlayer.Controls.Add(this.btnStartNew);
            this.grpNewPlayer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.grpNewPlayer.Location = new System.Drawing.Point(24, 192);
            this.grpNewPlayer.Name = "grpNewPlayer";
            this.grpNewPlayer.Size = new System.Drawing.Size(376, 72);
            this.grpNewPlayer.TabIndex = 0;
            this.grpNewPlayer.TabStop = false;
            this.grpNewPlayer.Text = "Create new player";
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(96, 32);
            this.txtPlayerName.MaxLength = 30;
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(152, 21);
            this.txtPlayerName.TabIndex = 1;
            this.txtPlayerName.Text = "";
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.Location = new System.Drawing.Point(16, 32);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(80, 16);
            this.lblPlayerName.TabIndex = 0;
            this.lblPlayerName.Text = "Player name:";
            // 
            // btnStartNew
            // 
            this.btnStartNew.Location = new System.Drawing.Point(288, 32);
            this.btnStartNew.Name = "btnStartNew";
            this.btnStartNew.TabIndex = 3;
            this.btnStartNew.Text = "Start game";
            this.btnStartNew.Click += new System.EventHandler(this.btnStartNew_Click);
            // 
            // grpSelectPlayer
            // 
            this.grpSelectPlayer.Controls.Add(this.btnStartExisting);
            this.grpSelectPlayer.Controls.Add(this.chkContinuePrev);
            this.grpSelectPlayer.Controls.Add(this.lstPlayers);
            this.grpSelectPlayer.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.grpSelectPlayer.Location = new System.Drawing.Point(24, 56);
            this.grpSelectPlayer.Name = "grpSelectPlayer";
            this.grpSelectPlayer.Size = new System.Drawing.Size(376, 120);
            this.grpSelectPlayer.TabIndex = 1;
            this.grpSelectPlayer.TabStop = false;
            this.grpSelectPlayer.Text = "Select player";
            // 
            // btnStartExisting
            // 
            this.btnStartExisting.Location = new System.Drawing.Point(288, 80);
            this.btnStartExisting.Name = "btnStartExisting";
            this.btnStartExisting.TabIndex = 2;
            this.btnStartExisting.Text = "Start game";
            this.btnStartExisting.Click += new System.EventHandler(this.btnStartExisting_Click);
            // 
            // chkContinuePrev
            // 
            this.chkContinuePrev.Checked = true;
            this.chkContinuePrev.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkContinuePrev.Location = new System.Drawing.Point(224, 32);
            this.chkContinuePrev.Name = "chkContinuePrev";
            this.chkContinuePrev.Size = new System.Drawing.Size(144, 24);
            this.chkContinuePrev.TabIndex = 1;
            this.chkContinuePrev.Text = "Continue previous game";
            // 
            // lstPlayers
            // 
            this.lstPlayers.Location = new System.Drawing.Point(16, 32);
            this.lstPlayers.Name = "lstPlayers";
            this.lstPlayers.Size = new System.Drawing.Size(192, 69);
            this.lstPlayers.TabIndex = 0;
            // 
            // lblPlayers
            // 
            this.lblPlayers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblPlayers.Location = new System.Drawing.Point(24, 24);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(384, 16);
            this.lblPlayers.TabIndex = 2;
            this.lblPlayers.Text = "Pick a player or create a new player";
            // 
            // FormPlayers
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(424, 283);
            this.ControlBox = false;
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.grpNewPlayer);
            this.Controls.Add(this.grpSelectPlayer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPlayers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Players";
            this.grpNewPlayer.ResumeLayout(false);
            this.grpSelectPlayer.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if(components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }
        
		#endregion
	}
}
