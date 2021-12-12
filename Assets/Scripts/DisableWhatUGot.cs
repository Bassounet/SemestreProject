using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWhatUGot : MonoBehaviour
{

    [SerializeField] AudioSource Ads;
    [SerializeField] AudioClip nope;
      public void DisableMe()
    {

        this.gameObject.SetActive(false);
        Ads.PlayOneShot(nope);

    }
}
