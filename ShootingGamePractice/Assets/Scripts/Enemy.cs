using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class Enemy : LivingEntity
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        pathFinder = GetComponent<UnityEngine.AI.NavMeshAgent>();

        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while (target != null)
        {
            Vector3 targetPosition = target.position;

            if(dead == false)
            {
                pathFinder.SetDestination(targetPosition);
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }

    protected UnityEngine.AI.NavMeshAgent pathFinder;
    protected Transform target;
}
