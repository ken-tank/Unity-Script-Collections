using UnityEngine;

public enum ControlType
{
    Auto,
    Keyboard_and_Mouse,
    Touch
}

public class ControlTypes : MonoBehaviour
{
    public ControlType controlType = ControlType.Auto;

    void Awake() 
    {
        if (controlType == ControlType.Auto)
        {
            switch (SystemInfo.deviceType)
            {
                case DeviceType.Desktop:
                controlType = ControlType.Keyboard_and_Mouse;
                break;

                case DeviceType.Handheld:
                controlType = ControlType.Touch;
                break;
            }
        }
    }
}
