using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JengaPiece : MonoBehaviour
{
    public Vector3 startPos;
    

    void Start()
    {
        startPos = transform.position;
    }

    public bool IsColliding()
    {
        Vector3 scale = new Vector3(transform.localScale.x / 2.3f, transform.localScale.y / 1.9f, transform.localScale.z / 2.3f);
        Collider[] hits = Physics.OverlapBox(transform.position, scale, transform.rotation);
        if (hits.Length <= 1) return false;
        else return true;
    }
}
