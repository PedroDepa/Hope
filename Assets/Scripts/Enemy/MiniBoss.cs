using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class MiniBoss : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float startWaitTime = 4f;
    [SerializeField] private float timeToRotate = 2f;
    [SerializeField] private float speedWalk = 6f;
    [SerializeField] private float speedRun = 9f;


    [SerializeField] private float viewRadius = 15f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private float meshResolution = 1f;
    [SerializeField] private int edgeIterations = 4;
    [SerializeField] private float edgeDistance = 0.5f;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;
    bool isWalking;

    //private Animator animator;
    

    private void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;
        isWalking = true;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
        //animator.SetBool("isWalking", isWalking);

        EnviromentView();
        
        if(!m_IsPatrol)
        {
            Chasing();

        }
        else
        {
            PatroLing();
        }
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);
        }
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;

                }
            }
        }
    }

    

   

    private void PatroLing()
    {
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);

            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;

                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }

    }

        void Move(float speed)
        {
            isWalking = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = speed;
        }

        void Stop()
        {
            isWalking = false;
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
        }

        void NextPoint()
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }

        void CaughtPlayer()
        {
            m_CaughtPlayer = true;
        }

        void LookingPlayer(Vector3 player)
        {
            navMeshAgent.SetDestination(player);
            if (Vector3.Distance(transform.position, player) <= 0.3)
            {
                if (m_WaitTime <= 0)
                {
                    m_PlayerNear = false;
                    Move(speedWalk);
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                    m_WaitTime = startWaitTime;
                    m_TimeToRotate = timeToRotate;

                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;

                }
            }
        }
        void EnviromentView()
        {
            Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
            for (int i = 0; i < playerInRange.Length; i++)
            {
                Transform player = playerInRange[i].transform;
                Vector3 dirToPlayer = (player.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
                {
                    float dsToPlayer = Vector3.Distance(transform.position, player.position);
                    if (!Physics.Raycast(transform.position, dirToPlayer, dsToPlayer, obstacleMask))
                    {
                        m_PlayerInRange = true;
                        m_IsPatrol = false;

                    }
                    else
                    {
                        m_PlayerInRange = false;
                    }
                }

                if (Vector3.Distance(transform.position, player.position) > viewRadius)
                {
                    m_PlayerInRange = false;
                }
                if (m_PlayerInRange)
                {
                    m_PlayerPosition = player.transform.position;
                }
            }

        }
    
}
