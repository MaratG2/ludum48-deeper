using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0.5f, 10f)] [SerializeField] private float horizontalSpeed = 1f;
    [Range(1f, 10f)] [SerializeField] private float verticalSpeed = 1f;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;

    private float horizontalInput;
    private float horizontalInputRaw;
    private float verticalInput;
    private float verticalInputPos;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        horizontalInputRaw = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (verticalInput >= 0)
            verticalInputPos = verticalInput * verticalSpeed;
        else
            verticalInputPos = 0;

        if (horizontalInputRaw >= 0)
            sprite.flipX = true;
        else
            sprite.flipX = false;
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontalInput, -0.5f + verticalInputPos);

    }
}
