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
	/// This form lets the using pick a level set he wants to play
	/// </summary>
	public class FormLevels : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
        private System.Windows.Forms.ListBox lstLevelSets;
        private System.Windows.Forms.Label lblAuthorH;
        private System.Windows.Forms.Label lblUrlH;
        private System.Windows.Forms.Label lblEmailH;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label lblDescriptionH;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblNrOfLevels;
        private System.Windows.Forms.Label lblNrOfLevelsH;
		
		private ArrayList levelSets = new ArrayList();
		private string filenameLevelSet = string.Empty;
		private string nameLevelSet = string.Empty;


        /// <summary>
        /// Constructor
        /// </summary>
		public FormLevels()
		{
			InitializeComponent();
			
			// Loads the information of all level sets
            levelSets = LevelSet.GetAllLevelSetInfos();
            
            // Adds the title of each level set to the listbox
            foreach (LevelSet levelSet in levelSets)
                lstLevelSets.Items.Add(levelSet.Title);
            
            lstLevelSets.SelectedIndex = 0;
		}
		
		
		/// <summary>
		/// Update the level set information in the labels when the user
		/// selects a different level set.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void lstLevelSets_SelectedIndexChanged(object sender,
            System.EventArgs e)
        {
            int index = lstLevelSets.SelectedIndex;
            LevelSet levelSet = (LevelSet)levelSets[index];
            
            lblAuthor.Text = levelSet.Author;
            lblEmail.Text = levelSet.Email;
            lblUrl.Text = levelSet.Url;
            lblDescription.Text = levelSet.Description;
            lblNrOfLevels.Text = levelSet.NrOfLevelsInSet.ToString();
        }


        /// <summary>
        /// Set the name and filename of the level set
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            nameLevelSet = lstLevelSets.SelectedItem.ToString();
            
            foreach (LevelSet levelSet in levelSets)
            {
                if (levelSet.Title == nameLevelSet)
                {
                    filenameLevelSet = levelSet.Filename;
                    break;
                }
            }
            
            this.Close();
        }

        
        // Properties        
        
        public string FilenameLevelSet
        {
            get { return filenameLevelSet; }
        }


		#region Windows Form Designer generated code
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lstLevelSets = new System.Windows.Forms.ListBox();
            this.lblAuthorH = new System.Windows.Forms.Label();
            this.lblDescriptionH = new System.Windows.Forms.Label();
            this.lblUrlH = new System.Windows.Forms.Label();
            this.lblEmailH = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lblNrOfLevels = new System.Windows.Forms.Label();
            this.lblNrOfLevelsH = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstLevelSets
            // 
            this.lstLevelSets.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lstLevelSets.Location = new System.Drawing.Point(24, 24);
            this.lstLevelSets.Name = "lstLevelSets";
            this.lstLevelSets.Size = new System.Drawing.Size(176, 199);
            this.lstLevelSets.TabIndex = 0;
            this.lstLevelSets.SelectedIndexChanged += new System.EventHandler(this.lstLevelSets_SelectedIndexChanged);
            // 
            // lblAuthorH
            // 
            this.lblAuthorH.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblAuthorH.Location = new System.Drawing.Point(224, 32);
            this.lblAuthorH.Name = "lblAuthorH";
            this.lblAuthorH.Size = new System.Drawing.Size(80, 16);
            this.lblAuthorH.TabIndex = 1;
            this.lblAuthorH.Text = "Author:";
            // 
            // lblDescriptionH
            // 
            this.lblDescriptionH.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblDescriptionH.Location = new System.Drawing.Point(224, 112);
            this.lblDescriptionH.Name = "lblDescriptionH";
            this.lblDescriptionH.Size = new System.Drawing.Size(104, 16);
            this.lblDescriptionH.TabIndex = 3;
            this.lblDescriptionH.Text = "Description:";
            // 
            // lblUrlH
            // 
            this.lblUrlH.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblUrlH.Location = new System.Drawing.Point(224, 64);
            this.lblUrlH.Name = "lblUrlH";
            this.lblUrlH.Size = new System.Drawing.Size(80, 16);
            this.lblUrlH.TabIndex = 4;
            this.lblUrlH.Text = "URL:";
            // 
            // lblEmailH
            // 
            this.lblEmailH.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblEmailH.Location = new System.Drawing.Point(224, 48);
            this.lblEmailH.Name = "lblEmailH";
            this.lblEmailH.Size = new System.Drawing.Size(80, 16);
            this.lblEmailH.TabIndex = 5;
            this.lblEmailH.Text = "Email:";
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new System.Drawing.Point(304, 32);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(168, 16);
            this.lblAuthor.TabIndex = 6;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(304, 48);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(168, 16);
            this.lblEmail.TabIndex = 7;
            // 
            // lblUrl
            // 
            this.lblUrl.Location = new System.Drawing.Point(304, 64);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(168, 16);
            this.lblUrl.TabIndex = 10;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(224, 136);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(248, 88);
            this.lblDescription.TabIndex = 11;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(392, 240);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.TabIndex = 12;
            this.btnSelect.Text = "Start game";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblNrOfLevels
            // 
            this.lblNrOfLevels.Location = new System.Drawing.Point(304, 80);
            this.lblNrOfLevels.Name = "lblNrOfLevels";
            this.lblNrOfLevels.Size = new System.Drawing.Size(168, 16);
            this.lblNrOfLevels.TabIndex = 14;
            // 
            // lblNrOfLevelsH
            // 
            this.lblNrOfLevelsH.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.lblNrOfLevelsH.Location = new System.Drawing.Point(224, 80);
            this.lblNrOfLevelsH.Name = "lblNrOfLevelsH";
            this.lblNrOfLevelsH.Size = new System.Drawing.Size(80, 16);
            this.lblNrOfLevelsH.TabIndex = 13;
            this.lblNrOfLevelsH.Text = "Level count:";
            // 
            // FormLevels
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(498, 277);
            this.ControlBox = false;
            this.Controls.Add(this.lblNrOfLevels);
            this.Controls.Add(this.lblNrOfLevelsH);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblUrl);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblEmailH);
            this.Controls.Add(this.lblUrlH);
            this.Controls.Add(this.lblDescriptionH);
            this.Controls.Add(this.lblAuthorH);
            this.Controls.Add(this.lstLevelSets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormLevels";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormLevels";
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
