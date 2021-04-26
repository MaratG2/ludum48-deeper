using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering;

public class EffectorScript : MonoBehaviour
{
    public int effectorNumber = 0;
    public PostProcessVolume postProcess;

    private Bloom b;
    private Vignette vg;
    private Grain gr;

    private void Start()
    {
        postProcess.profile.TryGetSettings(out b);
        postProcess.profile.TryGetSettings(out vg);
        postProcess.profile.TryGetSettings(out gr);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch(effectorNumber)
            {
                case 0:
                    collision.GetComponent<Player>().isFlashlightOn = true;
                    break;
                case 1:
                    //seems nothing
                    break;
                case 2:
                    gr.active = true;
                    gr.intensity.value = 0.2f;
                    gr.size.value = 1f;
                    //easy 
                    break;
                case 3:
                    gr.active = true;
                    gr.intensity.value = 0.5f;
                    gr.size.value = 1.5f;
                    //hard
                    break;
            }
        }
    }
}
