using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {
    public Transform target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (target != null)
        {
            Vector3 dir = target.position - transform.position;
            dir.Normalize();
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
	}
}
