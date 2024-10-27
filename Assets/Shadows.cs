using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Shadows : MonoBehaviour

{
    public Transform personaje;
    private NavMeshAgent agente;

    private void Awake ()
    {
        agente = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        agente.SetDestination(personaje.position);
    }
}