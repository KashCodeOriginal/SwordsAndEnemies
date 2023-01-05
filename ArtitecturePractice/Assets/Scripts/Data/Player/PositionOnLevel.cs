using System;

namespace Data.Player
{
    [Serializable]
    public class PositionOnLevel
    {
        public PositionOnLevel(string levelName, Vector3Data position)
        {
            LevelName = levelName;
            Position = position;
        }
    
        public PositionOnLevel(string sceneName)
        {
            LevelName = sceneName;
        }

        public string LevelName;

        public Vector3Data Position;
    }
}