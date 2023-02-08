using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsActionSystem : MonoBehaviour
{
    [Header("Equipement Script References")]
    [SerializeField] Equipement _equipement;

    [Header("Action Panel References")]
    [SerializeField] GameObject _actionPanel;
    [SerializeField] GameObject _useItemButton;
    [SerializeField] GameObject _equipItemButton;
    [SerializeField] GameObject _dropItemButton;
    [SerializeField] GameObject _destroyItemButton;
    [SerializeField] Transform _dropPoint;

    private ItemsData _itemCurrentlySelected;



    public GameObject ActionPanel { get => _actionPanel; set => _actionPanel = value; }
    public ItemsData ItemCurrentlySelected { get => _itemCurrentlySelected; set => _itemCurrentlySelected = value; }

    #region OpenActionPane
    //Methode permettant d'ouvrir et fermer le panel d'action
    //Method to open and close the action panel
    public void OpenActionPanel(ItemsData item, Vector3 slotPosition)
    {
        _itemCurrentlySelected = item;

        if (item == null)
        {
            ActionPanel.SetActive(false);
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
        ActionPanel.transform.position = slotPosition;
        ActionPanel.SetActive(true);
    }

    #endregion

    #region CloseActionPanel
    //Ferme le pannel d'action
    //Close the action panel
    public void CloseActionPanel()
    {
        ActionPanel.SetActive(false);
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

    #region DropActionButton
    //Methode qui gere le btn Drop du panel d'action de l'inventaire
    //Method that manages the btn Drop of the inventory action panel
    public void DropActionButton()
    {
        GameObject instantiatedItem = Instantiate(_itemCurrentlySelected.Prefab);
        instantiatedItem.transform.position = _dropPoint.position;
        Inventory._instance.RemoveItem(_itemCurrentlySelected);
        Inventory._instance.RefreshContent();
    }
    #endregion

    #region DestroyActionButton
    //Methode qui gere le btn Destroy du panel d'action de l'inventaire
    //Method that manages the btn Destroy of the inventory action panel
    public void DestroyActionButton()
    {
        Inventory._instance.RemoveItem(_itemCurrentlySelected);
        Inventory._instance.RefreshContent(); ;
        CloseActionPanel();
    }
    #endregion

    #region EquipActionButton
    public void EquipActionButton()
    {
        _equipement.EquipeAction();
    }

    #endregion
}
