using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeReceiver : MonoBehaviour
{


    [SerializeField] public GameObject Object;
    [SerializeField] public List<GameObject> objectList;


    // Start is called before the first frame update
    void Start()
    {
        if (Singleton.Instance != null)
        {
            Debug.Log("SwipeReceiver: Subscribing to OnSwipe event");
            Singleton.Instance.OnSwipe += this.OnSwipe;
            Object.SetActive(false);
        }
        else
        {
            Debug.LogError("Singleton instance is null. Ensure Singleton is initialized before SwipeReceiver.");
        }

    }

    private void OnDisable()
    {
        Singleton.Instance.OnSwipe -= this.OnSwipe;
    }

    public void OnSwipe(object sender, SwipeEventArgs args)
    {
        if (args.HitObject == null)
        {
            Debug.Log("SwipeReceiver: OnSwipe called");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
