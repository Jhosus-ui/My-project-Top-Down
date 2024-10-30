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
    [SerializeField] private float sprintDuration = 2f; // Duración 
    [SerializeField] private float sprintCooldown = 10f; // Tiempo 
    private float sprintTimer;
    private bool canSprint = true;

    // Singleton para evitar duplicados
    public static PlayerController instance;

    // Variables para sonidos
    public AudioClip walkingSound;   
    public AudioClip runningSound;   
    public AudioClip recoverySound; 
    private AudioSource audioSource;

   
    public float recoverySpeed = 5f;  // Velocidad de recuperación
    private bool isRecovering = false;


    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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

            HandleFootstepSounds(); 
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            if (audioSource.isPlaying)
            {
                audioSource.Stop(); 
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
            audioSource.Stop(); //Detener
        }
    }

    
    private void PlayFootstepSound(AudioClip soundClip)
    {
        if (audioSource.clip != soundClip)  
        {
            audioSource.clip = soundClip;  
            audioSource.Play();             
        }
        else if (!audioSource.isPlaying)    
        {
            audioSource.Play();             
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
        return keys.Contains(keyID); 
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
        SceneManager.LoadScene("Jumpscare"); 
    }

    public void ResetPlayerPosition()
    {
        // Establecer la posición inicial del jugador
        transform.position = new Vector2(PlayerPrefs.GetFloat("InitialPositionX"), PlayerPrefs.GetFloat("InitialPositionY"));
    }

}
