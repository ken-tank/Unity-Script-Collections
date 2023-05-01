using UnityEngine;
using UnityEngine.Events;

public class MouseInput : MonoBehaviour
{
    [SerializeField] float sensitive = 1;

    [Space(20)]
    public UnityEvent<Vector2> mouseDelta;

    void Update() 
    {
        Vector2 delta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * sensitive;
        mouseDelta.Invoke(delta);
    }

    void Start() 
    {
        LockMouse(true);
    }

    void OnEnable() 
    {
        LockMouse(true);
    }

    void OnDisable() 
    {
        LockMouse(false);
    }

    void LockMouse(bool value) 
    {
        Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !value;
    }
}
