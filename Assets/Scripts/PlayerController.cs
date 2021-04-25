using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0.5f, 10f)] [SerializeField] private float horizontalSpeed = 1f;
    [Range(1f, 10f)] [SerializeField] private float verticalSpeed = 1f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

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
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        horizontalInputRaw = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        verticalInputPos = verticalInput * verticalSpeed;

        if (Time.timeScale != 0f)
            angleMouse();
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontalInput, verticalInputPos);
    }

    float angleMouse()
    {
        mouse_pos = Input.mousePosition;
        mouse_pos.z = 5.23f; //The distance between the camera and object
        object_pos = Camera.main.WorldToScreenPoint(transform.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

        if (angle >= -90f && angle <= 90f)
        {
            transform.localScale = new Vector3(-1, 1, 1); //maybe need to delete
            addAngle = 0f;
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); //maybe need to delete
            addAngle = -180f;
        }

        angleBase = angle + addAngle;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBase));

        return angle;
    }
}
