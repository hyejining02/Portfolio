using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{

    public float radius = 0.3f;

    public Color color = Color.red;

    public bool wireFrame = false;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;

        if ( wireFrame )
            Gizmos.DrawWireSphere(transform.position, radius);
        else
            Gizmos.DrawSphere(transform.position, radius);
    }

}
