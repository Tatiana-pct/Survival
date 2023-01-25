using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField]private List<ItemData> _content = new List<ItemData>();

    public List<ItemData> Content { get => _content; set => _content = value; }

  
    //methode ajoutant un item a l'inventaire
    public void AddIten(ItemData item)
    {
        Content.Add(item);
    }
}
