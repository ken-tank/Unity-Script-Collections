using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(ControlTypes))]
public class Player_Move : MonoBehaviour
{
    ControlType controlType => GetComponent<ControlTypes>().controlType;

    [SerializeField] float speed = 5;
    [Tooltip("This Value Need Axis Input 'Sprint' on Input Manager")]
    [SerializeField] float sprintSpeedMultiply = 2f;
    [SerializeField] float jumpForce = 5;

    Vector2 axis_keyboard => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
    [HideInInspector] public Vector2 axis_touch;
    bool sprint_keyboard => Input.GetButton("Sprint");
    [HideInInspector] public bool sprint_touch;
    Rigidbody rb;
    CapsuleCollider capsuleCollider;
    bool isGround = true;

    void Start() 
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody comp))
        {
            rb = comp;
        }
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
    
    void FixedUpdate()
    {
        if (isGround) 
        {
            Move();
        }
    }

    void Update() 
    {
        DetectGround(0.2f);

        switch (controlType)
        {
            case ControlType.Keyboard_and_Mouse:
            if (Input.GetButtonDown("Jump") && isGround)
            {
                Jump();
            }
            break;
        }
    }

    void Move()
    {
        Vector3 direction = Vector3.zero;
        switch (controlType)
        {   
            case ControlType.Keyboard_and_Mouse:
            if (!sprint_keyboard)
            {
                direction = (transform.right * axis_keyboard.x) + (transform.forward * axis_keyboard.y) + new Vector3(0, rb.velocity.y, 0);
            }
            else
            {
                direction = (transform.right * (axis_keyboard.x * sprintSpeedMultiply)) + (transform.forward * (axis_keyboard.y * sprintSpeedMultiply)) + new Vector3(0, rb.velocity.y, 0);
            }
            break;

            case ControlType.Touch:
            if (!sprint_touch)
            {
                direction = (transform.right * axis_touch.x * speed) + (transform.forward * axis_touch.y * speed) + new Vector3(0, rb.velocity.y, 0);
            }
            else
            {
                direction = (transform.right * (axis_touch.x * speed * sprintSpeedMultiply)) + (transform.forward * (axis_touch.y * speed * sprintSpeedMultiply)) + new Vector3(0, rb.velocity.y, 0);
            }
            break;
        }
        rb.velocity = direction;
    }

    public void SetGround(bool value)
    {
        isGround = value;
    }

    public void Jump() 
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    void DetectGround(float groundDistance) 
    {
        Vector3 center = transform.position + Vector3.up * capsuleCollider.height;
        Vector3 size = new Vector3(capsuleCollider.radius, capsuleCollider.height - 0.1f, capsuleCollider.radius);
        if (Physics.BoxCast(center, size, Vector3.down, Quaternion.identity, groundDistance))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
}
