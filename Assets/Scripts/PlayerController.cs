using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Range(0.5f, 10f)] [SerializeField] private float horizontalSpeed = 1f;
    [Range(1f, 10f)] [SerializeField] private float verticalSpeed = 1f;
    [SerializeField] private float smoothCam = 10f;
    [SerializeField] private float smoothIntert = 10f;
    public bool movementBlock;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float horizontalInput;
    private float horizontalInputRaw;
    private float verticalInput;
    private float verticalInputPos;

    Vector3 mouse_pos;
    Vector3 object_pos;
    float angle = 0f;
    float angleBase;
    float wasAdd = 0;
    float addAngle = 180f;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }
    void Start()
    {

    }
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        horizontalInputRaw = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        animator.SetFloat("speed", Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

        if (Time.timeScale != 0f)
            angleMouse();
    }

    void FixedUpdate()
    {
        if(!movementBlock)
            rb2d.velocity = Vector2.Lerp(rb2d.velocity, new Vector2(horizontalInput * horizontalSpeed, verticalInput * verticalSpeed), Time.fixedDeltaTime * smoothIntert);
    }

    float angleMouse()
    {
        rb2d.angularVelocity = 0;

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
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
        Quaternion newRot = Quaternion.AngleAxis(angleBase, Vector3.forward);
        
        if (wasAdd == addAngle)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * smoothCam);
        }
        else
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBase));

        wasAdd = addAngle;
        return angle;
    }
}
