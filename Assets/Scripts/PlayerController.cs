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

    Vector3 mouse_pos;
    Vector3 object_pos;
    float angle;
    float angleBase;
    float addAngle = 0f;

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

        angleMouse();
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontalInput, -0.5f + verticalInputPos);
    }

    float angleMouse()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        angleBase = angle + addAngle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBase));

        if (angle >= -90f && angle <= 90f)
        {
            sprite.flipX = true;
            addAngle = 0f;
        }
        else
        {
            sprite.flipX = false;
            addAngle = -180f;
        }  

        return angle;
    }
}
