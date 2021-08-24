using Asteroids.Interfaces;
using UnityEngine;


public class Observer
{
    
    public void Add(IEnemy enemy)
    {
        enemy.DeathEnemy += OutputDeathEnemyEnemy;
    }

    public void Remove(IEnemy enemy)
    {
        enemy.DeathEnemy -= OutputDeathEnemyEnemy;
    }
    
    public void OutputDeathEnemyEnemy(IEnemy enemy)
    {
        Debug.Log(enemy.GetType().Name + " погиб!");
        Remove(enemy);
    }
}
