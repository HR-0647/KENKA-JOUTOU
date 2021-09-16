using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    GameObject[] enemyObjects;
    int enemyNum;

    void Start()
    {
        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        enemyNum = enemyObjects.Length;
        Debug.Log(enemyObjects.Length);
    }
}
