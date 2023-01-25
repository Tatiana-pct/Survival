using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    //Variable de distance de ramassage d'un item
    [SerializeField]private float _pickUpRange = 2.6f;

    [SerializeField]Inventory _inventory;

    [SerializeField]PickUpBehaviour  _playerPickupBehaviour;

    // Update is called once per frame
    void Update()
    {
        RaycastHit _hit;

        if(Physics.Raycast(transform.position, transform.forward, out _hit , _pickUpRange))
        {
            if(_hit.transform.CompareTag("Item"))
            {
                Debug.Log("there is an item in front of us");
                if(Input.GetKeyDown(KeyCode.E))
                {
                    _playerPickupBehaviour.DoPickUp(_hit.transform.gameObject.GetComponent<Item>());
                }
            }
        }

    }
}
