using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float Speed = 8f;
    [SerializeField] private float Damp = 400f;
    [SerializeField] private float JumpForce = 600f;

    [Header("Detecting if player is on the ground")]
    [SerializeField] private Vector2 GroundDetectorSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private float GroundDetectorOffset = 0.05f; // How much distance in y is added to detector's position. 0 = collides with player
    [SerializeField] private bool DrawGizmos = false;

    // Player's Components
    private Rigidbody2D rb;
    private Collider2D col;



    private void Start()
    {
        // Get Player's components
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        // Smooth horizontal movement. It's based on adding force and removing by velocity.
        // Speed variable is basicly the speed of the player, and the max speed.
        // Damp is the speed of how fastly player reaches max speed, and how fastly it slows down to 0.
        float horizontal = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector3.right * (horizontal * Speed - rb.velocity.x) * Damp * Time.deltaTime);

        
        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            // Create an overlapbox, and detect if touching ground.
            Collider2D result = Physics2D.OverlapBox(CalculateGroundDetectorPos(), GroundDetectorSize, 0);

            // If ground detected -> Jump
            if (result != null) rb.AddForce(Vector2.up * JumpForce);
        }
    }

    private void OnDrawGizmos()
    {
        if (!DrawGizmos || col == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(CalculateGroundDetectorPos(), GroundDetectorSize);
    }


    
    // Calculates the position for the ground detector area. This is used in detecting jump and drawing the gizmos.
    private Vector3 CalculateGroundDetectorPos()
    {
        // Position - 50% of collider's height - 50% of GroundDetectorArea's height - Offset
        return transform.position - new Vector3(0, col.bounds.size.y * 0.5f + GroundDetectorSize.y * 0.5f + GroundDetectorOffset);
    }
}
