using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Variables para sonidos
    public AudioClip walkingSound;   // Sonido al caminar
    public AudioClip runningSound;   // Sonido al correr
    public AudioClip recoverySound;  // Sonido de recuperación
    private AudioSource audioSource;

    // Rango de velocidades para caminar, correr, y recuperación
    public float recoverySpeed = 5f;  // Velocidad de recuperación
    private bool isRecovering = false;


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
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Guardar la posición inicial en PlayerPrefs
        PlayerPrefs.SetFloat("InitialPositionX", transform.position.x);
        PlayerPrefs.SetFloat("InitialPositionY", transform.position.y);

        // Comprobar si hay un spawn guardado
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

            HandleFootstepSounds(); // Llamada para manejar los sonidos de pasos
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); // Detener sonido si el personaje se detiene
            }
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
        isRecovering = true; // Entra en fase de recuperación

        yield return new WaitForSeconds(sprintCooldown);
        canSprint = true; // Reiniciar la habilidad de sprintar
        isRecovering = false; // Recuperación completada
    }

    // Manejar sonidos de pasos y correr
    private void HandleFootstepSounds()
    {
        float currentSpeed = moveInput.magnitude;  // Obtener la velocidad actual del personaje

        if (isRecovering)
        {
            PlayFootstepSound(recoverySound); // Sonido de recuperación
        }
        else if (isSprinting)
        {
            PlayFootstepSound(runningSound); // Sonido de correr
        }
        else if (currentSpeed > 0 && !isSprinting) // Está caminando
        {
            PlayFootstepSound(walkingSound); // Sonido de caminar
        }
        else if (currentSpeed == 0 && audioSource.isPlaying)
        {
            audioSource.Stop(); // Detener el sonido si no hay movimiento
        }
    }

    // Función para reproducir el sonido adecuado
    private void PlayFootstepSound(AudioClip soundClip)
    {
        if (audioSource.clip != soundClip)  // Si el sonido actual no es el que se debe reproducir
        {
            audioSource.clip = soundClip;   // Asignar el nuevo sonido
            audioSource.Play();             // Reproducir el sonido
        }
        else if (!audioSource.isPlaying)    // Si el sonido correcto no está reproduciéndose
        {
            audioSource.Play();             // Reproducirlo
        }
    }

    // Funciones para manejar llaves
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Llamar a la función que maneja el Game Over
            GameOver();
        }
    }

    private void GameOver()
    {
        // Cargar la escena de Game Over
        SceneManager.LoadScene("Jumpscare"); // Asegúrate de que el nombre sea correcto
    }

    public void ResetPlayerPosition()
    {
        // Establecer la posición inicial del jugador
        transform.position = new Vector2(PlayerPrefs.GetFloat("InitialPositionX"), PlayerPrefs.GetFloat("InitialPositionY"));
    }

}
