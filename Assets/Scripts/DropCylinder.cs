using UnityEngine;

public class DropCylinder : MonoBehaviour
{
    public GameObject obstacle;
    private GameObject[] agent;

    // Start is called before the first frame update
    private void Start()
    {
        agent = GameObject.FindGameObjectsWithTag("agent");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
            {
                // Debug.Log(" Instantate game Object");
                Instantiate(obstacle, hitInfo.point, obstacle.transform.rotation);
                foreach (GameObject a in agent)
                {
                    a.GetComponent<AIControl>().DetectNewObstacle(hitInfo.point);
                }
            }
        }
    }
}
