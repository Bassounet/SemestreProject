using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhatUGot : MonoBehaviour
{
    [SerializeField] AudioSource Ads;
    [SerializeField] AudioClip No;
    public bool activado;
      public void DisableMe()
    {
        if (!activado)
        {
            Ads.enabled = false;
            Ads.enabled = true;
            Ads.PlayOneShot(No);
            Debug.Log("bidoudou");
        }
        
        this.gameObject.SetActive(false);


    }
}
