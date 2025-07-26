using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //
    
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private AudioSource enemyDeadSound;
    [SerializeField] private float speed;
    private Vector2 startPos;
    private PlayerController player;
    private GameManager gameManager;
    private int scoreFromEnemy = 50;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        player = GameObject.Find("Player").GetComponent<PlayerController>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (gameManager.gameRunning)
        {
            EnemyMovement();
        }
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
                enemyDeadSound.PlayOneShot(enemyDeadSound.clip);
                transform.position = startPos;
                gameManager.UpdateScore(scoreFromEnemy);
            }
        }

        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            float enemyRotateAmount = Random.Range(0, 2) == 0 ? 90 : -90;

            transform.Rotate(0, 0, enemyRotateAmount);
        }
    }
}
