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

    private void RefreshContent()
    {
        for (int i = 0; i < _content.Count; i++)
        {
            _inventorySlotParent.GetChild(i).GetChild(0).GetComponent<Image>().sprite = _content[i].Visual;
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
