using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class Equipement : MonoBehaviour
{
    [Header("Library Equipement References")]
    [SerializeField] EquipementLibrary _equipementLibrary;

    [Header("Script ItemActionSystem References")]
    [SerializeField] ItemsActionSystem _itemsActionSystem;

    [Header("Script PlayerStats References")]
    [SerializeField] PlayerStats _playerStats;

    [Header("Equipement Panel References")]
    [SerializeField] Image _headSlotImage;
    [SerializeField] Image _chestSlotImage;
    [SerializeField] Image _handsSlotImage;
    [SerializeField] Image _legsSlotImage;
    [SerializeField] Image _feetsSlotImage;
    [SerializeField] Image _weaponSlotImage;


    [Header("Button Equipement References")]
    [SerializeField] Button _headDesequipButton;
    [SerializeField] Button _chestDesequipButton;
    [SerializeField] Button _handsDesequipButton;
    [SerializeField] Button _legsDesequipButton;
    [SerializeField] Button _feetsDesequipButton;
    [SerializeField] Button _WeaponDesequipButton;

    //Variables permettant de garder un trace des équipements actuels
    //Variables to keep track of current equipment
    private ItemsData _equipementHeadItem;
    private ItemsData _equipementChestItem;
    private ItemsData _equipementHandsItem;
    private ItemsData _equipementLegsItem;
    private ItemsData _equipementFeetItem;
    private ItemsData _equipementWeaponItem;

    public ItemsData EquipementWeaponItem { get => _equipementWeaponItem; set => _equipementWeaponItem = value; }

    #region DisablePreviousEquipedEquipement
    //Methode qui desactive l'équipement précédement équiper
    //Method that deactivates the previously equipped equipment
    private void DisablePreviousEquipedEquipement(ItemsData itemToDisable)
    {
        if (itemToDisable == null)
        {
            return;
        }

        //Recherche de l'item dans la librairy
        //Search for the item in the library
        EquipementLibraryItem equipementLibraryItem = _equipementLibrary.Content.Where(elem => elem.ItemsData == itemToDisable).First();
        if (equipementLibraryItem != null)
        {
            for (int i = 0; i < equipementLibraryItem.ElementsToDisable.Length; i++)
            {
                equipementLibraryItem.ElementsToDisable[i].SetActive(true);
            }

            equipementLibraryItem.ItemPrefab.SetActive(false);
        }

        //Retire des point d'amure lorque l'on s'équipe d'un item
        //Remove tack points when equipping an item
        _playerStats.CurrentArmorPoints -= itemToDisable.ArmorPoints;

        Inventory._instance.AddItem(itemToDisable);

    }
    #endregion

    #region DesequipeEquipement
    public void DesequipeEquipement(EquipementType equipementType)
    {

        if (Inventory._instance.IsFull())
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
                _headSlotImage.sprite = Inventory._instance.EmptySlotVisual;
                break;

            case EquipementType.Chest:
                currentItem = _equipementChestItem;
                _equipementChestItem = null;
                _chestSlotImage.sprite = Inventory._instance.EmptySlotVisual;
                break;

            case EquipementType.hands:
                currentItem = _equipementHandsItem;
                _equipementHandsItem = null;
                _handsSlotImage.sprite = Inventory._instance.EmptySlotVisual;
                break;

            case EquipementType.legs:
                currentItem = _equipementLegsItem;
                _equipementLegsItem = null;
                _legsSlotImage.sprite = Inventory._instance.EmptySlotVisual;
                break;

            case EquipementType.feets:
                currentItem = _equipementFeetItem;
                _equipementFeetItem = null;
                _feetsSlotImage.sprite = Inventory._instance.EmptySlotVisual;
                break;

            case EquipementType.weapon:
                currentItem = _equipementWeaponItem;
                _equipementWeaponItem = null;
                _weaponSlotImage.sprite = Inventory._instance.EmptySlotVisual;
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
        //Retire des point d'amure lorque l'on s'équipe d'un item
        //Remove tack points when equipping an item
        _playerStats.CurrentArmorPoints -= currentItem.ArmorPoints;

        Inventory._instance.AddItem(currentItem);
        Inventory._instance.RefreshContent();
    }
    #endregion

    #region UpdateEquipementDesequipButton
    public void UpdateEquipementDesequipButton()
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

        _WeaponDesequipButton.onClick.RemoveAllListeners();
        _WeaponDesequipButton.onClick.AddListener(delegate { DesequipeEquipement(EquipementType.weapon); });
        _WeaponDesequipButton.gameObject.SetActive(_equipementWeaponItem);
    }
    #endregion

    #region EquipeActionButton
    //Methode qui gere le btn Equip du panel d'action de l'inventaire
    //Method that manages the Equip btn of the inventory action panel
    public void EquipeAction()
    {
        print("Equipe Item : " + _itemsActionSystem.ItemCurrentlySelected.Name);

        //Recherche dans la librairie d'équipement l'item a équiper
        //Search in the equipment library for the item to equip
        EquipementLibraryItem equipementLibraryItem = _equipementLibrary.Content.Where(elem => elem.ItemsData == _itemsActionSystem.ItemCurrentlySelected).First();
        if (equipementLibraryItem != null)
        {

            //Affiche dans chaque slot l'image de l'équipement actuel
            //Display in each slot the image of the current equipment
            switch (_itemsActionSystem.ItemCurrentlySelected.EquipementType)
            {
                case EquipementType.Head:
                    DisablePreviousEquipedEquipement(_equipementHeadItem);
                    _headSlotImage.sprite = _itemsActionSystem.ItemCurrentlySelected.Visual;
                    _equipementHeadItem = _itemsActionSystem.ItemCurrentlySelected;
                    break;

                case EquipementType.Chest:
                    DisablePreviousEquipedEquipement(_equipementChestItem);
                    _chestSlotImage.sprite = _itemsActionSystem.ItemCurrentlySelected.Visual;
                    _equipementChestItem = _itemsActionSystem.ItemCurrentlySelected;
                    break;

                case EquipementType.hands:
                    DisablePreviousEquipedEquipement(_equipementHandsItem);
                    _handsSlotImage.sprite = _itemsActionSystem.ItemCurrentlySelected.Visual;
                    _equipementHandsItem = _itemsActionSystem.ItemCurrentlySelected;
                    break;

                case EquipementType.legs:
                    DisablePreviousEquipedEquipement(_equipementLegsItem);
                    _legsSlotImage.sprite = _itemsActionSystem.ItemCurrentlySelected.Visual;
                    _equipementLegsItem = _itemsActionSystem.ItemCurrentlySelected;
                    break;

                case EquipementType.feets:
                    DisablePreviousEquipedEquipement(_equipementFeetItem);
                    _feetsSlotImage.sprite = _itemsActionSystem.ItemCurrentlySelected.Visual;
                    _equipementFeetItem = _itemsActionSystem.ItemCurrentlySelected;
                    break;

                case EquipementType.weapon:
                    DisablePreviousEquipedEquipement(_equipementWeaponItem);
                    _weaponSlotImage.sprite = _itemsActionSystem.ItemCurrentlySelected.Visual;
                    _equipementWeaponItem = _itemsActionSystem.ItemCurrentlySelected;
                    break;

                default:
                    break;
            }
            for (int i = 0; i < equipementLibraryItem.ElementsToDisable.Length; i++)
            {
                equipementLibraryItem.ElementsToDisable[i].SetActive(false);
            }

            equipementLibraryItem.ItemPrefab.SetActive(true);

            //Rajoute des point d'amure lorque l'on s'équipe d'un item
            //Add tack points when equipping an item
            _playerStats.CurrentArmorPoints += _itemsActionSystem.ItemCurrentlySelected.ArmorPoints;

            Inventory._instance.RemoveItem(_itemsActionSystem.ItemCurrentlySelected);

        }
        else
        {
            Debug.LogError("Equipement : " + _itemsActionSystem.ItemCurrentlySelected.Name + " non existant dans dans la librairie des équipements");
        }
        _itemsActionSystem.CloseActionPanel();
    }

    #endregion
}
