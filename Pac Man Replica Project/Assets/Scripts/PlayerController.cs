using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canAttackEnemy;

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D playerRb;
    private GameManager gameManager;
    private float playerRotation;
    private float enemyCooldown = 5.0f;
    private int scoreFromMoney = 25;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerMovement();
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
            Destroy(collision.gameObject);
            gameManager.UpdateScore(scoreFromMoney);
            canAttackEnemy = true;
            StartCoroutine(EnemyCoundownRoutine());
        }
    }

    IEnumerator EnemyCoundownRoutine()
    {
        yield return new WaitForSeconds(enemyCooldown);
        canAttackEnemy= false;
    }
}
