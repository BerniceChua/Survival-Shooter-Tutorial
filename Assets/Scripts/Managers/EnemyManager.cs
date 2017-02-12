using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    // old, non-pooling version
    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }

    //void Spawn() {

    //    if (playerHealth.currentHealth <= 0f) {
    //        return;
    //    }

    //    int spawnPointIndex = Random.Range(0, spawnPoints.Length);


    //    GameObject obj = ObjectPoolerScript.current.GetPooledGameObject();

    //    if (obj == null) return;

    //    obj.transform.position = transform.position;
    //    obj.transform.rotation = transform.rotation;
    //    obj.SetActive(true);
    //}
}
