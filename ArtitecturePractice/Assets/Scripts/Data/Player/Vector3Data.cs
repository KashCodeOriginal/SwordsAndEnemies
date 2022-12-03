using System;
using UnityEngine;

namespace Data.Player
{
    [Serializable]
    public class Vector3Data
    {
        public Vector3Data(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        private float _x;
        private float _y;
        private float _z;

        public float X => _x;
        public float Y => _y;
        public float Z => _z;
    }
}