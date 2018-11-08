using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace CustomScripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class Arrow : MonoBehaviour
    {
        // reference to the Player Hand
        public Transform playerHandReference;

        // reference to the Player Bow
        public Transform playerBowReference;

        Rigidbody rb;

        public bool isFired;
        public bool isAttachedToBow;
        public bool collidedThisShot;

        public Vector3 storedShootDirection;
        public float storedShootForce;

        public void FireArrow(Vector3 dir, float force)
        {
            isFired = true;
            rb.isKinematic = false;
            rb.AddForce(-dir * force * 50, ForceMode.Impulse);

            StartCoroutine(DeSpawn());
        }

        // Use this for initialization
        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            rb.isKinematic = true;
            GetComponent<Collider>().enabled = true;
            isFired = false;
            transform.parent = null;
            collidedThisShot = false;
        }
        
        // Update is called once per frame
        void Update()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 0.05f);
            foreach (Collider col in cols)
            {
                if (col.name == "ArrowTrigger")
                {
                    isAttachedToBow = true;
                }

                if (isFired && !collidedThisShot)
                {
                    

                    // check for collisions
                    IDamagable DamageableInterface = col.gameObject.GetComponent<IDamagable>();
                    if (DamageableInterface != null)
                    {
                        Debug.Log("Hit enemy");
                        collidedThisShot = true;
                        GetComponent<Collider>().enabled = false;
                        rb.isKinematic = true;

                        DamageableInterface.TakeDamage(20f);
                    }

                    IInteractableplatform platformMover = col.gameObject.GetComponent<IInteractableplatform>();
                    if (platformMover != null)
                    {
                        Debug.Log("Hit platform");
                        collidedThisShot = true;
                        GetComponent<Collider>().enabled = false;
                        rb.isKinematic = true;

                        platformMover.MovePlatform();
                    }

                    StartGameTrigger StartTrigger = col.gameObject.GetComponent<StartGameTrigger>();
                    if (StartTrigger != null)
                    {
                        Debug.Log("Hit Start Trigger");
                        collidedThisShot = true;
                        GetComponent<Collider>().enabled = false;
                        rb.isKinematic = true;

                        StartTrigger.StartGame();
                    }
                }
            }

            if (!isFired)
            {
                if (SteamVR_Input._default.inActions.GrabGrip.GetState(SteamVR_Input_Sources.RightHand))
                { 
                    float DistanceHandAndBow = Vector3.Distance(playerHandReference.position, playerBowReference.position);
                    
                        if (DistanceHandAndBow < 0.7f && isAttachedToBow)
                        {
                            transform.position = playerBowReference.position;
                            Vector3 HandDirection = playerHandReference.position - transform.position;
                            HandDirection.Normalize();
                            transform.rotation = Quaternion.LookRotation(HandDirection, Vector3.back);

                            storedShootForce = DistanceHandAndBow;
                            storedShootDirection = HandDirection;

                            Vector3 middlept = (playerHandReference.position + playerBowReference.position) / 2;
                            transform.GetChild(0).position = middlept;
                        }
                        else
                        { 
                            transform.position = playerHandReference.position;
                            transform.rotation = playerHandReference.rotation;
                            isAttachedToBow = false;
                        }

                       
                }
                else
                {
                    transform.position = playerHandReference.position;
                    transform.rotation = playerHandReference.rotation;
                }
            }
            else
            {
                transform.forward = Vector3.Slerp(transform.forward, -rb.velocity.normalized, Time.deltaTime * 5);
            }

            if (SteamVR_Input._default.inActions.GrabGrip.GetStateUp(SteamVR_Input_Sources.RightHand))
            {
                if (storedShootForce > 0.2f && isAttachedToBow)
                {
                    transform.rotation = Quaternion.LookRotation(storedShootDirection, Vector3.forward);
                    FireArrow(storedShootDirection, storedShootForce);
                }
            }

            
        }

        public IEnumerator DeSpawn()
        {
            yield return new WaitForSeconds(5);
            isAttachedToBow = false;
            gameObject.SetActive(false);
        }
    }
}
