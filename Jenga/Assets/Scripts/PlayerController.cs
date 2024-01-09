using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // Setting Variables
    [SerializeField] private float pieceSensitivity = 12f;
    [SerializeField] private float cameraSensitivity = 3f;

    // Events
    public UnityEvent OnTurnChange;

    // Variables for controlling pieces
    private JengaPiece selectedPiece;
    private Vector3 pieceMovingDirection;
    private float lastMouseY;

    // Variables for camera
    private float rotationY;



    void Update()
    {
        // Rotate camera by mouse when mouse's right click is being clicked
        if (Input.GetMouseButton(1))
        {
            rotationY += Input.GetAxis("Mouse X") * cameraSensitivity;
            transform.localEulerAngles = new Vector3(0, rotationY, 0);
        }
        
        // Controlling pieces
        if (Input.GetMouseButtonDown(0)) SelectPiece();
        if (Input.GetMouseButtonUp(0)) DeselectPiece();
        if (Input.GetMouseButton(0) && selectedPiece) MovePiece();
        
    }


    private void SelectPiece()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            selectedPiece = hit.transform.GetComponent<JengaPiece>();
            pieceMovingDirection = -hit.normal;
            print(hit.normal);
        }

        // Set lastMouse to mouse position so the piece just doesn't teleport away
        lastMouseY = Input.mousePosition.y;
    }

    private void MovePiece()
    {
        float mouseChange = Input.mousePosition.y - lastMouseY;
        lastMouseY = Input.mousePosition.y;
        Vector3 posChange = pieceMovingDirection * mouseChange / Screen.height * pieceSensitivity;

        selectedPiece.transform.position += posChange;

        // Removes the piece if it isn't colliding with any other piece. This also changes the turn
        if (!selectedPiece.IsColliding())
        {
            OnTurnChange.Invoke();

            // Clean
            Destroy(selectedPiece.gameObject);
            DeselectPiece();
        }
    }

    private void DeselectPiece()
    {
        selectedPiece = null;
    }
}
