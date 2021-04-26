using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TilemapCollGen : MonoBehaviour
{
    private void Start()
    {
       gameObject.GetComponent<CompositeCollider2D>().GenerateGeometry();
    }
}
