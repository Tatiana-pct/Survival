//Script responsable du ramassage des Objs 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    //Variable de distance de ramassage d'un item
    [SerializeField]private float _pickUpRange = 2.6f;
    [SerializeField]Inventory _inventory;
    [SerializeField]PickUpBehaviour  _playerPickupBehaviour;
    [SerializeField]LayerMask _layerMask;
    [SerializeField] GameObject _pickUpTxt;

    // Update is called once per frame
    void Update()
    {
        RaycastHit _hit;

        if(Physics.Raycast(transform.position, transform.forward, out _hit , _pickUpRange, _layerMask))
        {
            if(_hit.transform.CompareTag("Item"))
            {
                _pickUpTxt.SetActive(true);
                
                if(Input.GetKeyDown(KeyCode.E))
                {
                    _playerPickupBehaviour.DoPickUp(_hit.transform.gameObject.GetComponent<Item>());
                }
            }
        }
        else
        {
            _pickUpTxt.SetActive(false);
        }

    }
}
