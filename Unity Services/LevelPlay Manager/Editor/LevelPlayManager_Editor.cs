using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelPlay_Manager))]
public class LevelPlayManager_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    [MenuItem("GameObject/Mediation/Mediation Manager", false, 0)]
    static void CreateManager() 
    {
        GameObject target = new GameObject("Mediation Manager");
        target.AddComponent<LevelPlay_Manager>();
        if (Selection.activeTransform != null)
        {
            target.transform.parent = Selection.activeTransform;
            target.transform.localPosition = Vector3.zero;
        }
        else
        {
            target.transform.position = Vector3.zero;
        }

        Selection.activeTransform = target.transform;
    }

    [MenuItem("GameObject/Mediation/Preload Ads", false, 0)]
    static void CreatePreLoad() 
    {
        GameObject target = new GameObject("Preload Ads");
        target.AddComponent<PreLoadAds>();
        if (Selection.activeTransform != null)
        {
            target.transform.parent = Selection.activeTransform;
            target.transform.localPosition = Vector3.zero;
        }
        else
        {
            target.transform.position = Vector3.zero;
        }

        Selection.activeTransform = target.transform;
    }

    [MenuItem("GameObject/Mediation/Elements/Banner", false)]
    static void CreateBanner() 
    {
        GameObject target = new GameObject("Banner Mediation");
        target.AddComponent<Banner_LevelPlay>();
        if (Selection.activeTransform != null)
        {
            target.transform.parent = Selection.activeTransform;
            target.transform.localPosition = Vector3.zero;
        }
        else
        {
            target.transform.position = Vector3.zero;
        }

        Selection.activeTransform = target.transform;
    }
    [MenuItem("GameObject/Mediation/Elements/Interstatial", false)]
    static void CreateInterstatial() 
    {
        GameObject target = new GameObject("Interstatial Mediation");
        target.AddComponent<Interstitional_LevelPlay>();
        if (Selection.activeTransform != null)
        {
            target.transform.parent = Selection.activeTransform;
            target.transform.localPosition = Vector3.zero;
        }
        else
        {
            target.transform.position = Vector3.zero;
        }

        Selection.activeTransform = target.transform;
    }
    [MenuItem("GameObject/Mediation/Elements/Rewarded", false)]
    static void CreateRewarded() 
    {
        GameObject target = new GameObject("Rewarded Mediation");
        target.AddComponent<Rewarded_LevelPlay>();
        if (Selection.activeTransform != null)
        {
            target.transform.parent = Selection.activeTransform;
            target.transform.localPosition = Vector3.zero;
        }
        else
        {
            target.transform.position = Vector3.zero;
        }

        Selection.activeTransform = target.transform;
    }
}
