using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public enum FinishActions { Destroy, Disable }

    [SerializeField] float massa = 1;
    [SerializeField] FinishActions finishActions;

    float speed;
    float gravity;

    bool isIntialize = false;

    float startTime;
    Vector3 startForward;
    Vector3 startPoint;
    
    float deltaSpeed;
    float time = 0;
    Vector3 last, current, next, end;   
    Vector3 drop;
    RaycastHit raycast;
    bool isHitted = false;

    [Space(20)]
    public UnityEvent<RaycastHit> onHit;

    public void Initialize(Transform startPoint, float speed, float gravity)
    {
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;
        this.startPoint = startPoint.position;
        this.startForward = startPoint.forward;
        this.speed = speed;
        this.gravity = gravity * massa;

        startTime = -Time.deltaTime;

        isIntialize = true;
        Invoke("CallFinishAction", 3);
    }

    Vector3 DropPoint()
    {
        time += Time.deltaTime;
        return (startForward * speed * time) + (Vector3.up * gravity * time * time * time);
    }

    void CallFinishAction()
    {
        switch (finishActions)
        {
            case FinishActions.Destroy:
            Destroy(gameObject);
            break;

            case FinishActions.Disable:
            gameObject.SetActive(false);
            break;
        }
    }

    void Hit(RaycastHit hit) 
    {
        next = hit.point;
        onHit.Invoke(hit);
        CallFinishAction();
    }

    void Update()
    {
        if (frame > 0) 
        {
            transform.position = Vector3.MoveTowards(transform.position, next, Time.deltaTime * speed);
            transform.LookAt(transform.position + DropPoint());
            if (transform.position == next && isHitted) Hit(raycast);
        }
    }

    int frame = 0;
    void FixedUpdate()
    {
        if (!isIntialize) return;

        deltaSpeed = speed * Time.deltaTime;
        current = transform.position;
        last = current - transform.forward * deltaSpeed;
        end = next + transform.forward * deltaSpeed;


        if (frame > 0)
        {
            if (Physics.Raycast(last, transform.forward, out RaycastHit hit, deltaSpeed))
            {
                Hit(hit);
            }
            else if (Physics.Raycast(next, transform.forward, out RaycastHit hitNext, deltaSpeed))
            {
                next = hitNext.point;
                raycast = hitNext;
                isHitted = true;
            }
            else
            {
                next = current + transform.forward * deltaSpeed;
            }
        }
        else
        {
            if (Physics.Raycast(current, transform.forward, out RaycastHit hit, deltaSpeed))
            {
                Hit(hit);
            }
            else
            {
                next = current + transform.forward * deltaSpeed;
            }
        }
        
        frame += 1;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(last, 0.25f);
        Gizmos.DrawWireSphere(current, 0.25f);
        Gizmos.DrawWireSphere(next, 0.25f);
        Gizmos.DrawWireSphere(end, 0.25f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(last, current);
        Gizmos.DrawLine(current, next);
        Gizmos.DrawLine(next, end);
    }
}
