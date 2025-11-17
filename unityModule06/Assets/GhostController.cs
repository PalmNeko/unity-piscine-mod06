using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class GhostController : MonoBehaviour
{
    public Transform target;
    public Animator animator;
    public float detectRange = 3.0f;
    private NavMeshAgent agent;
    private Vector3 initialPosition;
    private float returnTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        initialPosition = transform.position;
        returnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldTrace())
        {
            agent.SetDestination(target.position);
            returnTime = Time.time + 3.0f;
        }
        else
        {
            if (Time.time > returnTime)
                agent.SetDestination(initialPosition);
        }
        if (agent.velocity.magnitude != 0f)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    void OnAnimatorMove()
    {
        if (animator.GetBool("Walking"))
        {
            agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }
    }

    // ターゲットとの距離、壁があれば、無限
    private bool ShouldTrace()
    {
        RaycastHit hitInfo = new();
        Vector3 direct = target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direct);
        if (angle <= 45f && Physics.Raycast(transform.position, direct, out hitInfo, detectRange))
        {
            if ((hitInfo.point - target.position).magnitude < 0.5f)
                return true;
        }
        return false;
    }
}
