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
    float addAngle = 0f;

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

        //mouse_pos = Input.mousePosition;
        //mouse_pos.z = 5.23f; //The distance between the camera and object
        //object_pos = Camera.main.WorldToScreenPoint(transform.position);
        //mouse_pos.x = mouse_pos.x - object_pos.x;
        //mouse_pos.y = mouse_pos.y - object_pos.y;
        //angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;

        //Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 direction3 = (mouseScreenPosition - (Vector2)transform.position).normalized;  
        //transform.right = Vector2.Lerp(transform.right, -direction3, Time.deltaTime * smoothCam);
        //Debug.Log(transform.right.x == -1f && transform.right.y == 0f);

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
        Quaternion newRot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, Time.deltaTime * smoothCam);

        //if (angle >= -90f && angle <= 90f)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1); //maybe need to delete
        //    addAngle = 0f;
        //}
        //else
        //{
        //    transform.localScale = new Vector3(1, 1, 1); //maybe need to delete
        //    addAngle = -180f;
        //}
        //angleBase = angle + addAngle;

        //if (wasAdd == addAngle)
        //{

        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angleBase)), Time.deltaTime * smoothCam);

        //    //Debug.Log(Quaternion.ToEulerAngles(transform.rotation).z);
        //}
        //else
        //    transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleBase));

        //var direction = Quaternion.Euler(new Vector3(0, 0, angleBase)) * transform.right;
        //var direction2 = transform.rotation * transform.right;
        //Debug.DrawRay(transform.position, direction * 150f, Color.red, 1f);
        //Debug.DrawRay(transform.position, direction2 * 150f, Color.yellow, 1f);

        //wasAdd = addAngle;

        return angle;
    }
}
