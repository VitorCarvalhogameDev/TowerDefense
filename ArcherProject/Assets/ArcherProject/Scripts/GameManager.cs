using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public Transform CameraRig, CameraEye;

    public List<Transform> Crystals;

    [SerializeField]
    private int activeCrystals;

    public Transform[] EnemySpawnerPool;
    public Transform[] SpawnPositions;


    // Use this for initialization
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    public void StartGame()
    {
        StartCoroutine(StartSpawning());
    }


    #region Elevator Triggers
    public GameObject[] PlatformTriggers; 

    public void SetAllTriggersOff()
    {
        foreach(GameObject obj in PlatformTriggers)
        {
            obj.SetActive(false);
        }
    }

    public void SetAllTriggersOn()
    {
        foreach (GameObject obj in PlatformTriggers)
        {
            obj.SetActive(true);
        }
    }
    #endregion


	


    public IEnumerator StartSpawning()
    {
        yield return new WaitForSeconds(1);

        foreach(Transform t in EnemySpawnerPool)
        {
            if(!t.gameObject.activeSelf)
            {
                t.position = SpawnPositions[Random.Range(0, SpawnPositions.Length)].position;
                t.gameObject.SetActive(true);
                break;
            }
        }

        yield return new WaitForSeconds(5);
        StartCoroutine(StartSpawning());
    }

    public Transform GetRandomCrystalPosition()
    {
        activeCrystals = 0;

        for (int i = Crystals.Count - 1; i >= 0; i--)
        {
            if (Crystals[i] == null)
            {
                Crystals.RemoveAt(i);
            }
            else
            {
                activeCrystals += 1;
            }
        }

        if (activeCrystals > 0)
        {
            int randomCrystal = Random.Range(0, activeCrystals);

            return Crystals[randomCrystal];
        }
        else
        {
            return null;
        }

        
    }

    public void Update()
    {
       
    }
}
