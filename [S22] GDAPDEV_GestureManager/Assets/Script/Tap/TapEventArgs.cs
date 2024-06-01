using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TapEventArgs : EventArgs
{
    private Vector2 _position;
    private GameObject _hitObject;

    public Vector2 Position
    {
        get { return this._position; }
        set { this._position = value; }
    }

    public GameObject HitObject
    {
        get { return this._hitObject; }
        set { this._hitObject = value; }
    }

    public TapEventArgs(Vector2 position, GameObject hitObject = null)
    {
        this._position = position;
        this._hitObject = hitObject;
    }
}
