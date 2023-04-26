using UnityEngine;
using UnityEngine.AI;

public class CrowdAIController : MonoBehaviour
{
    private GameObject[] goalLocations;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("goal");
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
