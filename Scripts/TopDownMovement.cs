using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 6f;
    [SerializeField] private float Damp = 0.6f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get moving inputs as raw
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Smooth movement with normalized inputs and damping
        Vector2 normalizedInputs = new Vector2(horizontal, vertical).normalized;
        rb.AddForce(normalizedInputs * Speed * Damp - rb.velocity * Damp);
    }
}
