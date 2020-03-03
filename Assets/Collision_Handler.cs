using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collision_Handler : MonoBehaviour
{
    [Tooltip("Fx Prefab on Player")]public GameObject deathFx=null;
    public float levelLoadDelay=1f;
    private void OnTriggerEnter(Collider other)
    {
        deathFx.SetActive(true);
        Invoke("LoadScene", levelLoadDelay);
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        print("Player Dying");
        SendMessage("OnPlayerDeath");    
    }

    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}
