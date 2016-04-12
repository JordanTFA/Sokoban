// Jordan Aitken
// HND Software Development
// Sokoban Project

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace JordanSokoban
{
	public class Board : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;

        // Objects
        private PlayerData playerData;
        private LevelSet levelSet;
        private Level level;

        // Graphics
		private PictureBox screen;
		private Image img;

        // Information Display
        private System.Windows.Forms.Label lblMvs;
        private System.Windows.Forms.Label lblMoves;
        private System.Windows.Forms.Label lblPushes;
        private System.Windows.Forms.GroupBox grpMoves;
        private System.Windows.Forms.Label lblPlayerName;
        private System.Windows.Forms.Label lblLevelNr;
        private System.Windows.Forms.Label lblPshs;


        // Constructor
		public Board()
		{
			InitializeComponent();
			screen = new PictureBox();
			Controls.AddRange(new Control[] {screen});
            
            InitializeGame();
        }
        
        
        // Initial Menu Screen
        private void InitializeGame()
        {
            levelSet = new LevelSet();
            
            // List of all current players
			FormPlayers formPlayers = new FormPlayers();
			formPlayers.ShowDialog();
			playerData = new PlayerData(formPlayers.PlayerName);
			
            // If user continues their existing game
            // This will set the level to the most recently played
			if (formPlayers.ContinueExistingGame)
			{
                // Finds the last completed level (lastFinished)
			    playerData.LoadLastGameInfo();
			    levelSet.SetLevelSet(playerData.LastPlayedSet);
                
                // Sets current level to lastFinished + 1
			    levelSet.CurrentLevel = playerData.LastFinishedLevel + 1;
			    if (levelSet.CurrentLevel > levelSet.NrOfLevelsInSet)
			        levelSet.CurrentLevel = levelSet.NrOfLevelsInSet;

			    levelSet.LastFinishedLevel = playerData.LastFinishedLevel;
			}
            // If user starts a new game
			else
			{
			    // Pick a level set to play
                FormLevels formLevels = new FormLevels();
                formLevels.ShowDialog();
                levelSet.SetLevelSet(formLevels.FilenameLevelSet);
                levelSet.CurrentLevel = 1;
                
                // Creates a new save game if a new player has been created
                if (formPlayers.NewPlayer)
                    playerData.CreatePlayer(levelSet);
                else
                {
			        playerData.LoadPlayerInfo(levelSet);
		    	    playerData.SaveLevelSet(levelSet);
		    	}
		    }

            lblPlayerName.Text = playerData.Name;
            
			levelSet.SetLevelsInLevelSet(levelSet.Filename);
			level = (Level)levelSet[levelSet.CurrentLevel - 1];
			
			// Draws the level
            DrawLevel();
		}
		
		
        // Drawing the level
		private void DrawLevel()
		{
            // Sets height and width of the level
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

		// Draws changes, rather than redrawing the entire level
        private void DrawChanges()
        {
            img = level.DrawChanges();
            screen.Image = img;
            
            // Updates the label with number of pushes and moves
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
        
		
        // Reads user controls 
		private void AKeyDown(object sender, KeyEventArgs e)
		{
		    string result = e.KeyData.ToString();
		    
		    switch (result)
		    {
		        case "Up": // If Up-arrow is pressed, will move the character up one tile
		            MoveSokoban(MoveDirection.Up);
		            break;
                case "Down": // If Down-arrow is pressed, will move the character down one tile
                    MoveSokoban(MoveDirection.Down);
                    break;
                case "Right": // If Right-arrow is pressed, will move the character right one tile
                    MoveSokoban(MoveDirection.Right);
                    break;
                case "Left": // If Left-arrow is pressed, will move the character left one tile
                    MoveSokoban(MoveDirection.Left);
                    break;
                case "U": //If "U" is pressed, will undo the last crate move
                    DrawUndo();
                    break;
		    }
		}
		
		
        // Applies action from user's input if it is valid
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
            
            // Draws changes
            DrawChanges();

            // Displays a message when a level is complete and loads the next level
		    if (level.IsFinished())
		    {
		        levelSet.LastFinishedLevel = levelSet.CurrentLevel;
	            playerData.SaveLevel(level);
		        
		        if (levelSet.CurrentLevel < levelSet.NrOfLevelsInSet)
		        {
		            MessageBox.Show("Level Complete");
		            levelSet.CurrentLevel++;
		            level = (Level)levelSet[levelSet.CurrentLevel - 1];
		            DrawLevel();
		        }
		        else
		        {
		            MessageBox.Show("You beat the final level");
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Board));
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
            this.lblMvs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMvs.ForeColor = System.Drawing.Color.White;
            this.lblMvs.Location = new System.Drawing.Point(16, 24);
            this.lblMvs.Name = "lblMvs";
            this.lblMvs.Size = new System.Drawing.Size(52, 16);
            this.lblMvs.TabIndex = 0;
            this.lblMvs.Text = "Moves:";
            // 
            // lblPshs
            // 
            this.lblPshs.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPshs.ForeColor = System.Drawing.Color.White;
            this.lblPshs.Location = new System.Drawing.Point(16, 40);
            this.lblPshs.Name = "lblPshs";
            this.lblPshs.Size = new System.Drawing.Size(52, 16);
            this.lblPshs.TabIndex = 1;
            this.lblPshs.Text = "Pushes:";
            // 
            // lblMoves
            // 
            this.lblMoves.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoves.ForeColor = System.Drawing.Color.Orange;
            this.lblMoves.Location = new System.Drawing.Point(72, 24);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new System.Drawing.Size(44, 16);
            this.lblMoves.TabIndex = 2;
            // 
            // lblPushes
            // 
            this.lblPushes.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.lblPlayerName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerName.ForeColor = System.Drawing.Color.White;
            this.lblPlayerName.Location = new System.Drawing.Point(80, 16);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new System.Drawing.Size(150, 24);
            this.lblPlayerName.TabIndex = 4;
            this.lblPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLevelNr
            // 
            this.lblLevelNr.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.BackColor = System.Drawing.Color.Purple;
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
