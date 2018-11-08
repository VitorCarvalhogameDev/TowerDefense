using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace CustomScripts
{
    public class PlayerHand : MonoBehaviour
    {
        public ArrowSpawner arrowSpanwerPoolRef;

        public Bow PlayerBow;

        public bool playerHaveArrow;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                LookForArrow();
            }


            if (SteamVR_Input._default.inActions.GrabGrip.GetState(SteamVR_Input_Sources.RightHand))
            {
                LookForString();
            }
            if (SteamVR_Input._default.inActions.GrabGrip.GetStateUp(SteamVR_Input_Sources.RightHand))
            {
                PlayerBow.playerPushedString = false;
            }

        }

        public void LookForString()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 0.05f);
            foreach (Collider col in cols)
            {
                if (col.name == "ArrowTrigger")
                {
                    PlayerBow.playerPushedString = true;
                }
            }
        }

        public void LookForArrow()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, 0.05f);
            foreach (Collider col in cols)
            {
                if (col.name == "Quiver")
                {   // if spawner has no reference, assign one time
                    if(arrowSpanwerPoolRef == null)
                    {
                        arrowSpanwerPoolRef = col.GetComponent<ArrowSpawner>();
                    }
                    
                    playerHaveArrow = arrowSpanwerPoolRef.InstantiateArrow();
                }

                
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.05f);

        }
    }
}