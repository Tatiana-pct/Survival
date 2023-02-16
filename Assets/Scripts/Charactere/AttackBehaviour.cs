/////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////Script responsable des mouvement d'attaque du joueur/////////////////////
////////////////////////////Script responsible for player's attack moves/////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Equipement _equipementSystem;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && _equipementSystem.EquipementWeaponItem != null)
        {
            Debug.Log("Attack1");
            _animator.SetTrigger("Attack");
            Debug.Log("Attack2");
        }
    }
}
