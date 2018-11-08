using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameTrigger : MonoBehaviour {

    public void StartGame()
    {
        GameManager.instance.StartGame();
        gameObject.SetActive(false);
    }
}
