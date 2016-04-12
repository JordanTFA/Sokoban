// Jordan Aitken
// HND Software Development
// Sokoban Project

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;

namespace JordanSokoban
{
    // Contains Information in .xml 
    public class LevelSet
    {
        // Collection of the levels in a .XML level set
        private ArrayList levels = new ArrayList();
        
        private string title = string.Empty;
        private string description = string.Empty;
        private string email = string.Empty;
        private string url = string.Empty;
        private string author = string.Empty;
        private string filename = string.Empty;
        
        private int currentLevel = 0;
        private int nrOfLevelsInSet = 0;
        private int lastFinishedLevel = 0;
		
		#region Properties
		
        public string Title
        {
            get { return title; }
        }
        
        public string Description
        {
            get { return description; }
        }
        
        public string Email
        {
            get { return email; }
        }
        
        public string Url
        {
            get { return url; }
        }
        
        public string Author
        {
            get { return author; }
        }

        public string Filename
        {
            get { return filename; }
        }
        
        public int NrOfLevelsInSet
        {
            get { return nrOfLevelsInSet; }
        }
        
        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }
        
        public int LastFinishedLevel
        {
            set { lastFinishedLevel = value; }
        }
        
        #endregion
		
		
        // Constructor
        public LevelSet(string aTitle, string aDescription, string aEmail,
            string aUrl, string aAuthor, int aNrOfLevels, string aFilename)
        {
            title = aTitle;
            description = aDescription;
            email = aEmail;
            url = aUrl;
            author = aAuthor;
            nrOfLevelsInSet = aNrOfLevels;
            filename = aFilename;
        }
        
        

        // Constructor

        public LevelSet() {}
        
        
		// Indexer for the LevelSet object

		public Level this[int index]
		{
		    get { return (Level)levels[index]; }
		}

        
        // Sets the information
        public void SetLevelSet(string setName)
        {		    
		    // Load XML into memory
            XmlDocument doc = new XmlDocument();
            doc.Load(setName);
		    
		    filename = setName;
		    title = doc.SelectSingleNode("//Title").InnerText;
		    description = doc.SelectSingleNode("//Description").InnerText;
		    email = doc.SelectSingleNode("//Email").InnerText;
		    url = doc.SelectSingleNode("//Url").InnerText;

		    XmlNode levelCollection =doc.SelectSingleNode("//LevelCollection");
		    author = levelCollection.Attributes["Copyright"].Value;
		    XmlNodeList levels = doc.SelectNodes("//Level");
		    nrOfLevelsInSet = levels.Count;
        }
        
        
        // Reads all the Level elements from the level set
        public void SetLevelsInLevelSet(string setName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(setName);
            
            // Get all Level elements from the level set
            XmlNodeList levelInfoList = doc.SelectNodes("//Level");
            
            int levelNr = 1;
            foreach (XmlNode levelInfo in levelInfoList)
            {
                LoadLevel(levelInfo, levelNr);
                levelNr++;
            }
        }
        
        
        // Reads the level information from a particular Level node
        private void LoadLevel(XmlNode levelInfo, int levelNr)
        {
            // Read the attributes from the level element            
            XmlAttributeCollection xac = levelInfo.Attributes;
            string levelName = xac["Id"].Value;
            int levelWidth = int.Parse(xac["Width"].Value);
            int levelHeight = int.Parse(xac["Height"].Value);
            int nrOfGoals = 0;
		    
            // Read the layout of the level
            XmlNodeList levelLayout = levelInfo.SelectNodes("L");
            
            // Declare the level map
            ItemType[,] levelMap = new ItemType[levelWidth, levelHeight];
            
            // Read the level line by line
            for (int i = 0; i < levelHeight; i++)
            {
                string line = levelLayout[i].InnerText;
                bool wallEncountered = false;
                
                // Read the line character by character
                for (int j = 0; j < levelWidth; j++)
                {
                    // Fills the rest of the line with spaces if the end of the line is
                    // end of the line is shoter than the width of the level
                    if (j >= line.Length)
                        levelMap[j, i] = ItemType.Space;
                    else
                    {
                        switch (line[j].ToString())
                        {
                            case " ":
                                if (wallEncountered)
                                    levelMap[j, i] = ItemType.Floor;
                                else
                                    levelMap[j, i] = ItemType.Space;
                                break;
                            case "#":
                                levelMap[j, i] = ItemType.Wall;
                                wallEncountered = true;
                                break;
                            case "$":
                                levelMap[j, i] = ItemType.Package;
                                break;
                            case ".":
                                levelMap[j, i] = ItemType.Goal;
                                nrOfGoals++;
                                break;
                            case "@":
                                levelMap[j, i] = ItemType.Sokoban;
                                break;
                            case "*":
                                levelMap[j, i] = ItemType.PackageOnGoal;
                                nrOfGoals++;
                                break;
                            case "+":
                                levelMap[j, i] = ItemType.SokobanOnGoal;
                                nrOfGoals++;
                                break;
                            case "=":
                                levelMap[j, i] = ItemType.Space;
                                break;
                        }
                    }
                }
            }
            
            // Add a new level to the collection of levels in the level set.
            levels.Add(new Level(levelName, levelMap, levelWidth,
                levelHeight, nrOfGoals, levelNr, title));
        }
        
        
        // Gets a list of all level sets in the level set directory. This is
        // used for the user to select a level set he wants to play. Therefore
        // it's not needed to load the levels in each level set.
        public static ArrayList GetAllLevelSetInfos()
        {
            ArrayList levelSets = new ArrayList();
            
            // Read current path and remove the 'file:/' from the string
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                .GetName().CodeBase).Substring(6);
            
            // Read all files from the levels directory
            string [] fileEntries = Directory.GetFiles(path + "/levels");
            
            // Read the level info from the files with an .xml extension
            foreach (string filename in fileEntries)
            {
                FileInfo fileInfo = new FileInfo(filename);
                
                if (fileInfo.Extension.Equals(".xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);
                    
                    string title = doc.SelectSingleNode("//Title").InnerText;
                    string description =
                        doc.SelectSingleNode("//Description").InnerText;
                    string email = doc.SelectSingleNode("//Email").InnerText;
                    string url = doc.SelectSingleNode("//Url").InnerText;
                    XmlNode levelInfo
                        = doc.SelectSingleNode("//LevelCollection");
                    string author = levelInfo.Attributes[0].Value;
                    XmlNodeList levels = doc.SelectNodes("//Level");
                    
                    levelSets.Add(new LevelSet(title, description, email,
                        url, author, levels.Count, filename));
                }
            }
            
            return levelSets;
        }
    }
}
