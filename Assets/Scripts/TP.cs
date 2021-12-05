using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP : MonoBehaviour
{

    [SerializeField] GameObject Player;
    public GameObject TargetZone;

    private void Update()
    {

        this.gameObject.transform.LookAt(Player.transform);

    }

}
