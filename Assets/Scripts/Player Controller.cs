using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    private Rigidbody2D LeonRb;
    private Vector2 moveInput;

    // Referencia al Animator para controlar las animaciones
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        LeonRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Referencia al Animator
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);


    }

    private void FixedUpdate()
    {
        LeonRb.MovePosition(LeonRb.position + moveInput * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if colliding with a game object with specific tag
        if (collision.gameObject.tag.Equals("door1"))
        {
            Debug.Log("change scene");
            SceneManager.LoadScene(1);

        }
    }
}