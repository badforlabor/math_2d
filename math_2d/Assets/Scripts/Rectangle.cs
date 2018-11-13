using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rectangle : MonoBehaviour {

    public float width;
    public float height;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnDrawGizmos()
    {
        var p1 = new Vector3(-width / 2, -height / 2, 0);
        var p2 = p1 + new Vector3(width, 0, 0);
        var p3 = p2 + new Vector3(0, height, 0);
        var p4 = p1 + new Vector3(0, height, 0);

        p1 = this.transform.TransformPoint(p1);
        p2 = this.transform.TransformPoint(p2);
        p3 = this.transform.TransformPoint(p3);
        p4 = this.transform.TransformPoint(p4);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);

        Gizmos.DrawSphere(this.transform.position, 0.1f);
    }
}
