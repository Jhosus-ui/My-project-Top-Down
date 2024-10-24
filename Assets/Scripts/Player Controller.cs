using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Rigidbody2D LeonRb;
    private Vector2 moveInput;
    private Animator animator;

    // Singleton para evitar duplicados
    private static PlayerController instance;

    void Awake()
    {
        // Comprobar si ya existe una instancia del personaje
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir el objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados
        }
    }

    void Start()
    {
        LeonRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Referencia al Animator

        // Comprobar si hay un spawn guardado en PlayerPrefs
        if (PlayerPrefs.HasKey("SpawnPoint"))
        {
            // Buscar el objeto que tiene el tag guardado
            string spawnPointTag = PlayerPrefs.GetString("SpawnPoint");
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(spawnPointTag);

            if (spawnPoint != null)
            {
                // Mover al jugador al punto de spawn
                transform.position = spawnPoint.transform.position;

                // Eliminar la clave del spawn point después de usarla
                PlayerPrefs.DeleteKey("SpawnPoint");
            }
        }
        else
        {
            // Si no hay un punto de spawn guardado, el jugador comienza en el punto inicial predeterminado.
        }
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveInput = new Vector2(moveX, moveY).normalized;

        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
        animator.SetFloat("Speed", moveInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        LeonRb.MovePosition(LeonRb.position + moveInput * speed * Time.fixedDeltaTime);
    }
}
