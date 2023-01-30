//ce script gere l'inventaire
//this script manages the inventory
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField]List<ItemsData> _content = new List<ItemsData>();

    [SerializeField] GameObject _InventoryPanel;
    [SerializeField] Transform _inventorySlotParent;

    const int InventorySize = 24;

   

    


    //public List<ItemData> Content { get => _content; set => _content = value; }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            _InventoryPanel.SetActive(!_InventoryPanel.activeSelf);
        }
    }

    private void Start()
    {
        RefreshContent();   
    }

    //methode ajoutant un item a l'inventaire
    public void AddIten(ItemsData item)
    {
        _content.Add(item);
        RefreshContent();
    }

    //methode qui  va peupler le contenue reel de l'inventaire
    //method that will populate the actual content of the inventory
    private void RefreshContent()
    {
        for (int i = 0; i < _content.Count; i++)
        {
            Slot currentSlot = _inventorySlotParent.GetChild(i).GetComponent<Slot>();
            currentSlot.Item = _content[i];
            currentSlot.ItemVisual.sprite = _content[i].Visual;
        }
    }


    public void CloseInventory()
    {
        _InventoryPanel.SetActive(false);
    }

    public bool IsFull()
    {
        return InventorySize == _content.Count;
    }

}
