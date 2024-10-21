using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    
    private Rigidbody2D Leon;
    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        Leon = GetComponent<Rigidbody2D>();
      

    }

    // Update is called once per frame
    void Update()
    {
        float velocidadX = Input.GetAxisRaw("Horizontal");



        float velocidadY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(velocidadX, velocidadY).normalized;






    }

    private void FixedUpdate()

    {
        Leon.MovePosition(Leon.position + moveInput * speed * Time.fixedDeltaTime);
    
    }
}