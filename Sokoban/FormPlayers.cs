// Jordan Aitken
// HND Software Development
// Sokoban Project

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace JordanSokoban
{
	/// <summary>
	/// Lets the user pick a player or create a new player
	/// </summary>
	public class FormPlayers : System.Windows.Forms.Form
    {
		private System.ComponentModel.Container components = null;

        private string playerName = string.Empty;
        
        // Indicate if we want to continue an existing game or start a new one
        private bool continueExistingGame = true;
        private GroupBox grpSquad;
        private RadioButton radioSenior;
        private RadioButton radioNonPlayer;
        private RadioButton radioJunior;
        private Button btnUpdate;
        private Label lblSquadName;
        private Label lblName;
        private Label lblSquad;
        private Label lblPlayerName;
        private Label lblPlayer;
        private ComboBox cmbPlayers;
        private TabControl tabCategories;
        private TabPage tabPassing;
        private Label lblStanVal;
        private TrackBar trkPop;
        private TrackBar trkSpin;
        private TrackBar trkStandard;
        private Label lblPassingComments;
        private TextBox txtPassingComments;
        private Label lblPop;
        private Label lblSpin;
        private Label lblStandard;
        private TabPage tabTackling;
        private TrackBar trkScrabble;
        private Label lblRear;
        private TrackBar trkFront;
        private Label lblTacklingComments;
        private TextBox txtTacklingComments;
        private Label lblScrabble;
        private Label lblSide;
        private Label lblFront;
        private TrackBar trkRear;
        private TrackBar trkSide;
        private TabPage tabKicking;
        private TrackBar trkGoal;
        private Label lblKickingComments;
        private TextBox txtKickingComments;
        private Label lblGoal;
        private Label lblGrubber;
        private Label lblPunt;
        private Label lblDrop;
        private TrackBar trkDrop;
        private TrackBar trkPunt;
        private TrackBar trkGrubber;
        private Label lblTitle;
        
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
            this.grpSquad = new System.Windows.Forms.GroupBox();
            this.radioSenior = new System.Windows.Forms.RadioButton();
            this.radioNonPlayer = new System.Windows.Forms.RadioButton();
            this.radioJunior = new System.Windows.Forms.RadioButton();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.lblSquadName = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSquad = new System.Windows.Forms.Label();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.cmbPlayers = new System.Windows.Forms.ComboBox();
            this.tabCategories = new System.Windows.Forms.TabControl();
            this.tabPassing = new System.Windows.Forms.TabPage();
            this.lblStanVal = new System.Windows.Forms.Label();
            this.trkPop = new System.Windows.Forms.TrackBar();
            this.trkSpin = new System.Windows.Forms.TrackBar();
            this.trkStandard = new System.Windows.Forms.TrackBar();
            this.lblPassingComments = new System.Windows.Forms.Label();
            this.txtPassingComments = new System.Windows.Forms.TextBox();
            this.lblPop = new System.Windows.Forms.Label();
            this.lblSpin = new System.Windows.Forms.Label();
            this.lblStandard = new System.Windows.Forms.Label();
            this.tabTackling = new System.Windows.Forms.TabPage();
            this.trkScrabble = new System.Windows.Forms.TrackBar();
            this.lblRear = new System.Windows.Forms.Label();
            this.trkFront = new System.Windows.Forms.TrackBar();
            this.lblTacklingComments = new System.Windows.Forms.Label();
            this.txtTacklingComments = new System.Windows.Forms.TextBox();
            this.lblScrabble = new System.Windows.Forms.Label();
            this.lblSide = new System.Windows.Forms.Label();
            this.lblFront = new System.Windows.Forms.Label();
            this.trkRear = new System.Windows.Forms.TrackBar();
            this.trkSide = new System.Windows.Forms.TrackBar();
            this.tabKicking = new System.Windows.Forms.TabPage();
            this.trkGoal = new System.Windows.Forms.TrackBar();
            this.lblKickingComments = new System.Windows.Forms.Label();
            this.txtKickingComments = new System.Windows.Forms.TextBox();
            this.lblGoal = new System.Windows.Forms.Label();
            this.lblGrubber = new System.Windows.Forms.Label();
            this.lblPunt = new System.Windows.Forms.Label();
            this.lblDrop = new System.Windows.Forms.Label();
            this.trkDrop = new System.Windows.Forms.TrackBar();
            this.trkPunt = new System.Windows.Forms.TrackBar();
            this.trkGrubber = new System.Windows.Forms.TrackBar();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpSquad.SuspendLayout();
            this.tabCategories.SuspendLayout();
            this.tabPassing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkPop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkStandard)).BeginInit();
            this.tabTackling.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkScrabble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFront)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSide)).BeginInit();
            this.tabKicking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkGoal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkDrop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkPunt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkGrubber)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSquad
            // 
            this.grpSquad.Controls.Add(this.radioSenior);
            this.grpSquad.Controls.Add(this.radioNonPlayer);
            this.grpSquad.Controls.Add(this.radioJunior);
            this.grpSquad.Location = new System.Drawing.Point(27, 15);
            this.grpSquad.Name = "grpSquad";
            this.grpSquad.Size = new System.Drawing.Size(121, 122);
            this.grpSquad.TabIndex = 19;
            this.grpSquad.TabStop = false;
            this.grpSquad.Text = "Select a Squad";
            // 
            // radioSenior
            // 
            this.radioSenior.AutoSize = true;
            this.radioSenior.Location = new System.Drawing.Point(6, 78);
            this.radioSenior.Name = "radioSenior";
            this.radioSenior.Size = new System.Drawing.Size(55, 17);
            this.radioSenior.TabIndex = 2;
            this.radioSenior.TabStop = true;
            this.radioSenior.Text = "Senior";
            this.radioSenior.UseVisualStyleBackColor = true;
            // 
            // radioNonPlayer
            // 
            this.radioNonPlayer.AutoSize = true;
            this.radioNonPlayer.Location = new System.Drawing.Point(6, 54);
            this.radioNonPlayer.Name = "radioNonPlayer";
            this.radioNonPlayer.Size = new System.Drawing.Size(77, 17);
            this.radioNonPlayer.TabIndex = 1;
            this.radioNonPlayer.TabStop = true;
            this.radioNonPlayer.Text = "Non-Player";
            this.radioNonPlayer.UseVisualStyleBackColor = true;
            // 
            // radioJunior
            // 
            this.radioJunior.AutoSize = true;
            this.radioJunior.Location = new System.Drawing.Point(7, 31);
            this.radioJunior.Name = "radioJunior";
            this.radioJunior.Size = new System.Drawing.Size(53, 17);
            this.radioJunior.TabIndex = 0;
            this.radioJunior.TabStop = true;
            this.radioJunior.Text = "Junior";
            this.radioJunior.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(27, 306);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(109, 34);
            this.btnUpdate.TabIndex = 18;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            // 
            // lblSquadName
            // 
            this.lblSquadName.AutoSize = true;
            this.lblSquadName.Location = new System.Drawing.Point(70, 272);
            this.lblSquadName.Name = "lblSquadName";
            this.lblSquadName.Size = new System.Drawing.Size(40, 13);
            this.lblSquadName.TabIndex = 17;
            this.lblSquadName.Text = "           ";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(70, 254);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(43, 13);
            this.lblName.TabIndex = 16;
            this.lblName.Text = "            ";
            // 
            // lblSquad
            // 
            this.lblSquad.AutoSize = true;
            this.lblSquad.Location = new System.Drawing.Point(25, 272);
            this.lblSquad.Name = "lblSquad";
            this.lblSquad.Size = new System.Drawing.Size(41, 13);
            this.lblSquad.TabIndex = 15;
            this.lblSquad.Text = "Squad:";
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.AutoSize = true;
            this.lblPlayerName.Location = new System.Drawing.Point(70, 254);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(32, 13);
            this.lblPlayerName.TabIndex = 14;
            this.lblPlayerName.Text = "yhtryt";
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Location = new System.Drawing.Point(25, 253);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(39, 13);
            this.lblPlayer.TabIndex = 13;
            this.lblPlayer.Text = "Player:";
            // 
            // cmbPlayers
            // 
            this.cmbPlayers.FormattingEnabled = true;
            this.cmbPlayers.Location = new System.Drawing.Point(27, 153);
            this.cmbPlayers.Name = "cmbPlayers";
            this.cmbPlayers.Size = new System.Drawing.Size(121, 21);
            this.cmbPlayers.TabIndex = 12;
            this.cmbPlayers.Text = "Players";
            // 
            // tabCategories
            // 
            this.tabCategories.Controls.Add(this.tabPassing);
            this.tabCategories.Controls.Add(this.tabTackling);
            this.tabCategories.Controls.Add(this.tabKicking);
            this.tabCategories.Location = new System.Drawing.Point(245, 96);
            this.tabCategories.Name = "tabCategories";
            this.tabCategories.SelectedIndex = 0;
            this.tabCategories.Size = new System.Drawing.Size(512, 262);
            this.tabCategories.TabIndex = 11;
            // 
            // tabPassing
            // 
            this.tabPassing.Controls.Add(this.lblStanVal);
            this.tabPassing.Controls.Add(this.trkPop);
            this.tabPassing.Controls.Add(this.trkSpin);
            this.tabPassing.Controls.Add(this.trkStandard);
            this.tabPassing.Controls.Add(this.lblPassingComments);
            this.tabPassing.Controls.Add(this.txtPassingComments);
            this.tabPassing.Controls.Add(this.lblPop);
            this.tabPassing.Controls.Add(this.lblSpin);
            this.tabPassing.Controls.Add(this.lblStandard);
            this.tabPassing.Location = new System.Drawing.Point(4, 22);
            this.tabPassing.Name = "tabPassing";
            this.tabPassing.Padding = new System.Windows.Forms.Padding(3);
            this.tabPassing.Size = new System.Drawing.Size(504, 236);
            this.tabPassing.TabIndex = 0;
            this.tabPassing.Text = "Passing";
            this.tabPassing.UseVisualStyleBackColor = true;
            // 
            // lblStanVal
            // 
            this.lblStanVal.AutoSize = true;
            this.lblStanVal.Location = new System.Drawing.Point(133, 38);
            this.lblStanVal.Name = "lblStanVal";
            this.lblStanVal.Size = new System.Drawing.Size(25, 13);
            this.lblStanVal.TabIndex = 8;
            this.lblStanVal.Text = "555";
            // 
            // trkPop
            // 
            this.trkPop.BackColor = System.Drawing.Color.White;
            this.trkPop.Location = new System.Drawing.Point(28, 177);
            this.trkPop.Maximum = 5;
            this.trkPop.Minimum = 1;
            this.trkPop.Name = "trkPop";
            this.trkPop.Size = new System.Drawing.Size(98, 45);
            this.trkPop.TabIndex = 7;
            this.trkPop.Value = 1;
            // 
            // trkSpin
            // 
            this.trkSpin.BackColor = System.Drawing.Color.White;
            this.trkSpin.Location = new System.Drawing.Point(28, 104);
            this.trkSpin.Maximum = 5;
            this.trkSpin.Minimum = 1;
            this.trkSpin.Name = "trkSpin";
            this.trkSpin.Size = new System.Drawing.Size(98, 45);
            this.trkSpin.TabIndex = 6;
            this.trkSpin.Value = 1;
            // 
            // trkStandard
            // 
            this.trkStandard.BackColor = System.Drawing.Color.White;
            this.trkStandard.Location = new System.Drawing.Point(28, 38);
            this.trkStandard.Maximum = 5;
            this.trkStandard.Minimum = 1;
            this.trkStandard.Name = "trkStandard";
            this.trkStandard.Size = new System.Drawing.Size(98, 45);
            this.trkStandard.TabIndex = 5;
            this.trkStandard.Value = 1;
            // 
            // lblPassingComments
            // 
            this.lblPassingComments.AutoSize = true;
            this.lblPassingComments.Location = new System.Drawing.Point(185, 6);
            this.lblPassingComments.Name = "lblPassingComments";
            this.lblPassingComments.Size = new System.Drawing.Size(56, 13);
            this.lblPassingComments.TabIndex = 4;
            this.lblPassingComments.Text = "Comments";
            // 
            // txtPassingComments
            // 
            this.txtPassingComments.Location = new System.Drawing.Point(188, 22);
            this.txtPassingComments.Multiline = true;
            this.txtPassingComments.Name = "txtPassingComments";
            this.txtPassingComments.Size = new System.Drawing.Size(320, 211);
            this.txtPassingComments.TabIndex = 3;
            // 
            // lblPop
            // 
            this.lblPop.AutoSize = true;
            this.lblPop.Location = new System.Drawing.Point(53, 161);
            this.lblPop.Name = "lblPop";
            this.lblPop.Size = new System.Drawing.Size(26, 13);
            this.lblPop.TabIndex = 2;
            this.lblPop.Text = "Pop";
            // 
            // lblSpin
            // 
            this.lblSpin.AutoSize = true;
            this.lblSpin.Location = new System.Drawing.Point(53, 88);
            this.lblSpin.Name = "lblSpin";
            this.lblSpin.Size = new System.Drawing.Size(28, 13);
            this.lblSpin.TabIndex = 1;
            this.lblSpin.Text = "Spin";
            // 
            // lblStandard
            // 
            this.lblStandard.AutoSize = true;
            this.lblStandard.Location = new System.Drawing.Point(53, 22);
            this.lblStandard.Name = "lblStandard";
            this.lblStandard.Size = new System.Drawing.Size(50, 13);
            this.lblStandard.TabIndex = 0;
            this.lblStandard.Text = "Standard";
            // 
            // tabTackling
            // 
            this.tabTackling.Controls.Add(this.trkScrabble);
            this.tabTackling.Controls.Add(this.lblRear);
            this.tabTackling.Controls.Add(this.trkFront);
            this.tabTackling.Controls.Add(this.lblTacklingComments);
            this.tabTackling.Controls.Add(this.txtTacklingComments);
            this.tabTackling.Controls.Add(this.lblScrabble);
            this.tabTackling.Controls.Add(this.lblSide);
            this.tabTackling.Controls.Add(this.lblFront);
            this.tabTackling.Controls.Add(this.trkRear);
            this.tabTackling.Controls.Add(this.trkSide);
            this.tabTackling.Location = new System.Drawing.Point(4, 22);
            this.tabTackling.Name = "tabTackling";
            this.tabTackling.Padding = new System.Windows.Forms.Padding(3);
            this.tabTackling.Size = new System.Drawing.Size(504, 236);
            this.tabTackling.TabIndex = 1;
            this.tabTackling.Text = "Tackling";
            this.tabTackling.UseVisualStyleBackColor = true;
            // 
            // trkScrabble
            // 
            this.trkScrabble.BackColor = System.Drawing.Color.White;
            this.trkScrabble.Location = new System.Drawing.Point(35, 188);
            this.trkScrabble.Maximum = 5;
            this.trkScrabble.Minimum = 1;
            this.trkScrabble.Name = "trkScrabble";
            this.trkScrabble.Size = new System.Drawing.Size(104, 45);
            this.trkScrabble.TabIndex = 10;
            this.trkScrabble.Value = 1;
            // 
            // lblRear
            // 
            this.lblRear.AutoSize = true;
            this.lblRear.Location = new System.Drawing.Point(68, 54);
            this.lblRear.Name = "lblRear";
            this.lblRear.Size = new System.Drawing.Size(30, 13);
            this.lblRear.TabIndex = 1;
            this.lblRear.Text = "Rear";
            // 
            // trkFront
            // 
            this.trkFront.BackColor = System.Drawing.Color.White;
            this.trkFront.Location = new System.Drawing.Point(35, 22);
            this.trkFront.Maximum = 5;
            this.trkFront.Minimum = 1;
            this.trkFront.Name = "trkFront";
            this.trkFront.Size = new System.Drawing.Size(104, 45);
            this.trkFront.TabIndex = 7;
            this.trkFront.Value = 1;
            // 
            // lblTacklingComments
            // 
            this.lblTacklingComments.AutoSize = true;
            this.lblTacklingComments.Location = new System.Drawing.Point(185, 6);
            this.lblTacklingComments.Name = "lblTacklingComments";
            this.lblTacklingComments.Size = new System.Drawing.Size(56, 13);
            this.lblTacklingComments.TabIndex = 6;
            this.lblTacklingComments.Text = "Comments";
            // 
            // txtTacklingComments
            // 
            this.txtTacklingComments.Location = new System.Drawing.Point(188, 22);
            this.txtTacklingComments.Multiline = true;
            this.txtTacklingComments.Name = "txtTacklingComments";
            this.txtTacklingComments.Size = new System.Drawing.Size(316, 211);
            this.txtTacklingComments.TabIndex = 5;
            // 
            // lblScrabble
            // 
            this.lblScrabble.AutoSize = true;
            this.lblScrabble.Location = new System.Drawing.Point(68, 167);
            this.lblScrabble.Name = "lblScrabble";
            this.lblScrabble.Size = new System.Drawing.Size(49, 13);
            this.lblScrabble.TabIndex = 3;
            this.lblScrabble.Text = "Scrabble";
            // 
            // lblSide
            // 
            this.lblSide.AutoSize = true;
            this.lblSide.Location = new System.Drawing.Point(68, 116);
            this.lblSide.Name = "lblSide";
            this.lblSide.Size = new System.Drawing.Size(28, 13);
            this.lblSide.TabIndex = 2;
            this.lblSide.Text = "Side";
            // 
            // lblFront
            // 
            this.lblFront.AutoSize = true;
            this.lblFront.Location = new System.Drawing.Point(68, 6);
            this.lblFront.Name = "lblFront";
            this.lblFront.Size = new System.Drawing.Size(31, 13);
            this.lblFront.TabIndex = 0;
            this.lblFront.Text = "Front";
            // 
            // trkRear
            // 
            this.trkRear.BackColor = System.Drawing.Color.White;
            this.trkRear.Location = new System.Drawing.Point(35, 73);
            this.trkRear.Maximum = 5;
            this.trkRear.Minimum = 1;
            this.trkRear.Name = "trkRear";
            this.trkRear.Size = new System.Drawing.Size(104, 45);
            this.trkRear.TabIndex = 8;
            this.trkRear.Value = 1;
            // 
            // trkSide
            // 
            this.trkSide.BackColor = System.Drawing.Color.White;
            this.trkSide.Location = new System.Drawing.Point(35, 135);
            this.trkSide.Maximum = 5;
            this.trkSide.Minimum = 1;
            this.trkSide.Name = "trkSide";
            this.trkSide.Size = new System.Drawing.Size(104, 45);
            this.trkSide.TabIndex = 9;
            this.trkSide.Value = 1;
            // 
            // tabKicking
            // 
            this.tabKicking.Controls.Add(this.trkGoal);
            this.tabKicking.Controls.Add(this.lblKickingComments);
            this.tabKicking.Controls.Add(this.txtKickingComments);
            this.tabKicking.Controls.Add(this.lblGoal);
            this.tabKicking.Controls.Add(this.lblGrubber);
            this.tabKicking.Controls.Add(this.lblPunt);
            this.tabKicking.Controls.Add(this.lblDrop);
            this.tabKicking.Controls.Add(this.trkDrop);
            this.tabKicking.Controls.Add(this.trkPunt);
            this.tabKicking.Controls.Add(this.trkGrubber);
            this.tabKicking.Location = new System.Drawing.Point(4, 22);
            this.tabKicking.Name = "tabKicking";
            this.tabKicking.Padding = new System.Windows.Forms.Padding(3);
            this.tabKicking.Size = new System.Drawing.Size(504, 236);
            this.tabKicking.TabIndex = 2;
            this.tabKicking.Text = "Kicking";
            this.tabKicking.UseVisualStyleBackColor = true;
            // 
            // trkGoal
            // 
            this.trkGoal.BackColor = System.Drawing.Color.White;
            this.trkGoal.Location = new System.Drawing.Point(35, 190);
            this.trkGoal.Maximum = 5;
            this.trkGoal.Minimum = 1;
            this.trkGoal.Name = "trkGoal";
            this.trkGoal.Size = new System.Drawing.Size(104, 45);
            this.trkGoal.TabIndex = 10;
            this.trkGoal.Value = 1;
            // 
            // lblKickingComments
            // 
            this.lblKickingComments.AutoSize = true;
            this.lblKickingComments.Location = new System.Drawing.Point(185, 6);
            this.lblKickingComments.Name = "lblKickingComments";
            this.lblKickingComments.Size = new System.Drawing.Size(56, 13);
            this.lblKickingComments.TabIndex = 6;
            this.lblKickingComments.Text = "Comments";
            // 
            // txtKickingComments
            // 
            this.txtKickingComments.Location = new System.Drawing.Point(188, 22);
            this.txtKickingComments.Multiline = true;
            this.txtKickingComments.Name = "txtKickingComments";
            this.txtKickingComments.Size = new System.Drawing.Size(316, 211);
            this.txtKickingComments.TabIndex = 5;
            // 
            // lblGoal
            // 
            this.lblGoal.AutoSize = true;
            this.lblGoal.Location = new System.Drawing.Point(74, 171);
            this.lblGoal.Name = "lblGoal";
            this.lblGoal.Size = new System.Drawing.Size(29, 13);
            this.lblGoal.TabIndex = 3;
            this.lblGoal.Text = "Goal";
            // 
            // lblGrubber
            // 
            this.lblGrubber.AutoSize = true;
            this.lblGrubber.Location = new System.Drawing.Point(73, 121);
            this.lblGrubber.Name = "lblGrubber";
            this.lblGrubber.Size = new System.Drawing.Size(45, 13);
            this.lblGrubber.TabIndex = 2;
            this.lblGrubber.Text = "Grubber";
            // 
            // lblPunt
            // 
            this.lblPunt.AutoSize = true;
            this.lblPunt.Location = new System.Drawing.Point(73, 54);
            this.lblPunt.Name = "lblPunt";
            this.lblPunt.Size = new System.Drawing.Size(29, 13);
            this.lblPunt.TabIndex = 1;
            this.lblPunt.Text = "Punt";
            // 
            // lblDrop
            // 
            this.lblDrop.AutoSize = true;
            this.lblDrop.Location = new System.Drawing.Point(73, 6);
            this.lblDrop.Name = "lblDrop";
            this.lblDrop.Size = new System.Drawing.Size(30, 13);
            this.lblDrop.TabIndex = 0;
            this.lblDrop.Text = "Drop";
            // 
            // trkDrop
            // 
            this.trkDrop.BackColor = System.Drawing.Color.White;
            this.trkDrop.Location = new System.Drawing.Point(35, 22);
            this.trkDrop.Maximum = 5;
            this.trkDrop.Minimum = 1;
            this.trkDrop.Name = "trkDrop";
            this.trkDrop.Size = new System.Drawing.Size(104, 45);
            this.trkDrop.TabIndex = 7;
            this.trkDrop.Value = 1;
            // 
            // trkPunt
            // 
            this.trkPunt.BackColor = System.Drawing.Color.White;
            this.trkPunt.Location = new System.Drawing.Point(35, 73);
            this.trkPunt.Maximum = 5;
            this.trkPunt.Minimum = 1;
            this.trkPunt.Name = "trkPunt";
            this.trkPunt.Size = new System.Drawing.Size(104, 45);
            this.trkPunt.TabIndex = 8;
            this.trkPunt.Value = 1;
            // 
            // trkGrubber
            // 
            this.trkGrubber.BackColor = System.Drawing.Color.White;
            this.trkGrubber.Location = new System.Drawing.Point(35, 139);
            this.trkGrubber.Maximum = 5;
            this.trkGrubber.Minimum = 1;
            this.trkGrubber.Name = "trkGrubber";
            this.trkGrubber.Size = new System.Drawing.Size(104, 45);
            this.trkGrubber.TabIndex = 9;
            this.trkGrubber.Value = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(277, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(212, 37);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Simply Rugby";
            // 
            // FormPlayers
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.ClientSize = new System.Drawing.Size(785, 394);
            this.ControlBox = false;
            this.Controls.Add(this.grpSquad);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.lblSquadName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblSquad);
            this.Controls.Add(this.lblPlayerName);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.cmbPlayers);
            this.Controls.Add(this.tabCategories);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPlayers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Players";
            this.grpSquad.ResumeLayout(false);
            this.grpSquad.PerformLayout();
            this.tabCategories.ResumeLayout(false);
            this.tabPassing.ResumeLayout(false);
            this.tabPassing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkPop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSpin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkStandard)).EndInit();
            this.tabTackling.ResumeLayout(false);
            this.tabTackling.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkScrabble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkFront)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkRear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkSide)).EndInit();
            this.tabKicking.ResumeLayout(false);
            this.tabKicking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trkGoal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkDrop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkPunt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkGrubber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
	}
}
