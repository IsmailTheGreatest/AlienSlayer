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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
        previousPosition = transform.position;
    }

    private void Update()
    {
        // Manhattan movement towards the player
        Vector3 direction = player.position - transform.position;
        Vector2 velocity = Vector2.zero;

        if (movingHorizontally)
        {
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                velocity = new Vector2(Mathf.Sign(direction.x) * speed, 0);
            }
            else
            {
                movingHorizontally = false;
                velocity = new Vector2(0, Mathf.Sign(direction.y) * speed);
            }
        }
        else
        {
            if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                velocity = new Vector2(0, Mathf.Sign(direction.y) * speed);
            }
            else
            {
                movingHorizontally = true;
                velocity = new Vector2(Mathf.Sign(direction.x) * speed, 0);
            }
        }

        SetVelocity(velocity);

        // Check if the monster is stuck
        if (Vector3.Distance(transform.position, previousPosition) < 0.01f)
        {
            framesWithoutMovement++;
            if (framesWithoutMovement >= MaxFramesStuck)
            {
                movingHorizontally = !movingHorizontally;
                framesWithoutMovement = 0;
            }
        }
        else
        {
            framesWithoutMovement = 0;
        }

        previousPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") || collision.CompareTag("Player"))
        {
            // Notify the spawner before destroying the monster
            spawner.OnMonsterDestroyed();
            Destroy(gameObject); // Destroy the monster
        }

        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // Destroy the bullet
        }

        if (collision.CompareTag("EnvironmentTrigger"))
        {
            if (collision.TryGetComponent(out EnvironmentObjectTrigger trigger))
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
