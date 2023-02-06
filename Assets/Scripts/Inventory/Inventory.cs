//ce script gere l'inventaire
//this script manages the inventory
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Equipement Panel References")]
    [SerializeField] Image _headSlotImage;
    [SerializeField] Image _chestSlotImage;
    [SerializeField] Image _handsSlotImage;
    [SerializeField] Image _legsSlotImage;
    [SerializeField] Image _feetsSlotImage;

    [SerializeField] Button _headDesequipButton;
    [SerializeField] Button _chestDesequipButton;
    [SerializeField] Button _handsDesequipButton;
    [SerializeField] Button _legsDesequipButton;
    [SerializeField] Button _feetsDesequipButton;

    [Header("Library Equipement References")]
    [SerializeField] EquipementLibrary _equipementLibrary;

    const int InventorySize = 24;

    private ItemsData _itemCurrentlySelected;
    private bool _isOpen = false;

    //Variables permettant de garder un trace des équipements actuels
    //Variables to keep track of current equipment
    private ItemsData _equipementHeadItem;
    private ItemsData _equipementChestItem;
    private ItemsData _equipementHandsItem;
    private ItemsData _equipementLegsItem;
    private ItemsData _equipementFeetItem;

    public static Inventory _instance;


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
    private void RefreshContent()
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

        UpdateEquipementDesequipButton();
    }

    #endregion

    #region OpenInventory
    private void OpenInventory()
    {
        _InventoryPanel.SetActive(true);
        _isOpen = true;
    }

    #endregion

    #region CloseInventory

    public void CloseInventory()
    {
        _InventoryPanel.SetActive(false);
        _actionPanel.SetActive(false);
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

    #region OpenActionPane
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

    #endregion

    #region CloseActionPanel
    //Ferme le pannel d'action
    //Close the action panel
    public void CloseActionPanel()
    {
        _actionPanel.SetActive(false);
        _itemCurrentlySelected = null;
    }
    #endregion

    #region UseActionButton
    //Methode qui gere le btn Use du panel d'action de l'inventaire
    //Method that manages the btn Use of the inventory action panel
    public void UseActionButton()
    {
        print("Use Item : " + _itemCurrentlySelected.Name);
        CloseActionPanel();
    }
    #endregion

    #region EquipeActionButton
    //Methode qui gere le btn Equip du panel d'action de l'inventaire
    //Method that manages the Equip btn of the inventory action panel
    public void EquipeActionButton()
    {
        print("Equipe Item : " + _itemCurrentlySelected.Name);
        EquipementLibraryItem equipementLibraryItem = _equipementLibrary.Content.Where(elem => elem.ItemsData == _itemCurrentlySelected).First();
        if (equipementLibraryItem != null)
        {
   
            //Affiche dans chaque slot l'image de l'équipement actuel
            //Display in each slot the image of the current equipment
            switch (_itemCurrentlySelected.EquipementType)
            {
                case EquipementType.Head:
                    DisablePreviousEquipedEquipement(_equipementHeadItem);
                    _headSlotImage.sprite = _itemCurrentlySelected.Visual;
                    _equipementHeadItem = _itemCurrentlySelected;
                    break;
                case EquipementType.Chest:
                    DisablePreviousEquipedEquipement(_equipementChestItem);
                    _chestSlotImage.sprite = _itemCurrentlySelected.Visual;
                    _equipementChestItem = _itemCurrentlySelected;
                    break;
                case EquipementType.hands:
                    DisablePreviousEquipedEquipement(_equipementHandsItem);
                    _handsSlotImage.sprite = _itemCurrentlySelected.Visual;
                    _equipementHandsItem = _itemCurrentlySelected;
                    break;
                case EquipementType.legs:
                    DisablePreviousEquipedEquipement(_equipementLegsItem);
                    _legsSlotImage.sprite = _itemCurrentlySelected.Visual;
                    _equipementLegsItem = _itemCurrentlySelected;
                    break;
                case EquipementType.feets:
                    DisablePreviousEquipedEquipement(_equipementFeetItem);
                    _feetsSlotImage.sprite = _itemCurrentlySelected.Visual;
                    _equipementFeetItem = _itemCurrentlySelected;
                    break;
                default:
                    break;
            }
            for (int i = 0; i < equipementLibraryItem.ElementsToDisable.Length; i++)
            {
                equipementLibraryItem.ElementsToDisable[i].SetActive(false);
            }

            equipementLibraryItem.ItemPrefab.SetActive(true);

            _content.Remove(_itemCurrentlySelected);
            RefreshContent();

        }
        else
        {
            Debug.LogError("Equipement : " + _itemCurrentlySelected.Name + " non existant dans dans la librairie des équipements");
        }
        CloseActionPanel();
    }

    #endregion

    #region DropActionButton
    //Methode qui gere le btn Drop du panel d'action de l'inventaire
    //Method that manages the btn Drop of the inventory action panel
    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(_itemCurrentlySelected.Prefab);
        instantiatedItem.transform.position = _dropPoint.position;
        _content.Remove(_itemCurrentlySelected);
        RefreshContent();
    }
    #endregion

    #region DestroyActionButton
    //Methode qui gere le btn Destroy du panel d'action de l'inventaire
    //Method that manages the btn Destroy of the inventory action panel
    public void DestroyActionButton()
    {
        _content.Remove(_itemCurrentlySelected);
        RefreshContent();
        CloseActionPanel();
    }
    #endregion

    #region UpdateEquipementDesequipButton
    private void UpdateEquipementDesequipButton()
    {
        _headDesequipButton.onClick.RemoveAllListeners();
        _headDesequipButton.onClick.AddListener(delegate { DesequipeEquipement(EquipementType.Head); });
        _headDesequipButton.gameObject.SetActive(_equipementHeadItem);

        _chestDesequipButton.onClick.RemoveAllListeners();
        _chestDesequipButton.onClick.AddListener(delegate { DesequipeEquipement(EquipementType.Chest); });
        _chestDesequipButton.gameObject.SetActive(_equipementChestItem);

        _handsDesequipButton.onClick.RemoveAllListeners();
        _handsDesequipButton.onClick.AddListener(delegate { DesequipeEquipement(EquipementType.hands); });
        _handsDesequipButton.gameObject.SetActive(_equipementHandsItem);

        _legsDesequipButton.onClick.RemoveAllListeners();
        _legsDesequipButton.onClick.AddListener(delegate { DesequipeEquipement(EquipementType.legs); });
        _legsDesequipButton.gameObject.SetActive(_equipementLegsItem);

        _feetsDesequipButton.onClick.RemoveAllListeners();
        _feetsDesequipButton.onClick.AddListener(delegate { DesequipeEquipement(EquipementType.feets); });
        _feetsDesequipButton.gameObject.SetActive(_equipementFeetItem);
    }
    #endregion

    #region DesequipeEquipement
    public void DesequipeEquipement(EquipementType equipementType)
    {

        if (IsFull())
        {

            Debug.Log("l'inventaire est plein, impossible de se déséquiper de cet élément");
            return;
        }

        ItemsData currentItem = null;

        switch (equipementType)
        {
            case EquipementType.Head:
                currentItem = _equipementHeadItem;
                _equipementHeadItem = null;
                _headSlotImage.sprite = _emptySlotVisual;
                break;

            case EquipementType.Chest:
                currentItem = _equipementChestItem;
                _equipementChestItem = null;
                _chestSlotImage.sprite = _emptySlotVisual;
                break;

            case EquipementType.hands:
                currentItem = _equipementHandsItem;
                _equipementHandsItem = null;
                _handsSlotImage.sprite = _emptySlotVisual;
                break;

            case EquipementType.legs:
                currentItem = _equipementLegsItem;
                _equipementLegsItem = null;
                _legsSlotImage.sprite = _emptySlotVisual;
                break;

            case EquipementType.feets:
                currentItem = _equipementFeetItem;
                _equipementFeetItem = null;
                _feetsSlotImage.sprite = _emptySlotVisual;
                break;

        }

        EquipementLibraryItem equipementLibraryItem = _equipementLibrary.Content.Where(elem => elem.ItemsData == currentItem).First();
        if (equipementLibraryItem != null)
        {
            for (int i = 0; i < equipementLibraryItem.ElementsToDisable.Length; i++)
            {
                equipementLibraryItem.ElementsToDisable[i].SetActive(true);
            }

            equipementLibraryItem.ItemPrefab.SetActive(false);

        }
        AddItem(currentItem);

        RefreshContent();
    }

    #endregion

    #region DisablePreviousEquipedEquipement
    //Methode qui desactive l'équipement précédement équiper
    //Method that deactivates the previously equipped equipment
    private void DisablePreviousEquipedEquipement(ItemsData itemToDisable)
    {
        if(itemToDisable == null)
        { 
            return; 
        }

        EquipementLibraryItem equipementLibraryItem = _equipementLibrary.Content.Where(elem => elem.ItemsData == itemToDisable).First();
        if (equipementLibraryItem != null)
        {
            for (int i = 0; i < equipementLibraryItem.ElementsToDisable.Length; i++)
            {
                equipementLibraryItem.ElementsToDisable[i].SetActive(true);
            }

            equipementLibraryItem.ItemPrefab.SetActive(false);

        }
        AddItem(itemToDisable);

    }

    #endregion

}
