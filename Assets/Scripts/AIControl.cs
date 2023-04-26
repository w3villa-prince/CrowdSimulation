using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour
{
    private GameObject[] goalLocations;
    private NavMeshAgent agent;
    private Animator anim;
    private float speedMult;
    private float detectionRadius = 20;
    private float fleeRadius = 10;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("goal");

        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
        anim = this.GetComponent<Animator>();

        anim.SetFloat("wOffset", Random.Range(0, 1));

        ResetAgent();

        // agent.SetDestination(goal.transform.position);
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, this.transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (this.transform.position - position).normalized;
            Vector3 newgoal = this.transform.position + fleeDirection * fleeRadius;
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newgoal, path);
            if (path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }

    private void ResetAgent()
    {
        speedMult = Random.Range(.5f, 2f);
        anim.SetFloat("speedMult", speedMult);
        agent.speed *= speedMult;
        anim.SetTrigger("isWalking");
        agent.ResetPath();
        agent.angularSpeed = 120;
    }

    private void Update()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            int i = Random.Range(0, goalLocations.Length);
            agent.SetDestination(goalLocations[i].transform.position);
        }
    }
}
