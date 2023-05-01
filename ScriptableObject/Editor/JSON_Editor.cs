using UnityEditor;
using System.IO;
using UnityEngine;

[CustomEditor(typeof(JSON_Data))]
class JSON_Editor : Editor
{
    JSON_Data data => (JSON_Data) target;

    public void OnEnable() 
    {
        if (File.Exists(data.path))
        {
            //data.data = data.Load();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create | Update"))
        {
            if (data.fileName == "")
            {
                Debug.LogWarning("<color=yellow>JSON Warning:</color> " + "File Name Can't Empty");
            }
            else
            {
                data.Create(data.data);
                Debug.Log("<color=green>JSON Created:</color> " + data.path);
            }
        }

        if (File.Exists(data.path))
        {
            if (GUILayout.Button("Delete"))
            {
                data.Delete();
                Debug.Log("<color=red>JSON Deleted:</color> " + data.path);
            }

            if(GUILayout.Button("Copy JSON Path"))
            {
                GUIUtility.systemCopyBuffer = data.path;
            }

            EditorGUILayout.Space(20);
            EditorGUILayout.BeginFadeGroup(1);
            EditorGUILayout.LabelField("Info File: ");
            foreach (var item in data.Load().field)
            {
                try {
                    EditorGUILayout.LabelField ("-- > " + item.key + ": " + item.value);
                } catch {}
            }
            EditorGUILayout.EndFadeGroup();
        }
    }
}
