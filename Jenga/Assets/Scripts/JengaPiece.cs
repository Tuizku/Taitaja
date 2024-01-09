using JetBrains.Annotations;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaPiece : MonoBehaviour
{
    static List<Rigidbody> allPieceBodies = new();
    

    private void OnEnable()
    {
        allPieceBodies.Add(GetComponent<Rigidbody>());
    }

    private void OnDisable()
    {
        allPieceBodies.Remove(GetComponent<Rigidbody>());
    }

    public bool IsColliding()
    {
        Vector3 scale = new Vector3(transform.localScale.x / 2.5f, transform.localScale.y / 1.95f, transform.localScale.z / 2.5f);
        Collider[] hits = Physics.OverlapBox(transform.position, scale, transform.rotation);
        if (hits.Length <= 1) return false;
        else return true;
    }

    public static int GetTowerActivePieces()
    {
        int activeAmount = 0;
        
        for (int i = 0; i < allPieceBodies.Count; i++)
        {
            if (allPieceBodies[i].velocity.magnitude > 0.2f) activeAmount++;
        }
        print(activeAmount);
        return activeAmount;
    }
}
