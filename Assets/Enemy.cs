using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject deathFx = null;
    ScoreBoard scoreboard;
    public int scorePerHit = 12;
    public int hits = 10;
    // Start is called before the first frame update
    void Start()
    {
        AddBoxCollider();
        scoreboard= FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        BoxCollider boxcollider = gameObject.AddComponent<BoxCollider>();
        boxcollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hits <= 1)
        {
            KillObject();
        }
      
    }

    private void ProcessHit()
    {
        scoreboard.ScoreHit(scorePerHit);
        hits--;
    }

    private void KillObject()
    {
        Instantiate(deathFx, transform.position, Quaternion.identity);
        print("Destroying GameObject" + gameObject.name);
        Destroy(gameObject);
    }
}
