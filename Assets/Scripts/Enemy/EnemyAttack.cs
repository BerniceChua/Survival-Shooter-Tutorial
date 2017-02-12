using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    [SerializeField] SphereCollider m_attackTrigger;

    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

    private void OnCollisionStay(Collision collision) {
        if(collision.gameObject == player)
        {
            playerInRange = true;
        }
    }
    // original of this was:
    //private void OnTriggerEnter(Collider other) {
    //    if (other.gameObject == player) {
    //        playerInRange = true;
    //    }
    //}
    // changed this from "OnTriggerEnter" to "OnCollisionStay" so that it can
    // have 2 triggers: the bigger sphere collider triggers its alertness, while
    // the smaller sphere collider (this one) triggers the enemy to damage the player.

    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}