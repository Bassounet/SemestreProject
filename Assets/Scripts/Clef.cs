using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clef : MonoBehaviour
{

    public string WhatUGat;

    [SerializeField] GameObject ScriptForAccess;

    public bool KeyVestaire;
    public bool KeyLabo;
    public bool KeyBibli;
    public bool Collected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (KeyVestaire && Collected)
        {

            ScriptForAccess.GetComponent<testCam>().AccesVestiaire = true;            

        }

        if (KeyBibli && Collected)
        {

            ScriptForAccess.GetComponent<testCam>().AccessBibli = true;

        }

        if (KeyLabo && Collected)
        {

            ScriptForAccess.GetComponent<testCam>().AccessLabo = true;

        }

    }
}
