using UnityEngine;

[RequireComponent(typeof(ControlTypes))]
public class Player_Look : MonoBehaviour
{
    ControlType controlType => GetComponent<ControlTypes>().controlType;

    [SerializeField] bool limitY, limitX;
    [SerializeField] float sensitive = 1, min_y = -90, max_y = 90, min_x = -90, max_x = 90;
    [SerializeField] Transform cam;

    Vector2 axis_keyboard => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * sensitive;
    [HideInInspector] public Vector2 axis_touch;
    float rot_x, rot_y;
    Vector3 startSelf, startCam;

    bool start_once = true;
    
    void Start()
    {
        startSelf = transform.localEulerAngles;
        startCam = cam.localEulerAngles;

        if (start_once) 
        {   
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            start_once = false;
        }
    }

    void OnEnable() 
    {
        if(!start_once) 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void OnDisable() 
    {
        if(!start_once) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    void Update()
    {
        switch (controlType)
        {
            case ControlType.Keyboard_and_Mouse:
            if (limitX) rot_x = Mathf.Clamp(rot_x + axis_keyboard.x, min_x, max_x);
            else rot_x += axis_keyboard.x;
            if (limitY) rot_y = Mathf.Clamp(rot_y + axis_keyboard.y, min_y, max_y);
            else rot_y += axis_keyboard.y;
            break;

            case ControlType.Touch:
            if (limitX) rot_x = Mathf.Clamp(rot_x + axis_touch.x * sensitive, min_x, max_x);
            else rot_x += axis_touch.x * sensitive;
            if (limitY) rot_y = Mathf.Clamp(rot_y + axis_touch.y * sensitive, min_y, max_y);
            else rot_y += axis_touch.y * sensitive;
            break;
        }
        
        FreeLook();
    }

    void FreeLook()
    {
        transform.localEulerAngles = startSelf + new Vector3(0, rot_x, 0);
        cam.localEulerAngles = startCam + new Vector3(-rot_y, 0, 0);
    }
}
