using UnityEngine;

namespace Asteroids.Models
{
    public class PointTime
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Velocity;
        public float AngularVelocity;
        
        public PointTime (Vector3 position, Quaternion rotation, Vector3
            velocity, float angularVelocity)
        {
            Position = position;
            Rotation = rotation;
            Velocity = velocity;
            AngularVelocity = angularVelocity;
        }
    }
}