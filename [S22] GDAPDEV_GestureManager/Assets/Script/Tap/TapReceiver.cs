using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapReceiver : MonoBehaviour
{


    [SerializeField] public GameObject Object;
    [SerializeField] public List<GameObject> objectList;


    // Start is called before the first frame update
    void Start()
    {
        if (Singleton.Instance != null)
        {
            Debug.Log("TapReceiver: Subscribing to OnTap event");
            Singleton.Instance.OnTap += this.OnTap;
            Object.SetActive(false);
        }
        else
        {
            Debug.LogError("Singleton instance is null. Ensure Singleton is initialized before TapReceiver.");
        }

    }

    private void OnDisable()
    {
        Singleton.Instance.OnTap -= this.OnTap;
    }

    public void OnTap(object sender, TapEventArgs args)
    {
        if (args.HitObject == null)
        {
            Debug.Log("TapReceiver: OnTap called");

            Ray ray = Camera.main.ScreenPointToRay(args.Position);
            Vector3 spawnPos = ray.GetPoint(10);

            GameObject instance = GameObject.Instantiate(this.Object, spawnPos, Quaternion.identity);
            instance.SetActive(true);
            this.objectList.Add(instance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
