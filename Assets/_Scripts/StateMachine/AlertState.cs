using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : IEnemyState {
    private float m_searchTimer;
    private readonly StatePatternEnemy m_enemy;

    public AlertState(StatePatternEnemy statePatternEnemy) {
        m_enemy = statePatternEnemy;
    }

	// Update is called once per frame
	public void UpdateState () {
        Look();
        Search();
	}

    public void OnTriggerEnter(Collider other) {

    }

    public void ToPatrolState() {
        m_enemy.m_currentState = m_enemy.m_patrolState;
        m_searchTimer = 0.0f;
    }

    public void ToAlertState() {
        Debug.Log("Can't transition to the same state.");
    }

    public void ToChaseState() {
        m_enemy.m_currentState = m_enemy.m_chaseState;
        m_searchTimer = 0.0f;
    }

    private void Look() {
        RaycastHit hit;

        if (Physics.Raycast(m_enemy.m_eyes.transform.position, m_enemy.m_eyes.transform.forward, out hit, m_enemy.m_sightRange) && hit.collider.CompareTag("Player")) {
            m_enemy.m_chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    private void Search() {
        m_enemy.m_meshRendererFlag.material.color = Color.yellow;
        m_enemy.m_navMeshAgent.Stop();
        m_enemy.transform.Rotate(0, m_enemy.m_searchingTurnSpeed * Time.deltaTime, 0); // rotates around the y-axis on each frame.
        m_searchTimer += Time.deltaTime;

        // check if remaining distance is less than stopping distance, AND do we no longer have more path that we're trying to walk?
        if (m_searchTimer >= m_enemy.m_searchingDuration) {
            ToPatrolState();
        }
    }

}