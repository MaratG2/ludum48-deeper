using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public int musicState = 0;
    private int wasMusicState = 0;
    public AudioSource[] audioSources;
    private Player player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(wasMusicState != musicState)
        {
            foreach (var source in audioSources)
            {
                source.Stop();
            }
            audioSources[musicState].Play();
        }

        wasMusicState = musicState;
    }
}
