using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Chir Name", menuName = "Inventory System/ Items/ ObjectCollectable")]

public class ChirObject : ItemsObjects
{

    public void Awake()
    {
        type = ItemType.ObjectCollectable;
    }

}
