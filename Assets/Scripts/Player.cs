using System;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : NetworkBehaviour
{
    public string playerName;
    public int health;
    [SerializeField] Camera playerCamera; 
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float jumpVaue = 5f;
    [SerializeField] private LayerMask ground;

    Rigidbody rigidbody;
    bool isGrounded;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    
    private void OnEnable()
    {
        Transform spawnPoint = Singleton<SpawnManager>.Instance.GetRandomSpawnPoint();
        if (spawnPoint == null) { return; }
        transform.position = spawnPoint.position;
    }

    public void Initialize()
    {
        playerName = "Player" + Random.Range(1, 1000).ToString();
        health = 100;  
    }
    private void Start()
    {
        if (IsOwner)
        {
            if (playerCamera == null) { return;}
            playerCamera.enabled = true;
        }
        else
        {
            if (playerCamera == null) { return;}
            playerCamera.enabled = false;
        }
    }
    
    private void Update()
    {
        if (!IsOwner) { return;}
        Move();
        Jump();
    }
    
    public void Spawn(Transform spawnPoint)
    {
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation; 
    }
    
    private void Move()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        Vector3 value = new Vector3(xMovement, 0, yMovement) * (speed * Time.deltaTime);
        transform.Translate(value, Space.World);
    }
    
    private void Jump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, ground);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.AddForce(Vector3.up * jumpVaue, ForceMode.Impulse);
        }
    }
}