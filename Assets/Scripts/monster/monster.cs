using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : Physics2DHandler
{
    private Transform player; // Reference to the player's transform
    public float speed = 2.0f; // Speed of the monster
    public MonsterSpawner spawner; // Reference to the spawner (set when the monster is spawned)

    private Vector3 previousPosition;
    private int framesWithoutMovement = 0;
    private const int MaxFramesStuck = 10;
    private bool movingHorizontally = true;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); // Find the player
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Ensure the player GameObject is tagged as 'Player'.");
        }

        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            // If player is not found, do not attempt to move towards them
            return;
        }

        // Manhattan movement towards the player
        Vector3 direction = player.position - transform.position;
        Vector2 velocity = Vector2.zero;

        if (movingHorizontally)
        {
            velocity = new Vector2(Mathf.Sign(direction.x), 0) * speed;
        }
        else
        {
            velocity = new Vector2(0, Mathf.Sign(direction.y)) * speed;
        }

        // Apply movement with null check for Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = velocity;
        }
        else
        {
            Debug.LogWarning("Rigidbody2D component is missing on Monster.");
        }

        // Check for movement
        if (transform.position == previousPosition)
        {
            framesWithoutMovement++;
            if (framesWithoutMovement >= MaxFramesStuck)
            {
                movingHorizontally = !movingHorizontally; // Change direction if stuck
                framesWithoutMovement = 0;
                Debug.Log("Monster is stuck. Changing movement direction to escape.");
            }
        }
        else
        {
            framesWithoutMovement = 0;
        }

        previousPosition = transform.position;
    }

    // Handle collision with the player and bullets
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            spawner.OnMonsterDestroyed();

            Destroy(gameObject);
            Debug.Log("Monster destroyed by bullet.");
            return; // Exit to prevent further collision handling
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            HealthSliderController health = collision.gameObject.GetComponent<HealthSliderController>();
            if (health != null)
            {
                health.TakeDamage(15f);
                Debug.Log("Player health decreased by 15%");
            }
            else
            {
                Debug.LogWarning("HealthSliderController component not found on Player.");
            }
            
        }

        if (collision.gameObject.CompareTag("EnvironmentTrigger"))
        {
            if (collision.gameObject.TryGetComponent(out EnvironmentObjectTrigger trigger))
            {
                trigger.SetActiveObjects();
            }
        }
    }

    protected override void Awake()
        {
            base.Awake();
        }
}
