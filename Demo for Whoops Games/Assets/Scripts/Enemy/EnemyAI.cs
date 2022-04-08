using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyStatus
    {
        Idle,
        Escape,
        Dead
    }
    public EnemyStatus curretEnemyStatus = EnemyStatus.Idle;
    [SerializeField] float patrolRadius = 7f;
    Transform player;
    Animator anim;
    NavMeshAgent agent;
    bool isWalking, isEscape;
    int escapeOneShot;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        curretEnemyStatus = EnemyStatus.Idle;
    }
    void MakeIsWalkingFalse()
    {
        isWalking = false;
    }
    void Walk()
    {
        anim.SetBool("enemyWalk", true);
        agent.isStopped = false;
        isWalking = false;
        agent.SetDestination(GetRandomPosition());
    }
    void Escape()
    {

        agent.isStopped = false;
        isWalking = false;
        agent.SetDestination(GetRandomPosition());
    }
    private void Update()
    {
        switch (curretEnemyStatus)
        {
            case EnemyStatus.Idle:
                if (agent.remainingDistance <= 0.1f)
                {
                    
                    anim.SetBool("enemyWalk", false);
                }
                if (!isWalking && agent.remainingDistance <= 0.09f || agent.hasPath && !isWalking)
                {
                    Vector3 agentTarget = new Vector3(agent.destination.x, transform.position.y, agent.destination.z);
                    agent.enabled = false;
                    transform.position = agentTarget;
                    agent.enabled = true;
                    Walk();
                    isWalking = true;
                }
                break;
            case EnemyStatus.Escape:
                while (escapeOneShot == 0)
                {
                    anim.SetBool("enemyWalk", false);
                    anim.SetBool("enemyRun", true);
                    escapeOneShot++;
                }
                if (agent.remainingDistance <= 0.05f)
                {
                    isEscape = false;
                }
                if (!isEscape && agent.remainingDistance <= 0.1f || agent.hasPath && !isEscape)
                {
                    agent.speed = 10;
                    Escape();
                    isEscape = true;
                }
                break;
            case EnemyStatus.Dead:
                Dead();
                break;
        }
    }

    private void Dead()
    {

        agent.isStopped = true;
        anim.SetBool("enemyRun", false);
        anim.SetBool("enemyWalk", false);
        anim.SetBool("enemyDying", true);
        
    }
    Vector3 GetRandomPosition()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * patrolRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, 1);
        return hit.position;
    }
}
