using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour {

    public float Radius = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
    }
    void OnDrawGizmos()
    {
        if (Radius > 0)
        {
            Gizmos.DrawSphere(this.transform.position, Radius);
        }
    }
}
