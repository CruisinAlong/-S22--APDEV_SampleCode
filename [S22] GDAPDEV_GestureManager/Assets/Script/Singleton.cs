using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    private Touch _trackedFinger;
    private float _gestureTime;

    [SerializeField]
    private TapProperty _tapProperty;
    private SwipeProperty _swipeProperty;
    private ESwipeDirection _swipeDirection;
    public EventHandler<TapEventArgs> OnTap;
    public EventHandler<SwipeEventArgs> OnSwipe;


    private Vector2 _startPoint = Vector2.zero;
    private Vector2 _endPoint = Vector2.zero;

    private void CheckTap()
    {
        Debug.Log($"Singleton: Checking tap with gestureTime = {_gestureTime}, startPoint = {_startPoint}, endPoint = {_endPoint}");

        if (this._gestureTime <= this._tapProperty.Time && Vector2.Distance(this._startPoint, this._endPoint) <= (Screen.dpi * this._tapProperty.MaxDistance))
        {
            Debug.Log("Singleton: Tap conditions met");
            this.FireTapEvent();
        }
        else
        {
            Debug.Log("Singleton: Tap conditions not met");
        }
    }   

    private void CheckSwipe()
    {
        Debug.Log($"Singleton: Checking tap with gestureTime = {_gestureTime}, startPoint = {_startPoint}, endPoint = {_endPoint}");

        if (this._gestureTime <= this._swipeProperty.Time && Vector2.Distance(this._startPoint, this._endPoint) >= (Screen.dpi * this._swipeProperty.MinDistance))
        {
            Debug.Log("Singleton: Swipe conditions met");
        }
        else
        {
            Debug.Log("Singleton: Swipe condition not met");
        }
    }

    private void ESwipeDirection(Vector2 rawDirection)
    {

        // rawDirection is end point - start point
        if(rawDirection.x > 0)
        {
            return ESwipeDirection.LEFT;
        }
        if(rawDirection.y > 0)
        {
            return ESwipeDirection.UP;
        }
        if(rawDirection.x < 0)
        {
            return ESwipeDirection.RIGHT;
        }
        if(rawDirection.y < 0)
        {
            return ESwipeDirection.DOWN;
        }

    }

    private GameObject GetHitObject(Vector2 screenPoint)
    {
        GameObject hitObject = null;
        Ray ray = Camera.main.ScreenPointToRay(this._startPoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            hitObject = hit.collider.gameObject;
            Debug.Log("Singleton: Physics Raycast condition met");
        }

        return hitObject;
    }

    private void FireTapEvent()
    {
        if (this.OnTap != null)
        {
            Debug.Log("Singleton: Firing OnTap event");

            GameObject hitObject = this.GetHitObject(this._startPoint);

            TapEventArgs args = new TapEventArgs(this._startPoint, hitObject);
            this.OnTap(this, args);

            if (hitObject != null)
            {
                ITappable handler = hitObject.GetComponent<ITappable>();
                if (handler != null)
                {
                    handler.OnTap(args);
                }
            }
        }
        else
        {
            Debug.Log("Singleton: OnTap event is null");
        }
    }

    private void FireSwipeEvent()
    {
        if(this.OnSwipe != null)
        {
            Debug.Log("Singleton: Firing OnSwipe event");


            SwipeEventArgs args = new SwipeEventArgs();
            this.OnSwipe(this, args);


    

        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            this._trackedFinger = Input.GetTouch(0);
            switch(this._trackedFinger.phase) 
            {
                case TouchPhase.Began:
                    this._startPoint = this._trackedFinger.position;
                    this._gestureTime = 0;
                    break;
                case TouchPhase.Ended:
                    this._endPoint = this._trackedFinger.position;
                    this.CheckTap();
                    this.CheckSwipe();
                    break;
                default:
                    this._gestureTime += Time.deltaTime;
                    break;
            }
        }
    }
}
