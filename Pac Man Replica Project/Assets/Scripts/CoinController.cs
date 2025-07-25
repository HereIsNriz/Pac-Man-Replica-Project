using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    //

    [SerializeField] private float rotationSpeed;
    private GameManager gameManager;
    private int scoreFromCoin = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameRunning)
        {
            RotateCoin();
        }
    }

    private void RotateCoin()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.UpdateScore(scoreFromCoin);
        }
    }
}
