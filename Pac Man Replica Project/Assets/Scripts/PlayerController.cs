using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //

    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D playerRb;
    private float playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        
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
}
