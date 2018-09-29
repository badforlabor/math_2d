using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

    public Transform p0;
    public Transform p1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        if (p0 != null && p1 != null)
        {
            Gizmos.DrawLine(p0.position, p1.position);
        }
    }
}
