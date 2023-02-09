using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBeheviour : MonoBehaviour
{   
    [SerializeField]private Inventory _inventory;
    [SerializeField]private Animator _animator;
    [SerializeField]MoveBehaviour _moveBehaviour; 
    [SerializeField]GameObject _pickAxeVisual; 

    private Item _currentItem;
    private Harvestable _currentHarvestable;

    #region DoPickUp
    //Methode permettant de collecter des items
    //Method to collect items
    public void DoPickUp(Item item)
    {
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
        //active le visuel de l'outil de farm
        //activate the visual of the farming tool
        _pickAxeVisual.SetActive(true);
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

    #region BreakHarvestable
    //Methode de destruction des collectible de farm
    //Method of destroying farm collectibles
    public void BreakHarvestable()
    {
        for (int i = 0; i < _currentHarvestable.HarvestableItem.Length; i++)
        {
            Ressouces ressouces = _currentHarvestable.HarvestableItem[i];
            for (int y = 0; y < Random.Range(ressouces.MinAmoutSpwane,(float) ressouces.MaxAmoutSpwane); y++)
            {
                GameObject instatiatedressouce = GameObject.Instantiate(ressouces.ItemData.Prefab);
                instatiatedressouce.transform.position = _currentHarvestable.transform.position;
            }
        }
        Destroy(_currentHarvestable.gameObject);
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
        _pickAxeVisual.SetActive(false);
        //debloque le deplacement
        //unblock the movement
        _moveBehaviour.CanMove = true;

    }
    #endregion

}
