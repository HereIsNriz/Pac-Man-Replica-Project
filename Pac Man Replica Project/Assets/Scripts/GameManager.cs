using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //

    [SerializeField] private GameObject enemy;
    private int numOfEnemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        numOfEnemy = GameObject.FindObjectsOfType<EnemyController>().Length;

        if (numOfEnemy < 1)
        {
            Instantiate(enemy, transform.position, enemy.transform.rotation);
        }
    }
}
