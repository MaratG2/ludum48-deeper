using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    //target can be rigidbody2d component of a player or some object
    public Rigidbody2D target;
    //speed of scrolling
    public float speed;
    public float widthScr = 200;
    private float initPos;
    private float initPosR;
    private float initPosL;
    public  float damp = 0.3f;
    void Start()
    {
        initPos = transform.localPosition.x;
        initPosR = initPos + damp;
        initPosL = initPos - damp;
        //Create a clone for filling rest of the screen
        //GameObject objectCopy = GameObject.Instantiate(this.gameObject);
        //Destroy ScrollBackground component in clone
        //Destroy(objectCopy.GetComponent<ParallaxEffect>());
        //Set clone parent and position
        //objectCopy.transform.SetParent(this.transform);
        //objectCopy.transform.localPosition = new Vector3(getWidth(), 0, 0);
    }

    void FixedUpdate()
    {
        //get target velocity
        //if you wish to replace target with a non-rigidbody object, this is the place
        float targetVelocity = target.velocity.x;
        //translate sprite according to target velocity
        this.transform.Translate(new Vector3(-speed * targetVelocity, 0, 0) * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, initPosL, initPosR), transform.position.y);
    }
}
