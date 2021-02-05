using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Data/WeaponData")]
    public class WeaponData : ScriptableObject
    {
        public AudioClip FireClip;
        public float FireRate;
    }
}