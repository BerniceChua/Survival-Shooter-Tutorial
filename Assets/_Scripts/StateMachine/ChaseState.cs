using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState {
    private readonly StatePatternEnemy m_enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy) {
        m_enemy = statePatternEnemy;
    }

    // Update is called once per frame
    public void UpdateState() {
        Look();
        Chase();
    }

    public void OnTriggerEnter(Collider other) {

    }

    public void ToPatrolState() {

    }

    public void ToAlertState() {
        m_enemy.m_currentState = m_enemy.m_alertState;
    }

    public void ToChaseState() {
        Debug.Log("Can't transition to the same state.");
    }

    private void Look() {
        RaycastHit hit;
        Vector3 enemyToTarget = (m_enemy.m_chaseTarget.position + m_enemy.m_offset) - m_enemy.m_eyes.transform.position;

        if (Physics.Raycast(m_enemy.m_eyes.transform.position, m_enemy.m_eyes.transform.forward, out hit, m_enemy.m_sightRange) && hit.collider.CompareTag("Player")) {
            m_enemy.m_chaseTarget = hit.transform;
        } else {
            ToAlertState();
        }
    }

    private void Chase() {
        m_enemy.m_meshRendererFlag.material.color = Color.red;
        m_enemy.m_navMeshAgent.destination = m_enemy.m_chaseTarget.position;
        m_enemy.m_navMeshAgent.Resume();
    }

}