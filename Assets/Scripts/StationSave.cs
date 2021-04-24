using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSave : MonoBehaviour
{
    public Transform savePoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player pl = collision.GetComponent<Player>();
        if(pl)
        {
            pl.savePos = savePoint.position;
        }
    }
}
