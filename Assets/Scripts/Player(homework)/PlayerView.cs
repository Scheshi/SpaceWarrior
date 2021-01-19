using System;
using UnityEngine;


public class PlayerView : MonoBehaviour, IDisposable
{
    public event Action Collision;

    public void Dispose()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collision?.Invoke();
    }

}
