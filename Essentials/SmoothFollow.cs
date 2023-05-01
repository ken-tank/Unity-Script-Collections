using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [Header("Follow Properties")]
    public Transform target = null;
    [SerializeField] float smoothTime = 0.1f;
    
    [Space(20)]
    [SerializeField] Vector3 offset;
    [SerializeField] bool isLocal;

    [Header("Contrain")]
    [SerializeField] bool X = true;
    [SerializeField] bool Y = true;
    [SerializeField] bool Z = true;

    // Variable Sementara
    Vector3 velocity = Vector3.zero;
    Vector3 pos;

    private void OnDrawGizmos() 
    {
        Follow();
    }

    void Update() 
    {
        Follow();
    }

    Vector3 targetPos;
    void Follow() 
    {
        if (target != null) 
        {
            if (X) targetPos = new Vector3(target.position.x, targetPos.y, targetPos.z);
            if (Y) targetPos = new Vector3(targetPos.x, target.position.y, targetPos.z);
            if (Z) targetPos = new Vector3(targetPos.x, targetPos.y, target.position.z);

            if (isLocal) pos = targetPos + (target.up * offset.y) + (target.right * offset.x) + (target.forward * offset.z);
            else pos = targetPos + offset;
        }

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothTime);
    }
}
