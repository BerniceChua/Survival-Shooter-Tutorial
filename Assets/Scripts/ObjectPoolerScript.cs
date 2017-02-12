using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerScript : MonoBehaviour {

    public static ObjectPoolerScript current;
    public GameObject pooledGameObject;
    public int pooledAmount = 20;
    public bool willGrow = true;

    List<GameObject> pooledGameObjects;

    void Awake() {
        current = this;
    }

    // Use this for initialization
    void Start() {
        pooledGameObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++) {
            GameObject obj = (GameObject)Instantiate(pooledGameObject);
            obj.SetActive(false);
            pooledGameObjects.Add(obj);
        }
    }

    public GameObject GetPooledGameObject() {
        for (int i = 0; i < pooledGameObjects.Count; i++) {
            if (!pooledGameObjects[i].activeInHierarchy) {
                return pooledGameObjects[i];
            }
        }

        if (willGrow) {
            GameObject obj = (GameObject)Instantiate(pooledGameObject);
            pooledGameObjects.Add(obj);
            return obj;
        }

        return null;
    }

}