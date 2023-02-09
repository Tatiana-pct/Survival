////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////// //Script responsable du ramassage des Objs et du farm///////////////////////////////////////////
/////////////////////// //Script responsible for collecting Objs and farming ///////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class Interact : MonoBehaviour
{
    //Variable de distance de ramassage d'un item
    [SerializeField] private float _interactRange = 2.6f;
    [SerializeField] Inventory _inventory;
    [SerializeField] InteractBeheviour _playerInteractBehaviour;
    [SerializeField] LayerMask _layerMask;
    [SerializeField] GameObject _interactText;

    // Update is called once per frame
    void Update()
    {
        RaycastHit _hit;

        if (Physics.Raycast(transform.position, transform.forward, out _hit, _interactRange, _layerMask))
        {
            _interactText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_hit.transform.CompareTag("Item"))
                {
                    _playerInteractBehaviour.DoPickUp(_hit.transform.gameObject.GetComponent<Item>());

                }
                if (_hit.transform.CompareTag("Harvestable"))
                {
                    _playerInteractBehaviour.DoHarvest(_hit.transform.gameObject.GetComponent<Harvestable>());

                }

            }

        }
        else
        {
            _interactText.SetActive(false);
        }

    }
}
