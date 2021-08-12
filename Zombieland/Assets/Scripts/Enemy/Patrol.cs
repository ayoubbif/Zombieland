using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    private bool isPatroling = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-braking allows for continuous movement between points 
        agent.autoBraking = false;
        GotoNextPoint();
    }
    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (isPatroling)
        {
            if (!agent.pathPending && agent.remainingDistance < 2.0f)
                GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    public void StopPatroling()
    {
        isPatroling = false;
    }

}