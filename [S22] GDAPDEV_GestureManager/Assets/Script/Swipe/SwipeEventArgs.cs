using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEventArgs : MonoBehaviour
{
    private ESwipeDirection _direction;


    public ESwipeDirection Direction
    {
        get { return this._direction; }
        set { this._direction = value; }
    }



    public SwipeEventArgs(ESwipeDirection direction)
    {
        this._direction = direction;

    }
}
