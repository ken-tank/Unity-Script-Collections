using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MediationManager))]
public class Mediation_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    [MenuItem("GameObject/Mediation/Mediation Manager", false, 1)]
    static void CreateManager() 
    {
        GameObject target = new GameObject("Mediation Manager");
        target.AddComponent<MediationManager>();
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
        target.AddComponent<BannerMediation>();
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
        target.AddComponent<InterstitialMediation>();
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
        target.AddComponent<RewardMediation>();
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
