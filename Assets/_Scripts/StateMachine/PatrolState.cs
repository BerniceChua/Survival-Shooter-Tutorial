using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState {
    private int m_nextWayPoint;
    private readonly StatePatternEnemy m_enemy;

    public PatrolState(StatePatternEnemy statePatternEnemy) {
        m_enemy = statePatternEnemy;
    }

	// Update is called once per frame
	public void UpdateState() {
        Look();
        Patrol();
	}

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
            ToAlertState();
    }

    public void ToPatrolState() {
        Debug.Log("Can't transition to the same state.");
    }

    public void ToAlertState() {
        m_enemy.m_currentState = m_enemy.m_alertState;
    }

    public void ToChaseState() {
        m_enemy.m_currentState = m_enemy.m_chaseState;
    }

    private void Look() {
        RaycastHit hit;

        if (Physics.Raycast(m_enemy.m_eyes.transform.position, m_enemy.m_eyes.transform.forward, out hit, m_enemy.m_sightRange) && hit.collider.CompareTag("Player")) {
            m_enemy.m_chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    void Patrol() {
        m_enemy.m_meshRendererFlag.material.color = Color.green;
        m_enemy.m_navMeshAgent.destination = m_enemy.m_waypoints[m_nextWayPoint].position;
        m_enemy.m_navMeshAgent.Resume();

        // check if remaining distance is less than stopping distance, AND do we no longer have more path that we're trying to walk?
        if (m_enemy.m_navMeshAgent.remainingDistance <= m_enemy.m_navMeshAgent.stoppingDistance && !m_enemy.m_navMeshAgent.pathPending) {
            m_nextWayPoint = (m_nextWayPoint + 1) % m_enemy.m_waypoints.Length;
        }
    }

}