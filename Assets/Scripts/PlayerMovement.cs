using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int controlScheme;

    private Rigidbody2D rb;

    public Camera mainCamera;

    private GameControllerScript gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
        rb = GetComponent<Rigidbody2D>();

        controlScheme = StaticVariables.controlScheme;
    }

    void Update()
    {
        if(!gc.gameOver && controlScheme == 0)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * gc.playerSpeed, Input.GetAxis("Vertical") * gc.playerSpeed);
            transform.up = ((Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position);
        }

        if (!gc.gameOver && controlScheme == 1)
        {
            rb.velocity = transform.up * gc.playerSpeed * Input.GetAxis("Vertical");
            transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - (Input.GetAxisRaw("Horizontal") * gc.playerRotationSpeed * Time.deltaTime));
            rb.angularVelocity = 0;
        }

        if (gc.gameOver)
        {
            rb.velocity = new Vector2(0f, 0f);
            rb.angularVelocity = 0;
        }
    }
}
