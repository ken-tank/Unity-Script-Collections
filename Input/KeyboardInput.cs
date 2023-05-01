using UnityEngine;
using UnityEngine.Events;

public class KeyboardInput : MonoBehaviour
{
    public UnityEvent<Vector2> WASD;
    public UnityEvent WASD_Up;

    int once = 1;
    void Update() 
    {
        Vector2 wasd = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (wasd.x != 0 || wasd.y != 0)
        {
            WASD.Invoke(wasd.normalized);
            once = 1;
        }
        else
        {
            if (once > 0)
            {
                WASD_Up.Invoke();
                once --;
            }
        }
    }
}
