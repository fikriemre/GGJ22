using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelButton : MonoBehaviour
{
    public GameObject NonPressed;
    public GameObject Pressed;
    public UnityEvent ButtonTriggered;
    private bool triggered = false;

    private void Start()
    {
        Pressed.SetActive(false);
        NonPressed.SetActive(true);
    }

 

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (triggered)
            return;
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc)
        {
            triggered = true;
            ButtonTriggered?.Invoke();
            Pressed.SetActive(true);
            NonPressed.SetActive(false);
        }
        
        
        
        
    }
}