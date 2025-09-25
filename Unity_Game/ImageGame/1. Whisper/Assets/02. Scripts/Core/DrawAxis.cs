using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAxis : MonoBehaviour
{

    public Color gizmoColor = Color.red;

    public float lineLength = 3;

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;

        Gizmos.DrawLine(transform.position - (Vector3.right * lineLength * 0.5f), transform.position + (Vector3.right * lineLength * 0.5f));

        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position - (Vector3.up * lineLength * 0.5f), transform.position + (Vector3.up * lineLength * 0.5f));
    }

}
