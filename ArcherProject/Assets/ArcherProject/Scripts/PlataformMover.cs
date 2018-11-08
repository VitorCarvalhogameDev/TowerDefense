using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMover : MonoBehaviour
{
    public List<Transform> WayPoints;

    public Transform movingPlatform;
    public Animator platformAnimator;

    public GameObject platFormTrigger1, platFormTrigger2;

    public bool GoingForward;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    public void MovePlatform()
    {
        if (!GoingForward)
        {
            GoingForward = true;
            platformAnimator.SetBool("Moving", true);
            StartCoroutine(MovePlatformForward());
        }
        else
        {
            GoingForward = false;
            platformAnimator.SetBool("Moving", true);
            StartCoroutine(MovePlatformBackward());
        }
    }

    public IEnumerator MovePlatformForward()
    {

        // GameManager.instance.SetAllTriggersOff();
        platFormTrigger1.SetActive(false);
        platFormTrigger2.SetActive(false);
        
        yield return new WaitForSeconds(1);

        // get the platform layer
        int layerMask = 1 << 10;
        RaycastHit hit;
        if (Physics.Raycast(GameManager.instance.CameraEye.position, Vector3.down, out hit, 5, layerMask))
        {
            if (hit.transform == movingPlatform)
            {
                GameManager.instance.CameraRig.parent = null;
                Debug.Log("On top Of Platform!");
                GameManager.instance.CameraRig.parent = movingPlatform;
            }
        }
       

        for (int i = 0; i < WayPoints.Count; i++)
        {
            bool moving=true;
            while (moving)
            {
                
                if (Input.GetKeyDown(KeyCode.N))
                {
                    break;
                }
                if (Vector3.Distance(movingPlatform.position, WayPoints[i].position) >= 0.01f)
                {
                    movingPlatform.position = Vector3.MoveTowards(movingPlatform.position, WayPoints[i].position, Time.deltaTime);
                    yield return new WaitForFixedUpdate();
                }
                else
                    moving = false;
                
            }
        }
       
        platformAnimator.SetBool("Moving", false);

        platFormTrigger1.SetActive(true);
        platFormTrigger2.SetActive(true);

        //GameManager.instance.SetAllTriggersOn();
    }

    public IEnumerator MovePlatformBackward()
    {
        //GameManager.instance.SetAllTriggersOff();
        platFormTrigger1.SetActive(false);
        platFormTrigger2.SetActive(false);
        yield return new WaitForSeconds(1);

        // get the platform layer
        int layerMask = 1 << 10;
        RaycastHit hit;
        if (Physics.Raycast(GameManager.instance.CameraEye.position, Vector3.down, out hit, 5, layerMask))
        {
            if (hit.transform == movingPlatform)
            {
                GameManager.instance.CameraRig.parent = null;
                Debug.Log("On top Of Platform!");
                GameManager.instance.CameraRig.parent = movingPlatform;
            }
        }

        for (int i = WayPoints.Count; i > 0; i--)
        {
            bool moving = true;
            while (moving)
            {

                if (Input.GetKeyDown(KeyCode.N))
                {
                    break;
                }
                if (Vector3.Distance(movingPlatform.position, WayPoints[i - 1].position) >= 0.01f)
                {
                    movingPlatform.position = Vector3.MoveTowards(movingPlatform.position, WayPoints[i - 1].position, Time.deltaTime);
                    yield return new WaitForFixedUpdate();
                }
                else
                    moving = false;

            }
        }
        
        platformAnimator.SetBool("Moving", false);


        platFormTrigger1.SetActive(true);
        platFormTrigger2.SetActive(true);
        //GameManager.instance.SetAllTriggersOn();
    }
}
