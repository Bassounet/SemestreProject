using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{

    public InventoryObject inventory;

    public int X_SPACE_BETWEEN_ITEM;
    public int NbColumn;
    public int Y_SPACE_BETWEEN_ITEM;
    Dictionary<InventorySlots, GameObject> itemDiplayed = new Dictionary<InventorySlots, GameObject> ();

    void Start()
    {
        CreateDisplay();
    }

    
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {

        for ( int i = 0; i < inventory.Container.Count; i++)
        {

            if(itemDiplayed.ContainsKey(inventory.Container[i]))
            {

                itemDiplayed[inventory.Container[i]].GetComponent<Image>().sprite = inventory.Container[i].item.logo;

            }
            else
            {

                var obj = Instantiate(inventory.Container[i].item.prefab/* création du préfab qui est relié à l'objet dans l'inventaire*/, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i); // on calcul avec notre ptit i pour pouvoir faire avec le i du dessus
                obj.GetComponentInChildren<Image>().sprite = inventory.Container[i].item.logo;
                itemDiplayed.Add(inventory.Container[i], obj);

            }

        }

    }

    public void CreateDisplay()
    {

        for ( int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab/* création du préfab qui est relié à l'objet dans l'inventaire*/, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i); // on calcul avec notre ptit i pour pouvoir faire avec le i du dessus
            obj.GetComponent<Image>().sprite = inventory.Container[i].item.logo;

            itemDiplayed.Add(inventory.Container[i], obj);

        }

    }

    public Vector3 GetPosition(int i )
    {

        return new Vector3(X_SPACE_BETWEEN_ITEM * (i % NbColumn), (-Y_SPACE_BETWEEN_ITEM * (i / NbColumn)), 0f); // determination de la position d'arrivée de notre 


    }
}
