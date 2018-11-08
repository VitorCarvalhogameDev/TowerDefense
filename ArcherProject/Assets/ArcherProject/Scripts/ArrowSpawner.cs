using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomScripts
{ 
    public class ArrowSpawner : MonoBehaviour
    {
        public Transform[] arrowPool;

        public void Start()
        {
           
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                InstantiateArrow();
            }
        }
        
        public bool InstantiateArrow()
        {
            foreach(Transform t in arrowPool)
            {
                if(!t.gameObject.activeSelf)
                {
                    t.gameObject.SetActive(true);
                    return true;
                }
            }
            return false;
        }
    }
}
