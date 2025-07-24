using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //
    
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private float speed;
    private Vector2 startPos;
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        enemyRb.velocity = transform.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.canAttackEnemy)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                transform.position = startPos;
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.Rotate(0, 0, -90);
        }
    }
}
