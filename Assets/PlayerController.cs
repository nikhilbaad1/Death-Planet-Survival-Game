using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m/s")][SerializeField] float controlSpeed = 20f;
    [Tooltip("In m")] [SerializeField] float xRange = 7.5f;
    [Tooltip("In m")] [SerializeField] float yRange = 3.5f;

    [Header("Screen-Position Based")]
    public float postionPitchFactor = 1f;
    public float postionYawFactor = 6f;
    public float xThrow,yThrow;

    [Header("Control-Throw Based")]
    public float controlPitchFactor = 30f;
    public float controlrollFactor =-30f;

    bool isControlEnabled = true;

    public GameObject[] guns = null;


    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRoatation();
            ProcessFire();
        }

    }



    private void ProcessRoatation()
    {
        float pitcDueToPosition = transform.localPosition.y * postionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitcDueToPosition + pitchDueToControlThrow;
        float roll = xThrow*controlrollFactor;
        float yaw = this.transform.localPosition.x *postionYawFactor ;
        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    void ProcessTranslation() {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = controlSpeed * xThrow * Time.deltaTime;
        float yOffset = controlSpeed * yThrow * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float rawYPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, this.transform.localPosition.z);
    }

    private void ProcessFire()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        { 
        SetGunsActive(true);
            print("firing");
        }
        else
        {
        SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isactive)
    {
        foreach (GameObject gun in guns)
        {
             var emi =  gun.GetComponent<ParticleSystem>().emission;
            emi.enabled = isactive;
        }
    }
    void OnPlayerDeath()//called by string reference
    {
        //print("Control frozen");
        isControlEnabled = false;
    }


}
