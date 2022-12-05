using System;

namespace Data.Player
{
    [Serializable]
    public class Vector3Data
    {
        public Vector3Data(float x, float y, float z)
        {
           X = x; 
           Y = y;
           Z = z;
        }

        public float X;
        public float Y;
        public float Z;
    }
}