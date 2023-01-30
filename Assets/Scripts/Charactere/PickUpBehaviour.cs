using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehaviour : MonoBehaviour
{   
    [SerializeField]private Inventory _inventory;
    [SerializeField]private Animator _animator;
    [SerializeField]MoveBehaviour _moveBehaviour; 

    private Item _currentItem;
    
   public void DoPickUp(Item item)
    {
        if(_inventory.IsFull())
        {
            Debug.Log("Inventory Full, can't pick up : " + item.name);
            return;
        }

        _currentItem = item;
        //jouer l'animation de ramassage
        _animator.SetTrigger("Pickup");
        // bloquer le deplacement pendant le ramassage
        _moveBehaviour.CanMove = false;
        
    }

    public void AddItemToInventory()
    {
        //ajouter l'item a l'inventaire du joueur
        _inventory.AddIten(_currentItem.Itemdata);
        //detruit le game object 
        Destroy(_currentItem.gameObject);
        //réinitialise la variable courante
        _currentItem = null;
    }

    public void ReEnablePlayerMouvement()
    {
        //debloque le deplacement
        _moveBehaviour.CanMove = true;
    }
}
