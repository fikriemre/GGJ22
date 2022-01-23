using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Shinjingi;
using UnityEngine;

public class Sublevel : MonoBehaviour
{
    public PlayerController player;
    public CinemachineVirtualCamera cam; 
    public bool SELECTED =>_sublevelSelected;
    private bool _sublevelSelected = false;
    
    private List<Informer> informers = new List<Informer>();
     public void AddInformer(Informer informer)
        {
            informers.Add(informer);
        }
    
        public void RemoveInformer(Informer informer)
        {
            if (informers.Contains(informer))
            {
                informers.Remove(informer);
            }
        }
    public void SetLevelSelection(bool selection)
    {
        _sublevelSelected = selection;
        cam.gameObject.SetActive(selection);
        player.SetEnableInput(selection);
        foreach (var info in informers)
        {
                info.SetActivation(selection);
        }
    } 
}