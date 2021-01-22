using UnityEngine;


[CreateAssetMenu(fileName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public Rigidbody2D Bullet;
    public float Force;
    public float Damage;
}
