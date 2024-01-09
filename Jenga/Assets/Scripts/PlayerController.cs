using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Setting Variables
    [SerializeField] private float pieceSensitivity = 12f;
    [SerializeField] private float cameraSensitivity = 3f;
    [SerializeField] private float pieceSize = 4f;

    // Variables for controlling pieces
    private Transform selectedPiece;
    private Vector3 startSelectedPos = Vector3.zero;
    private float startMouseY;

    // Variables for camera
    private float rotationX;
    private float rotationY;
    private Transform cameraTransform;


    void Start() { cameraTransform = transform.GetChild(0); }

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
        if (Input.GetMouseButton(0) && selectedPiece) MovePiece();
        if (Input.GetMouseButtonUp(0)) DeselectPiece();
    }


    private void SelectPiece()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100))
        {
            selectedPiece = hit.transform;
            print(selectedPiece.name);
        }

        // Set the starting positions for moving the piece
        startSelectedPos = selectedPiece.position;
        startMouseY = Input.mousePosition.y / Screen.height;
    }

    private void MovePiece()
    {
        float posChange = Input.mousePosition.y / Screen.height - startMouseY;
        selectedPiece.position = startSelectedPos + selectedPiece.right * posChange * pieceSensitivity;

        if (Vector3.Distance(startSelectedPos, selectedPiece.position) > pieceSize - 0.5f)
        {
            Destroy(selectedPiece.gameObject);
            DeselectPiece();
        }
    }

    private void DeselectPiece()
    {
        selectedPiece = null;
        startSelectedPos = Vector3.zero;
        startMouseY = 0;
    }
}
