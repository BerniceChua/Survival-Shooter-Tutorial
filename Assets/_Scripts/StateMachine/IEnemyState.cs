using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState {

    // Update is called once per frame
    void UpdateState();

    void OnTriggerEnter(Collider other);

    void ToPatrolState();

    void ToAlertState();

    void ToChaseState();

}