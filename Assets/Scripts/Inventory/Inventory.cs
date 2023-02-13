//ce script gere l'inventaire
//this script manages the inventory
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [Header ("Script Equipement References" )]
    [SerializeField] Equipement _equipement;

    [Header("Script ItemActionSystem References")]
    [SerializeField] ItemsActionSystem _itemsActionSystem;

    [Header("Script CraftingSystem References")]
    [SerializeField] CraftingSystem _craftingSystem;

    [Header("Content element References")]
    //[SerializeField] List<ItemsData> _content = new List<ItemsData>();
    [SerializeField] List<ItemInInventory> _content = new List<ItemInInventory>();

    [Header("Inventory Panel References")]
    [SerializeField] GameObject _inventoryPanel;
    [SerializeField] Transform _inventorySlotParent;
    [SerializeField] Sprite _emptySlotVisual;

    const int InventorySize = 24;

    private bool _isOpen = false;

    public static Inventory _instance;

    public Sprite EmptySlotVisual { get => _emptySlotVisual; set => _emptySlotVisual = value; }
    public List<ItemInInventory> Content { get => _content; set => _content = value; }

    private void Awake()
    {
        _instance = this;
    }


    private void Update()
    {
        //Affiche ou ferme le panel inventaire via le Btn "I"
        //Displays or closes the inventory panel via the Btn "I"
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (_isOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }

    }

    private void Start()
    {
        RefreshContent();
        CloseInventory();
    }

    #region AddItem
    //methode ajoutant un item a l'inventaire + stack
    //method adding an item to the inventoryc + stack
    public void AddItem(ItemsData item)
    {
        ItemInInventory itemInventory = _content.Where(elem => elem._itemsData == item).FirstOrDefault(); 
        if(itemInventory!= null && item.Stackable)
        {
            itemInventory.count++;
        }
        else
        {
            _content.Add(new ItemInInventory{_itemsData = item, count = 1 });
        }

        RefreshContent();
    }
    #endregion

    #region RefreshContent
    public void RefreshContent()
    {
        //On vide tous les slots/visuel
        //We empty all the slots/visual
        for (int i = 0; i < _inventorySlotParent.childCount; i++)
        {
            Slot currentSlot = _inventorySlotParent.GetChild(i).GetComponent<Slot>();
            currentSlot.Item = null;
            currentSlot.ItemVisual.sprite = _emptySlotVisual;
            currentSlot.CountTxt.enabled = false;
        }

        // On peuple le visuel des slots selon le contenu reel de l'inventaire
        // We populate the visual of the slots according to the real content of the inventory
        for (int i = 0; i < _content.Count; i++)
        {
            Slot currentSlot = _inventorySlotParent.GetChild(i).GetComponent<Slot>();
            currentSlot.Item = _content[i]._itemsData;
            currentSlot.ItemVisual.sprite = _content[i]._itemsData.Visual;

            if(currentSlot.Item.Stackable)
            {
                currentSlot.CountTxt.enabled = true;
                currentSlot.CountTxt.text = _content[i].count.ToString();
            }
        }

        _equipement.UpdateEquipementDesequipButton();
        _craftingSystem.UpdateDisplayRecipes();

    }
    #endregion

    #region GetContent 
    //Methode verifiant les items requis
    //Method verifying required items
    public List<ItemInInventory> GetContent()
    {
        return _content;
    }
    #endregion

    #region RemoveItem
    //methode supprimant un item a l'inventaire
    //method deleting an item from the inventory
    public void RemoveItem(ItemsData item, int count =1)
    {

        ItemInInventory itemInventory = _content.Where(elem => elem._itemsData == item).FirstOrDefault();
        if (itemInventory.count > count && item.Stackable)
        {
            itemInventory.count -= count;
        }
        else
        {
            _content.Remove(itemInventory);
        }
        RefreshContent();
    }
    #endregion

    #region OpenInventory
    private void OpenInventory()
    {
        _inventoryPanel.SetActive(true);
        _isOpen = true;
    }

    #endregion

    #region CloseInventory
    public void CloseInventory()
    {
        _inventoryPanel.SetActive(false);
        _itemsActionSystem.ActionPanel.SetActive(false);
        TooltipSystem._instance.Hide();
        _isOpen = false;
    }

    #endregion

    #region IsFull
    public bool IsFull()
    {
        return InventorySize == _content.Count;
    }
    #endregion

    #region Class ItemInInventory
    [System.Serializable]
    public class ItemInInventory
    {
        public ItemsData _itemsData;
        public int count;
    }

    #endregion









}
