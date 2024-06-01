using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnReceiver : MonoBehaviour, ITappable
{
    public void OnTap(TapEventArgs args)
    {
        Destroy(this.gameObject);
        Debug.Log("Clicked a cube");
    }
}
