using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Vector2 initialPosition; 
    public Transform[] patrolPoints; 
    public float detectionRange = 5f;  
    public float loseRange = 10f;      
    public float speed = 2f;          


    private int currentPatrolIndex = 0; 
    private bool isPatrolling = true;   
    private bool movingForward = true;  

    private void Start() // Movement 
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("No se encontró el jugador en esta escena.");
            return;
        }

        
        transform.position = initialPosition;

        isPatrolling = true;
        currentPatrolIndex = 0;
    }

    private void Update()
    {
        if (player == null) return; 

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        
        if (distanceToPlayer <= detectionRange)
        {
            isPatrolling = false;
            ChasePlayer();
        }
        
        else if (distanceToPlayer > loseRange)
        {
            isPatrolling = true;
            ReturnToPatrol();
        }

        
        if (isPatrolling)
        {
            Patrol();
        }
    }

    private void OnEnable()
    {
        
        if (patrolPoints.Length > 0)
        {
            transform.position = patrolPoints[0].position;
        }
        else
        {
            transform.position = initialPosition;  
        }

        isPatrolling = true;
        currentPatrolIndex = 0;
        movingForward = true; 
    }

    
    private void Patrol()  ///Patrullaje
    {
        if (patrolPoints.Length == 0) return;  

        Transform targetPoint = patrolPoints[currentPatrolIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            
            if (movingForward)
            {
                
                if (currentPatrolIndex >= patrolPoints.Length - 1)
                {
                    movingForward = false;
                }
                else
                {
                    currentPatrolIndex++;  
                }
            }
            
            else
            {
                
                if (currentPatrolIndex <= 0)
                {
                    movingForward = true;
                }
                else
                {
                    currentPatrolIndex--;
                }
            }
        }
    }

   
    private void ChasePlayer()  //Regresas
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    
    private void ReturnToPatrol()
    {
        Patrol();
    }
}
