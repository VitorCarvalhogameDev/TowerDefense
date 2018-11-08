using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoverHitPoint : MonoBehaviour , IInteractableplatform
{
    public PlataformMover thisMoverRef;

    // Use this for initialization
    void Start()
    {
        thisMoverRef = transform.parent.GetComponent<PlataformMover>();

    }

    public void MovePlatform()
    {
        thisMoverRef.MovePlatform();
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
