using UnityEngine;


namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        public GameObject PlayerPrefab;
        public GameObject ParticlesAroundPlayer;
        public float Speed;
        public float Acceleration;
        public float Hp;
        public float CameraOffset;
    }
}
