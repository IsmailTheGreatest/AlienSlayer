using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FloatingEffect : MonoBehaviour
{
    [Header("Scaling Settings")]
    [Tooltip("Duration of one full scaling cycle (scale up and down) in seconds.")]
    [SerializeField]
    private float scaleCycleDuration = 1f;

    [Tooltip("Maximum scale multiplier (e.g., 1.2 for 120%).")]
    [SerializeField]
    private float maxScaleMultiplier = 1.2f;

    [Header("Bouncing Settings")]
    [Tooltip("Amplitude of the bounce (how high the object bounces).")]
    [SerializeField]
    private float bounceAmplitude = 0.5f;

    [Tooltip("Frequency of the bounce (how fast the object bounces).")]
    [SerializeField]
    private float bounceFrequency = 2f;

    // Original position and scale
    private Vector3 originalPosition;
    private Vector3 originalScale;

    void Start()
    {
        // Ensure the Collider2D is set as a trigger
        Collider2D collider = GetComponent<Collider2D>();
        if (!collider.isTrigger)
        {
            Debug.LogWarning($"{gameObject.name}: Collider2D is not set as a trigger. Setting it to trigger.");
            collider.isTrigger = true;
        }

        // Store the original position and scale
        originalPosition = transform.position;
        originalScale = transform.localScale;

        // Start animation coroutines
        StartCoroutine(ScaleCycle());
        StartCoroutine(Bounce());
    }

    /// <summary>
    /// Coroutine to handle continuous scaling (pulsing) animation.
    /// </summary>
    private IEnumerator ScaleCycle()
    {
        Vector3 targetScale = originalScale * maxScaleMultiplier;
        float halfDuration = scaleCycleDuration / 2f;

        while (true)
        {
            // Scale Up
            yield return StartCoroutine(ScaleOverTime(transform.localScale, targetScale, halfDuration));

            // Scale Down
            yield return StartCoroutine(ScaleOverTime(targetScale, originalScale, halfDuration));
        }
    }

    /// <summary>
    /// Coroutine to smoothly scale the GameObject from startScale to endScale over duration.
    /// </summary>
    private IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = endScale;
    }

    /// <summary>
    /// Coroutine to handle continuous bouncing animation.
    /// </summary>
    private IEnumerator Bounce()
    {
        while (true)
        {
            // Calculate the next bounce duration based on frequency
            float bounceDuration = 1f / bounceFrequency;
            float elapsed = 0f;

            while (elapsed < bounceDuration)
            {
                // Calculate the new Y position using a sine wave for smooth up and down movement
                float newY = originalPosition.y + Mathf.Sin((elapsed / bounceDuration) * Mathf.PI * 2f) * bounceAmplitude;
                transform.position = new Vector3(originalPosition.x, newY, originalPosition.z);

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Ensure the object returns to its original position
            transform.position = originalPosition;
        }
    }
}
