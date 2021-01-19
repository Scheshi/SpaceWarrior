using Asteroid.Interfaces;
using System;
using UnityEngine;


public class PlayerView : MonoBehaviour, IDisposable, IPlayer
{
    public event Action<float> Losses;

    public void Dispose()
    {
        Destroy(gameObject);
    }

    public void Damage(float point)
    {
        Losses?.Invoke(point);
    }

}
