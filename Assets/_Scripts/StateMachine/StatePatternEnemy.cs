using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour {
    public float m_searchingTurnSpeed = 120.0f;
    public float m_searchingDuration = 4.0f;
    public float m_sightRange = 20.0f;
    public Transform[] m_waypoints;
    public Transform m_eyes; // the empty game object at eye level from the enemy, for the raycast to come from a sensible place
    public Vector3 m_offset = new Vector3(0.0f, 0.5f, 0.0f);
    public MeshRenderer m_meshRendererFlag; // the little icon on top of the enemy's head that tells us what state of alertness the enemy is in

    [SerializeField] SphereCollider m_triggerSphereCollider;

    [HideInInspector] public Transform m_chaseTarget;
    [HideInInspector] public IEnemyState m_currentState;
    [HideInInspector] public PatrolState m_patrolState;
    [HideInInspector] public AlertState m_alertState;
    [HideInInspector] public ChaseState m_chaseState;
    [HideInInspector] public NavMeshAgent m_navMeshAgent;

    private void Awake() {
        m_patrolState = new PatrolState(this);
        m_alertState = new AlertState(this);
        m_chaseState = new ChaseState(this);

        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start () {
        m_currentState = m_patrolState; // the first state that the enemy needs to be.
	}
	
	// Update is called once per frame
	void Update () { // to make this more performant, can use coroutine "IEnumerator()"
        m_currentState.UpdateState();
        // Because of the interface pattern, each of the classes have UpdateState(), so 
        // now every time we call every frame in StatePatternEnemy.cs, it will 
        // call UpdateState() on whichever state is the current state.  
        // So each UpdateState() will still be called even if we switch between PatrolState, AlertState, & ChaseState.
    }

    private void OnTriggerEnter(Collider other) {
        //if (this.GetComponent<Collider>() == m_triggerSphereCollider)
            m_currentState.OnTriggerEnter(other);
    }
}
