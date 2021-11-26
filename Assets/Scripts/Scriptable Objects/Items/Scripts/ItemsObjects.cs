using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// on cr�e une classe qui d�coule du scriptable object et pas monobehaviour. et dedans on met les �l�ments que vont contenir nos futurs objects


public enum ItemType
{

    ObjectCollectable,

}

public abstract class ItemsObjects : ScriptableObject
{

    public GameObject prefab;

    public ItemType type;

    [TextArea(15, 20)]
    public string SentenceH;

    public Sprite logo;

}
