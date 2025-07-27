using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public bool canAttackEnemy;

    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private AudioSource collectMoneySound;
    [SerializeField] private AudioSource collectCoinSound;
    [SerializeField] private AudioSource playerDeadSound;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private float speed;
    private GameManager gameManager;
    private Vector2 startingPos;
    private float playerRotation;
    private float enemyCooldown = 5.0f;
    private int scoreFromMoney = 25;
    private int lives = 3;
    private int livesSubstracted = 1;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        livesText.text = $"Lives: {lives}";

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
            PlayerMovement();
        }
        else
        {
            speed = 0;
            playerRb.velocity = Vector3.zero;
        }
    }

    private void PlayerMovement()
    {
        playerRb.velocity = transform.right * speed * Time.deltaTime;

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 playerRotate = new Vector2(horizontalInput, verticalInput).normalized;

        if (playerRotate != Vector2.zero)
        {
            playerRotation = Mathf.Atan2(playerRotate.y, playerRotate.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, playerRotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Money"))
        {
            collectMoneySound.PlayOneShot(collectMoneySound.clip);
            Destroy(collision.gameObject);
            gameManager.UpdateScore(scoreFromMoney);
            canAttackEnemy = true;
            StartCoroutine(EnemyCoundownRoutine());
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            collectCoinSound.PlayOneShot(collectCoinSound.clip);
        }
    }

    IEnumerator EnemyCoundownRoutine()
    {
        yield return new WaitForSeconds(enemyCooldown);
        canAttackEnemy= false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canAttackEnemy)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                playerDeadSound.PlayOneShot(playerDeadSound.clip);
                transform.position = startingPos;
                transform.rotation = Quaternion.identity;
                SubstractLives(livesSubstracted);
            }
        }
    }

    private void SubstractLives(int livesToSubstract)
    {
        if (lives > 0)
        {
            lives -= livesToSubstract;
            livesText.text = $"Lives: {lives}";
        }

        if (lives == 0)
        {
            lives = 0;
            gameManager.GameOver();
        }
    }
}
