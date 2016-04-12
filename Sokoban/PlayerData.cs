// Jordan Aitken
// HND Software Development
// Sokoban Project

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;

namespace JordanSokoban
{
	/// <summary>
	/// This class handles the user's savegame (XML). It loads the required
	/// information from the file to be used in the game. It also adds/updates
	/// new information to the file.
	/// </summary>
	public class PlayerData
	{
	    private string name = string.Empty;             // Player name
	    private string filename = string.Empty;         // Savegame URI
	    private int lastFinishedLevel = 0;
	    private string lastPlayedSet = string.Empty;
	
        #region Properties
        
        public string Name
        {
            get { return name; }
        }
        
        public int LastFinishedLevel
        {
            get { return lastFinishedLevel; }
        }

        public string LastPlayedSet
        {
            get { return lastPlayedSet; }
        }
		
		#endregion
	
		
	    /// <summary>
	    /// Constructor. Sets the player name and path+filename for the player
	    /// </summary>
	    /// <param name="aName">Player name</param>
		public PlayerData(string aName)
		{
		    name = aName;
		    
            // Read current path and remove the 'file:/' from the string
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly()
                .GetName().CodeBase).Substring(6);
            filename = path + "/savegames/" + aName + ".xml";
		}
		
		
		/// <summary>
		/// Creates a savegame for a new player. This happens when a new player
		/// has selected a level set that he wants to play. The XML contains
		/// the player name, name of the last played level set, the number of
		/// last played level, the levelSets node, and therein a levelSet
		/// element with a title attribute containing the name of the levelset.
		/// <param name="levelSet">LevelSet object</param>
        public void CreatePlayer(LevelSet levelSet)
        {
            XmlDocument doc = new XmlDocument();
            
            // Create new file (<playerName>.xml) in savegames directory
            XmlTextWriter writer = new XmlTextWriter(filename, null);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteProcessingInstruction("xml",
                "version='1.0' encoding='ISO-8859-1'");
            writer.WriteStartElement("savegame");
            writer.Close();
		    
            doc.Load(filename);
            
            // Add all elements
            XmlNode root = doc.DocumentElement;
            XmlElement playerName = doc.CreateElement("playerName");
            playerName.InnerText = name;
            XmlElement lastPlayedNameSet =
                doc.CreateElement("lastPlayedNameSet");
            lastPlayedNameSet.InnerText = levelSet.Filename;
            XmlElement lastFinishedLevel =
                doc.CreateElement("lastFinishedLevel");
            lastFinishedLevel.InnerText = "0";
            XmlElement levelSets = doc.CreateElement("levelSets");
            
            XmlElement nodeLevelSet = doc.CreateElement("levelSet");
            XmlAttribute xa = doc.CreateAttribute("title");
            xa.Value = levelSet.Title;
            nodeLevelSet.Attributes.Append(xa);
            XmlElement lastFinishedLevelInSet =
                doc.CreateElement("lastFinishedLevelInSet");
            lastFinishedLevelInSet.InnerText = "0";
            
            nodeLevelSet.AppendChild(lastFinishedLevelInSet);
            levelSets.AppendChild(nodeLevelSet);
            root.AppendChild(playerName);
            root.AppendChild(lastPlayedNameSet);
            root.AppendChild(lastFinishedLevel);
            root.AppendChild(levelSets);
            
            doc.Save(filename);
        }
        
		
        /// <summary>
        /// This method is called when we choose and existing player but we
        /// don't continue our previous game. In the case the user can choose a
        /// different level set, so we need to set this level set here. We also
        /// set the last finished level to 0, so we begin with the first level
        /// in the level set. Of course, if the user has already done some
        /// levels in the level set, he can jump to the other levels later.
        /// </summary>
        /// <param name="levelSet"></param>
		public void LoadPlayerInfo(LevelSet levelSet)
		{            
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode lastPlayedNameSet =
                doc.SelectSingleNode("//lastPlayedNameSet");
            lastPlayedNameSet.InnerText = levelSet.Filename;
                        
            doc.Save(filename);
            lastFinishedLevel = 0;

            // TO DO: Load other player settings, if any
		}
		
		
		/// <summary>
		/// This method is called when we choose an existing player and we also
		/// choose to continue the previously saved game. In this case we need
		/// to load the last played set and the last finished level from the
		/// savegame, so we can jump to the level where the player left off the
		/// previous time he played the game.
		/// </summary>
        public void LoadLastGameInfo()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            
            XmlNode lastPlayedNameSet =
                doc.SelectSingleNode("//lastPlayedNameSet");
            lastPlayedSet = lastPlayedNameSet.InnerText;
            XmlNode lastFinishedLvl =
                doc.SelectSingleNode("//lastFinishedLevel");
            lastFinishedLevel = int.Parse(lastFinishedLvl.InnerText);
        }
        
        
        /// <summary>
        /// This method is called when we choose and existing player but we
        /// don't continue our previous game. After selecting a levelset that
        /// the user wants to play, we add this levelset to the savegame (if it
        /// isn't already there). We also set the last played levelset to the
        /// levelset the player just picked and we also set the last played
        /// level to 0.
        /// The reason why we don't call this method when we create a new
        /// player (in the CreatePlayer() method) is that upon creating a new
        /// player, we already set the level set (that he picked for the very
        /// first time).
        /// </summary>
        /// <param name="levelSet">LevelSet object</param>
        public void SaveLevelSet(LevelSet levelSet)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode lastFilename = doc.SelectSingleNode("//lastPlayedNameSet");
            lastFilename.InnerText = levelSet.Filename;
            XmlNode lastFinishedLvl = doc.SelectSingleNode("//lastFinishedLevel");
            lastFinishedLvl.InnerText = "0";

            XmlNode setName = doc.SelectSingleNode("/savegame/levelSets/" +
                "levelSet[@title = \"" + levelSet.Title + "\"]");
            
            if (setName == null) // We play set for the first time
            {
                XmlNode levelSets = doc.GetElementsByTagName("levelSets")[0];
                
                XmlElement newLevelSet = doc.CreateElement("levelSet");
                XmlAttribute xa = doc.CreateAttribute("title");
                xa.Value = levelSet.Title;
                newLevelSet.Attributes.Append(xa);
                XmlElement lastFinishedLevelInSet = doc.CreateElement("lastFinishedLevelInSet");
                lastFinishedLevelInSet.InnerText = "0";
                
                newLevelSet.AppendChild(lastFinishedLevelInSet);
                levelSets.AppendChild(newLevelSet);
            }
            
            doc.Save(filename);
        }
		
		
        /// <summary>
        /// This method is called after finishing a level. It updates the last
        /// finished level. It then searches if the level within the current
        /// level set exists in the savegame. If it doesn't, the player has
        /// finished the level for the first time. In this case we add a level
        /// node with a level number attribute. The level node consists of two
        /// elements: moves and pushes.
        /// If the level already exists in the level set, it means that the
        /// player has already finished this level before. In this case, we
        /// check if it took the player less pushes than the previous time. If
        /// so, we update the pushes and moves in the savegame. If his pushes
        /// are equal and the moves are less than the previous time, we update
        /// the moves. In any other case, this attempt of solving the level was
        /// worse than the previous time, so we don't update anything.
        /// </summary>
        /// <param name="level">Level object</param>
		public void SaveLevel(Level level)
		{
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNode lastFinishedLvl = doc.SelectSingleNode("//lastFinishedLevel");
		    lastFinishedLvl.InnerText = level.LevelNr.ToString();
            
            XmlNode setName = doc.SelectSingleNode("/savegame/levelSets/" +
                "levelSet[@title = \"" + level.LevelSetName + "\"]");
            XmlNode nodeLevel = setName.SelectSingleNode("level[@levelNr = " +
                level.LevelNr + "]");

            if (nodeLevel == null)
            {            
                XmlElement nodeNewLevel = doc.CreateElement("level");
                XmlAttribute xa = doc.CreateAttribute("levelNr");
                xa.Value = level.LevelNr.ToString();
                nodeNewLevel.Attributes.Append(xa);
                XmlElement moves = doc.CreateElement("moves");
                moves.InnerText = level.Moves.ToString();
                XmlElement pushes = doc.CreateElement("pushes");
                pushes.InnerText = level.Pushes.ToString();
                
                nodeNewLevel.AppendChild(moves);
                nodeNewLevel.AppendChild(pushes);
                setName.AppendChild(nodeNewLevel);
            }
            else
            {
                XmlElement moves = nodeLevel["moves"];
                XmlElement pushes = nodeLevel["pushes"];
                int nrOfMoves = int.Parse(moves.InnerText);
                int nrOfPushes = int.Parse(pushes.InnerText);
                
                if (level.Pushes < nrOfPushes)
                {
                    pushes.InnerText = level.Pushes.ToString();
                    moves.InnerText = level.Moves.ToString();
                }
                else if (level.Pushes == nrOfPushes && level.Moves < nrOfMoves)
                    moves.InnerText = level.Moves.ToString();
            }
            
            doc.Save(filename);
		}
        

        /// <summary>
        /// The method returns an ArrayList containing all the player names. We
        /// read the player names from the savegames in the savegame directory.
        /// </summary>
        /// <returns>ArrayList with all the player names</returns>
        public static ArrayList GetPlayers()
        {
            ArrayList playerNames = new ArrayList();
		    
            // Read current path and remove the 'file:/' from the string
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().
                GetName().CodeBase).Substring(6);

            // Read all files from the savegame directory
            string [] fileEntries = Directory.GetFiles(path + "/savegames");
		    
            // Read the playerName tag from the files with an .xml extension
            foreach(string filename in fileEntries)
            {
                FileInfo fileInfo = new FileInfo(filename);
                if (fileInfo.Extension.Equals(".xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filename);
                    
                    XmlNode playerName = doc.SelectSingleNode("//playerName");
                    if (playerName != null)
                        playerNames.Add(playerName.InnerText);
                }
            }
		    
            return playerNames;
        }
	}
}
