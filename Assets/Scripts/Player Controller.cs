using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // Velocidad normal
    [SerializeField] private float sprintSpeed = 4.75f; // Velocidad al sprintar
    private Rigidbody2D LeonRb;
    private Vector2 moveInput;
    private Animator animator;
    public bool canMove = true;

    // Variables para el sprint
    private bool isSprinting = false;
    [SerializeField] private float sprintDuration = 2f; // Duración del sprint
    [SerializeField] private float sprintCooldown = 10f; // Tiempo de recuperación antes de volver a sprintar
    private float sprintTimer;
    private bool canSprint = true;

    // Singleton para evitar duplicados
    public static PlayerController instance;

    private void Awake()
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

        // Posición inicial del jugador
        transform.position = new Vector2(4.16f, -0.84f); // Coordenadas iniciales

        // Comprobar si hay un spawn guardado en PlayerPrefs
        if (PlayerPrefs.HasKey("SpawnPoint"))
        {
            string spawnPointTag = PlayerPrefs.GetString("SpawnPoint");
            GameObject spawnPoint = GameObject.FindGameObjectWithTag(spawnPointTag);

            if (spawnPoint != null)
            {
                transform.position = spawnPoint.transform.position;
                PlayerPrefs.DeleteKey("SpawnPoint");
            }
        }
    }

    void Update()
    {
        if (canMove)
        {
            // Manejo de movimiento
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");
            moveInput = new Vector2(moveX, moveY).normalized;

            // Activar sprint
            if (Input.GetKeyDown(KeyCode.LeftShift) && canSprint)
            {
                StartCoroutine(Sprint());
            }

            animator.SetFloat("MoveX", moveX);
            animator.SetFloat("MoveY", moveY);
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            // Ajustar velocidad según si se está sprintando o no
            float currentSpeed = isSprinting ? sprintSpeed : speed;
            LeonRb.MovePosition(LeonRb.position + moveInput * currentSpeed * Time.fixedDeltaTime);
        }
    }

    

    private IEnumerator Sprint()
    {
        isSprinting = true;
        canSprint = false;

        yield return new WaitForSeconds(sprintDuration);

        isSprinting = false;
        yield return new WaitForSeconds(sprintCooldown);
        canSprint = true; // Reiniciar la habilidad de sprintar
    }



    private List<string> keys = new List<string>(); // Lista para almacenar las llaves del jugador

    public void AddKey(string keyID)
    {
        if (!keys.Contains(keyID)) // Evita duplicados
        {
            keys.Add(keyID);
            Debug.Log("Llave añadida: " + keyID);
        }
    }

    public bool HasKey(string keyID)
    {
        return keys.Contains(keyID); // Devuelve true si el jugador tiene la llave
    }

}
