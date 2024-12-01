﻿using UnityEngine;

/// <summary>
/// This class has conditions to determine which of the crosshairs should be enabled and updating. 
/// </summary>
public class CrosshairHandler : MonoBehaviour
{


    [TextArea(3, 10)]
    public string notes = "This class has conditions to determine which of the crosshairs should be enabled and updating.";

    private CrosshairMouse mouseCrosshair;


    private bool isReady;

    private void Awake()
    {
        mouseCrosshair = FindObjectOfType<CrosshairMouse>();
       
    }

    private void Update()
    {
        CheckIfReady();


            if (!mouseCrosshair.IsActive)
            {
                mouseCrosshair.IsActive = true;
           
            }

            mouseCrosshair.UpdateCrosshair();
        }
    

    private void CheckIfReady()
    {
        if (!isReady)
        {
            if (mouseCrosshair == null)
            {
                Debug.LogError(gameObject.name + ": mouse or/and joystick crosshair missing!");
                return;
            }

            isReady = true;
        }
    }
}
