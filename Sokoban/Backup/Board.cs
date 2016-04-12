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
using System.Data;

namespace SokobanPro
{
	/// <summary>
	/// This class draws everything on screen. It also handles user input.
	/// </summary>
	public class Board : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;

        // Initialize the required objects
        private PlayerData playerData;
        private LevelSet levelSet;
        private Level level;

        // Objects for drawing graphics on screen
		private PictureBox screen;
		private Image img;

        // Form controls to display information on the screen
        private System.Windows.Forms.Label lblMvs;
        private System.Windows.Forms.Label lblMoves;
        private System.Windows.Forms.Label lblPushes;
        private System.Windows.Forms.GroupBox grpMoves;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblLevelNr;
        private System.Windows.Forms.Label lblPshs;


        /// <summary>
        /// Constructor
        /// </summary>
		public Board()
		{
			InitializeComponent();
			screen = new PictureBox();
			Controls.AddRange(new Control[] {screen});
            
            InitializeGame();
        }
        
        
        /// <summary>
        /// Sets the data for PlayerData, LevelSet, etc..
        /// </summary>
        private void InitializeGame()
        {
            levelSet = new LevelSet();
            
            // Display the form to pick a player
			FormPlayers formPlayers = new FormPlayers();
			formPlayers.ShowDialog();
			playerData = new PlayerData(formPlayers.PlayerName);
			
			// If we continue an existing game, we load the level set and the
			// last finished level number from that set. If we start a new game
			// we let the player choose a level set
			if (formPlayers.ContinueExistingGame)
			{
			    playerData.LoadLastGameInfo();
			    levelSet.SetLevelSet(playerData.LastPlayedSet);
                
                // The level we want to play is the one after the last finished
                // If we've completed the set already, we play the last level.
			    levelSet.CurrentLevel = playerData.LastFinishedLevel + 1;
			    if (levelSet.CurrentLevel > levelSet.NrOfLevelsInSet)
			        levelSet.CurrentLevel = levelSet.NrOfLevelsInSet;

			    levelSet.LastFinishedLevel = playerData.LastFinishedLevel;
			}
			else
			{
			    // Pick a level set to play
                FormLevels formLevels = new FormLevels();
                formLevels.ShowDialog();
                levelSet.SetLevelSet(formLevels.FilenameLevelSet);
                levelSet.CurrentLevel = 1;
                
                // If we've created a new player, we create a new savegame. If
                // we pick an existing player, we load the info from the save
                // game and add the levelset to the savegame if it don't exist.
                if (formPlayers.NewPlayer)
                    playerData.CreatePlayer(levelSet);
                else
                {
			        playerData.LoadPlayerInfo(levelSet);
		    	    playerData.SaveLevelSet(levelSet);
		    	}
		    }

            lblPlayerName.Text = playerData.Name;
            
			// Load the levels in the LevelSet object and set the current level
			levelSet.SetLevelsInLevelSet(levelSet.Filename);
			level = (Level)levelSet[levelSet.CurrentLevel - 1];
			
			// Draw the level on the screen
            DrawLevel();
		}
		
		
		/// <summary>
		/// This method sets the width and the height of the screen,
		/// according to the level width and height. It then lets the level
		/// itself. Lastly, we set the labels to display the player/level info.
		/// </summary>
		private void DrawLevel()
		{
		    int levelWidth = (level.Width + 2) * Level.ITEM_SIZE;
		    int levelHeight = (level.Height + 2) * Level.ITEM_SIZE;
            
            this.ClientSize = new Size(levelWidth + 150, levelHeight);
            screen.Size = new System.Drawing.Size(levelWidth, levelHeight);

            img = level.DrawLevel();
			screen.Image = img;

			lblPlayerName.Location = new Point(levelWidth, 25);
			lblLevelNr.Location = new Point(levelWidth, 65);
			
			grpMoves.Location = new Point(levelWidth + 15, 90);
            lblMvs.Location = new Point(15, 20);
            lblPshs.Location = new Point(15, 36);
            lblMoves.Location = new Point(70, 20);
            lblPushes.Location = new Point(70, 36);
            
            lblMoves.Text = "0";
            lblPushes.Text = "0";
            lblLevelNr.Text = "Level: " + level.LevelNr;
		}

		
		/// <summary>
		/// After moving Sokoban we only draw the changes on the level, and not
		/// redraw the whole level
		/// </summary>
        private void DrawChanges()
        {
            img = level.DrawChanges();
            screen.Image = img;
            
            // Update labels with number of moves and pushes
            lblMoves.Text = level.Moves.ToString();
            lblPushes.Text = level.Pushes.ToString();
        }
        
        
        
        private void DrawUndo()
        {
            if (level.IsUndoable)
            {
                img = level.Undo();
                screen.Image = img;
            
                // Update labels with number of moves and pushes
                lblMoves.Text = level.Moves.ToString();
                lblPushes.Text = level.Pushes.ToString();
            }
        }
        
		
		/// <summary>
		/// Reads input from the keyboard and does something depending on what
		/// key is pressed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AKeyDown(object sender, KeyEventArgs e)
		{
		    string result = e.KeyData.ToString();
		    
		    switch (result)
		    {
		        case "Up":
		            MoveSokoban(MoveDirection.Up);
		            break;
                case "Down":
                    MoveSokoban(MoveDirection.Down);
                    break;
                case "Right":
                    MoveSokoban(MoveDirection.Right);
                    break;
                case "Left":
                    MoveSokoban(MoveDirection.Left);
                    break;
                case "U":
                    DrawUndo();
                    break;
		    }
		}
		
		
		/// <summary>
		/// Handles all the actions that must be performed after we decide to
		/// move Sokoban in a particular direction: We call the level.Move()
		/// method in Level to check if Sokoban can move in the direction we
		/// him to. Then we update the screen to reflect the movement changes.
		/// Then we check if the level is solved. We also check if this was the
		/// last level in the levelset.
		/// </summary>
		/// <param name="direction">Move direction</param>
		private void MoveSokoban(MoveDirection direction)
		{
		    if (direction == MoveDirection.Up)
		        level.MoveSokoban(MoveDirection.Up);
            else if (direction == MoveDirection.Down)
                level.MoveSokoban(MoveDirection.Down);
            else if (direction == MoveDirection.Right)
                level.MoveSokoban(MoveDirection.Right);
            else if (direction == MoveDirection.Left)
                level.MoveSokoban(MoveDirection.Left);
            
            // Draw the changes of the level
            DrawChanges();

            // If the level is finished we save the number of moves and pushes
            // and the last finished level to the savegame. If we haven't came
            // to the last level, we'll just play the next. Otherwise, ...
		    if (level.IsFinished())
		    {
		        levelSet.LastFinishedLevel = levelSet.CurrentLevel;
	            playerData.SaveLevel(level);
		        
		        if (levelSet.CurrentLevel < levelSet.NrOfLevelsInSet)
		        {
		            MessageBox.Show("Well done!!");
		            levelSet.CurrentLevel++;
		            level = (Level)levelSet[levelSet.CurrentLevel - 1];
		            DrawLevel();
		        }
		        else
		        {
		            MessageBox.Show("That was the last level!");
		            this.Close();
		        }
		    }
		}
		
		#region Windows Form Designer generated code
		
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new Board());
        }
        
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Board));
            this.lblMvs = new System.Windows.Forms.Label();
            this.lblPshs = new System.Windows.Forms.Label();
            this.lblMoves = new System.Windows.Forms.Label();
            this.lblPushes = new System.Windows.Forms.Label();
            this.grpMoves = new System.Windows.Forms.GroupBox();
            this.lblPlayerName = new System.Windows.Forms.Label();
            this.lblLevelNr = new System.Windows.Forms.Label();
            this.grpMoves.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMvs
            // 
            this.lblMvs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblMvs.ForeColor = System.Drawing.Color.White;
            this.lblMvs.Location = new System.Drawing.Point(16, 24);
            this.lblMvs.Name = "lblMvs";
            this.lblMvs.Size = new System.Drawing.Size(52, 16);
            this.lblMvs.TabIndex = 0;
            this.lblMvs.Text = "Moves:";
            // 
            // lblPshs
            // 
            this.lblPshs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblPshs.ForeColor = System.Drawing.Color.White;
            this.lblPshs.Location = new System.Drawing.Point(16, 40);
            this.lblPshs.Name = "lblPshs";
            this.lblPshs.Size = new System.Drawing.Size(52, 16);
            this.lblPshs.TabIndex = 1;
            this.lblPshs.Text = "Pushes:";
            // 
            // lblMoves
            // 
            this.lblMoves.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblMoves.ForeColor = System.Drawing.Color.Orange;
            this.lblMoves.Location = new System.Drawing.Point(72, 24);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(44, 16);
            this.lblMoves.TabIndex = 2;
            // 
            // lblPushes
            // 
            this.lblPushes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblPushes.ForeColor = System.Drawing.Color.Orange;
            this.lblPushes.Location = new System.Drawing.Point(64, 40);
            this.lblPushes.Name = "lblPushes";
            this.lblPushes.Size = new System.Drawing.Size(44, 16);
            this.lblPushes.TabIndex = 3;
            // 
            // grpMoves
            // 
            this.grpMoves.Controls.Add(this.lblPshs);
            this.grpMoves.Controls.Add(this.lblMvs);
            this.grpMoves.Controls.Add(this.lblMoves);
            this.grpMoves.Controls.Add(this.lblPushes);
            this.grpMoves.Location = new System.Drawing.Point(40, 56);
            this.grpMoves.Name = "grpMoves";
            this.grpMoves.Size = new System.Drawing.Size(120, 64);
            this.grpMoves.TabIndex = 4;
            this.grpMoves.TabStop = false;
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblPlayerName.Location = new System.Drawing.Point(80, 16);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(150, 24);
            this.lblPlayerName.TabIndex = 4;
            this.lblPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLevelNr
            // 
            this.lblLevelNr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblLevelNr.ForeColor = System.Drawing.Color.White;
            this.lblLevelNr.Location = new System.Drawing.Point(168, 48);
            this.lblLevelNr.Name = "lblLevelNr";
            this.lblLevelNr.Size = new System.Drawing.Size(150, 16);
            this.lblLevelNr.TabIndex = 4;
            this.lblLevelNr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Board
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(84)), ((System.Byte)(48)), ((System.Byte)(12)));
            this.ClientSize = new System.Drawing.Size(446, 200);
            this.Controls.Add(this.grpMoves);
            this.Controls.Add(this.lblPlayerName);
            this.Controls.Add(this.lblLevelNr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Board";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sokoban Pro v0.5";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AKeyDown);
            this.grpMoves.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }
        
		#endregion
	}
}
