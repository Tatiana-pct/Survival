////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsable des interaction joueur/////////////////////////////////////////////////////
//////////////////////////Script responsible for player interaction/////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractBeheviour : MonoBehaviour
{
    [Header("Inventory Script references")]
    [SerializeField]private Inventory _inventory;

    [Header("Animator references")]
    [SerializeField]private Animator _animator;

    [Header("MoveBehaviour Script references")]
    [SerializeField]MoveBehaviour _moveBehaviour;

    [Header("Equipement Script references")]
    [SerializeField] Equipement _equipementSystem;

    [Header("EquipementLibrary Script references")]
    [SerializeField] EquipementLibrary _equipementLibrary;

    [Header("Tools Visual")]
    [SerializeField]GameObject _pickAxeVisual;
    [SerializeField]GameObject _axeVisual;

    private Vector3 _spawnItemOffset = new Vector3(0,0.5f,0);
    private Item _currentItem;
    private Harvestable _currentHarvestable;
    private Tool _currentTool;
    private bool _isBusy;

    public bool IsBusy { get => _isBusy; set => _isBusy = value; }

    #region DoPickUp
    //Methode permettant de collecter des items
    //Method to collect items
    public void DoPickUp(Item item)
    {
        if(_isBusy)
        {
            return;
        }
        else
        {
            _isBusy = true;
        }

        if(_inventory.IsFull())
        {
            return;
        }

        _currentItem = item;
        //jouer l'animation de ramassage
        //play farm animation
        _animator.SetTrigger("Pickup");
        // bloquer le deplacement pendant le ramassage
        // block movement while picking up
        _moveBehaviour.CanMove = false;
    }
    #endregion

    #region DoHarvest
    //Methode permettant de farmer des items
    //Method to farm items
    public void DoHarvest(Harvestable harvestable)
    {
        if (_isBusy)
        {
            return;
        }
        else
        {
            _isBusy = true;
        }

        //active le visuel de l'outil de farm
        //activate the visual of the farming tool
        _currentTool = harvestable.Tool;
        EnabledToolGameObjectFromEnum(_currentTool);

        //variable temporaire
        //temporary variable
        _currentHarvestable = harvestable;

        //jouer l'animation de farm
        //play farm animation
        _animator.SetTrigger("Harvest");

        // bloquer le deplacement pendant le ramassage
        // block movement while picking up
        _moveBehaviour.CanMove = false;
    }
    #endregion

    #region BreakHarvestable (couroutine)
    //couroutine call from harvesting animation
    IEnumerator BreakHarvestable()
    {
        Harvestable currentlyHarvesting= _currentHarvestable;

        //Permet de désactiver la posibiité d'intéragir avec le Harvestable + d'une fois (passage du layer Harvestable à default)
        //Allows you to deactivate the possibility of interacting with the Harvestable + once (passage of the Harvestable layer to default)
        currentlyHarvesting.gameObject.layer = LayerMask.NameToLayer("Default");

        if(currentlyHarvesting.DisableKinematicOnHarvest)
        {
            Rigidbody rigidbody = currentlyHarvesting.gameObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = false;
            rigidbody.AddForce(transform.forward *800, ForceMode.Impulse);  
        }

        yield return new WaitForSeconds(currentlyHarvesting.DestroyDelay);

        for (int i = 0; i < currentlyHarvesting.HarvestableItem.Length; i++)
        {
            Ressouces ressouces = currentlyHarvesting.HarvestableItem[i];
            if(Random.Range(1,101) <= ressouces.DropChance)
            {
                GameObject instatiatedressouce = Instantiate(ressouces.ItemData.Prefab);
                instatiatedressouce.transform.position = currentlyHarvesting.transform.position + _spawnItemOffset;
            }
        }
        Destroy(currentlyHarvesting.gameObject);
    }
    #endregion

    #region AddItemInventory
    public void AddItemToInventory()
    {
        //ajouter l'item a l'inventaire du joueur
        //add the item to the player's inventory
        _inventory.AddItem(_currentItem.Itemdata);
        //detruit le gameobject 
        //destroy the gameobject
        Destroy(_currentItem.gameObject);
       
    }
    #endregion

    #region ReEnablePlayerMouvement
    public void ReEnablePlayerMouvement()
    {
        //desactive le visuel de l'outil de farm
        //disable the visual of the farming tool
        EnabledToolGameObjectFromEnum(_currentTool, false);
        //debloque le deplacement
        //unblock the movement
        _moveBehaviour.CanMove = true;
        _isBusy = false;

    }
    #endregion

    #region EnabledToolGameObjectFromEnum
    private void EnabledToolGameObjectFromEnum(Tool toolType, bool enabled = true)
    {
        //Recherche de l'item dans la librairy
        //Search for the item in the library
        EquipementLibraryItem equipementLibraryItem = _equipementLibrary.Content.Where(elem => elem.ItemsData == _equipementSystem.EquipementWeaponItem).FirstOrDefault();

        if (equipementLibraryItem != null)
        {
            for (int i = 0; i < equipementLibraryItem.ElementsToDisable.Length; i++)
            {
                equipementLibraryItem.ElementsToDisable[i].SetActive(enabled);
            }

            equipementLibraryItem.ItemPrefab.SetActive(!enabled);
        }

        switch (toolType)
        {
            case Tool.PickAxe:
                _pickAxeVisual.SetActive(enabled);
                break;
            case Tool.Axe:
                _axeVisual.SetActive(enabled);  
                break;
            default:
                break;
        }
    }

    #endregion

}
