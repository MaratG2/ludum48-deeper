using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInstance : MonoBehaviour
{
    public int musicToPlay = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            GetComponentInParent<MusicController>().musicState = musicToPlay;
    }
}
