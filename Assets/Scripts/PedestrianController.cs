using UnityEngine;
using UnityEngine.AI;

public class PedestrianController : MonoBehaviour
{
    [SerializeField] private float chooseDestinationChance = 0.01f; // 1%
    
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(Random.Range(0.0f, 1.0f) < chooseDestinationChance)
        {
            Vector3 pos = transform.position;
            navMeshAgent.SetDestination(new(
                pos.x + Random.Range(-100.0f, 100.0f), pos.y + Random.Range(-100.0f, 100.0f), pos.z + Random.Range(-100.0f, 100.0f)
            ));
        }
    }
}
