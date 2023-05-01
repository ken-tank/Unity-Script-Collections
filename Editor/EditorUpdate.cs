using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class EditorUpdate : MonoBehaviour
{
    static EditorUpdate() 
    {
        EditorApplication.update += Update;
    }

    static void Update() 
    {
        // taruh code mu di sini
    }
}
