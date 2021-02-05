using System.Collections.Generic;
using System.Linq;
using Asteroids.Interfaces;
using Asteroids.Models;
using UnityEngine;


namespace Asteroids.Services
{
    public class TimeRewinder: IFrameUpdatable, IFixedUpdatable
{
    private readonly Dictionary<Rigidbody2D, List<PointTime>> _points = new Dictionary<Rigidbody2D, List<PointTime>>();
    private KeyCode _rewindKey = KeyCode.Q;
    private bool _isRewinding = false;
    private float _recordTime = 5.0f;

    public TimeRewinder(float recordTime, KeyCode rewindKey)
    {
        _recordTime = recordTime;
        _rewindKey = rewindKey;
    }
    
    public void AddRewinder(Rigidbody2D rigidbody)
    {
        if (_points.FirstOrDefault(x => x.Key == rigidbody).Key == null)
        {
            _points.Add(rigidbody, new List<PointTime>());
        }
        return;
    }

    public void RemoveRewinder(Rigidbody2D rigidbody)
    {
        var removing = _points.FirstOrDefault(x => x.Key == rigidbody).Key;
        if (removing != null)
        {
            _points.Remove(removing);
        }
    }
    
    private void Rewind ()
    {
        foreach (var item in _points)
        {
            if (_points.Count > 1)
            {
                PointTime pointInTime = item.Value[0];
                item.Key.transform.position = pointInTime.Position;
                item.Key.transform.rotation = pointInTime.Rotation;
                item.Value.RemoveAt(0);
            }
            else
            {
                PointTime pointInTime = item.Value[0];
                item.Key.transform.position = pointInTime.Position;
                item.Key.transform.rotation = pointInTime.Rotation;
                StopRewind();
            }
        }
    }
    private void Record ()
    {
        foreach (var item in _points)
        {
            if (_points.Count > Mathf.Round(_recordTime /
                                            Time.fixedDeltaTime))
            {
                item.Value.RemoveAt(item.Value.Count - 1);
            }
            item.Value.Insert(0, new PointTime(item.Key.transform.position,
                item.Key.transform.rotation, item.Key.velocity, item.Key.angularVelocity)); 
        }
        
    }

    private void StartRewind()
    {
        foreach (var item in _points)
        {
            _isRewinding = true;
            item.Key.isKinematic = true;
        }
    }
    private void StopRewind ()
    {
        foreach (var item in _points)
        {
            _isRewinding = false;
            item.Key.isKinematic = false;
            item.Key.velocity = item.Value[0].Velocity;
            item.Key.angularVelocity = item.Value[0].AngularVelocity;  
        }
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(_rewindKey))
        {
            StartRewind();
        }
        if (Input.GetKeyUp(_rewindKey))
        {
            StopRewind();
        }
    }

    public void FixedUpdate()
    {
        if (_isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }

    }
}
}
