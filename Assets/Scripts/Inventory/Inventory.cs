//ce script gere l'inventaire
//this script manages the inventory
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [Header ("Script Equipement References" )]

    [SerializeField] Equipement _equipement;

    [Header("Script ItemActionSystem References")]
    [SerializeField] ItemsActionSystem _itemsActionSystem;

    [Header("Content element References")]
    [SerializeField] List<ItemsData> _content = new List<ItemsData>();

    [Header("Inventory Panel References")]
    [SerializeField] GameObject _inventoryPanel;
    [SerializeField] Transform _inventorySlotParent;
    [SerializeField] Sprite _emptySlotVisual;

    const int InventorySize = 24;

    private bool _isOpen = false;

    public static Inventory _instance;

    public Sprite EmptySlotVisual { get => _emptySlotVisual; set => _emptySlotVisual = value; }

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
    //methode ajoutant un item a l'inventaire
    public void AddItem(ItemsData item)
    {
        _content.Add(item);
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
        }

        // On peuple le visuel des slots selon le contenu reel de l'inventaire
        // We populate the visual of the slots according to the real content of the inventory
        for (int i = 0; i < _content.Count; i++)
        {
            Slot currentSlot = _inventorySlotParent.GetChild(i).GetComponent<Slot>();
            currentSlot.Item = _content[i];
            currentSlot.ItemVisual.sprite = _content[i].Visual;
        }

        _equipement.UpdateEquipementDesequipButton();
    }
    #endregion

    #region RemoveItem
    //methode ajoutant un item a l'inventaire
    public void RemoveItem(ItemsData item)
    {
        _content.Remove(item);
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









}
