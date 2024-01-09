using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    // Setting Variables
    [SerializeField] private float pieceSensitivity = 12f;
    [SerializeField] private float cameraSensitivity = 3f;
    [SerializeField] private float intensityWait = 5f;
    [SerializeField] private AnimationCurve intensityCurve;

    // Events
    public UnityEvent OnTurnChange;
    public UnityEvent OnPieceRemove;
    public UnityEvent<float> OnIntensityChange;

    // Variables for controlling pieces
    private JengaPiece selectedPiece;
    private Vector3 pieceMovingDirection;
    private float lastMouseY;

    // Private variables
    private float rotationY;
    private bool controlsEnabled = true;



    void Update()
    {
        // Rotate camera by mouse when mouse's right click is being clicked
        if (Input.GetMouseButton(1))
        {
            rotationY += Input.GetAxis("Mouse X") * cameraSensitivity;
            transform.localEulerAngles = new Vector3(0, rotationY, 0);
        }

        // Controlling pieces
        if (!controlsEnabled || GameManager.hasLost) return;
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
            StartCoroutine(ChangeTurnWhenStable());
            OnPieceRemove.Invoke();

            // Clean
            Destroy(selectedPiece.gameObject);
            DeselectPiece();
        }
    }

    private void DeselectPiece()
    {
        selectedPiece = null;
    }

    private IEnumerator ChangeTurnWhenStable()
    {
        controlsEnabled = false;
        float time = 0;
        while (time < intensityWait)
        {
            time += Time.deltaTime;
            OnIntensityChange.Invoke(intensityCurve.Evaluate(time / intensityWait));
            yield return new WaitForEndOfFrame();
        }

        OnTurnChange.Invoke();
        controlsEnabled = true;
    }
}
