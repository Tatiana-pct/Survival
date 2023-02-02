//ce script gere l'inventaire
//this script manages the inventory
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [Header("Content element References")]
    [SerializeField] List<ItemsData> _content = new List<ItemsData>();

    [Header("Inventory Panel References")]
    [SerializeField] GameObject _InventoryPanel;
    [SerializeField] Transform _inventorySlotParent;
    [SerializeField] Sprite _emptySlotVisual;

    [Header("Action Panel References")]
    [SerializeField] GameObject _actionPanel;
    [SerializeField] GameObject _useItemButton;
    [SerializeField] GameObject _equipItemButton;
    [SerializeField] GameObject _dropItemButton;
    [SerializeField] GameObject _destroyItemButton;
    [SerializeField] Transform _dropPoint;

    [Header("Library Equipement References")]
    [SerializeField] EquipementLibrary _equipementLibrary;

    const int InventorySize = 24;

    private ItemsData _itemCurrentlySelected;

    public static Inventory _instance;


    private void Awake()
    {
        _instance = this;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
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
        //On vide tous les slots/visuel
        //We empty all the slots/visual

        for (int i = 0; i < _inventorySlotParent.childCount ; i++)
        {
            Slot currentSlot = _inventorySlotParent.GetChild(i).GetComponent<Slot>();
            currentSlot.Item = null;
            currentSlot.ItemVisual.sprite = _emptySlotVisual;
        }

        // On peuple le visuel des slots selon le contenu reel de l'inventaire
        // We populate the visual of the slots according to the real content of the inventory
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

    //Methode permettant d'ouvrir et fermer le panel d'action
    //Method to open and close the action panel
    public void OpenActionPanel(ItemsData item, Vector3 slotPosition)
    {
        _itemCurrentlySelected = item;

        if (item == null)
        {
            _actionPanel.SetActive(false);
            return;
        }
        switch (item.ItemType)
        {
            case ItemType.Ressouce:
                _useItemButton.SetActive(false);
                _equipItemButton.SetActive(false);
                break;
            case ItemType.Equipement:
                _useItemButton.SetActive(false);
                _equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                _useItemButton.SetActive(true);
                _equipItemButton.SetActive(false);
                break;
            default:
                break;
        }
        _actionPanel.transform.position = slotPosition;
        _actionPanel.SetActive(true);
    }

    //Ferme le pannel d'action
    //Close the action panel
    public void CloseActionPanel()
    {
        _actionPanel.SetActive(false);
        _itemCurrentlySelected = null;
    }

    //Methode qui gere le btn Use du panel d'action de l'inventaire
    //Method that manages the btn Use of the inventory action panel
    public void UseActionButton()
    {
        print("Use Item : " + _itemCurrentlySelected.Name);
        CloseActionPanel();
    }

    //Methode qui gere le btn Equip du panel d'action de l'inventaire
    //Method that manages the Equip btn of the inventory action panel
    public void EquipeActionButton()
    {
        print("Equipe Item : " + _itemCurrentlySelected.Name);
        EquipementLibraryItem equipementLibraryItem = _equipementLibrary.Content.Where(elem => elem.ItemsData == _itemCurrentlySelected).First();
        if(equipementLibraryItem != null)
        {
            equipementLibraryItem.ItemPrefab.SetActive(true);
        }
        else
        {
            Debug.LogError("Equipement : " + _itemCurrentlySelected.Name + " non existant dans dans la librairie des équipements");
        }
        CloseActionPanel();
    }

    //Methode qui gere le btn Drop du panel d'action de l'inventaire
    //Method that manages the btn Drop of the inventory action panel
    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(_itemCurrentlySelected.Prefab);
        instantiatedItem.transform.position = _dropPoint.position;
        _content.Remove(_itemCurrentlySelected);
        RefreshContent();
    }

    //Methode qui gere le btn Destroy du panel d'action de l'inventaire
    //Method that manages the btn Destroy of the inventory action panel
    public void DestroyActionButton()
    {
        _content.Remove(_itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }

}
