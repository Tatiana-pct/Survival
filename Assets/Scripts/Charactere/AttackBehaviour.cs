/////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////Script responsable des mouvement d'attaque du joueur/////////////////////
////////////////////////////Script responsible for player's attack moves/////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    [Header("Animator reference")]
    [SerializeField] Animator _animator;

    [Header("Equipement Script reference")]
    [SerializeField] Equipement _equipementSystemScript;

    [Header("UiManagezr Script reference")]
    [SerializeField] UiManager _UiManagerScript;

    [Header("InteractBeheviour Script reference")]
    [SerializeField] InteractBeheviour _interactBeheviour;

    [Header("Configuration")]
    [SerializeField] float _attackRange;
    [SerializeField] LayerMask _layermaskToTouch;
    [SerializeField] Vector3 _attackOffset;

    //Variable du cooldown d'attaque
    //Attack Cooldown Variable
    bool _isAttacking;
    

    private void Update()
    {
        //Debug.DrawRay(transform.position + _attackOffset, transform.forward * _attackRange, Color.red);

        if(Input.GetMouseButtonDown(0) && CanAttack())
        {
            _isAttacking = true;
            SendAttack();
            _animator.SetTrigger("Attack");
        }
    }

    #region SendAttack
    //Methode qui permet d'envoyer des attaques
    void SendAttack()
    {
       
        RaycastHit hit;
        if(Physics.Raycast(transform.position + _attackOffset, transform.forward, out hit, _attackRange, _layermaskToTouch))
        {
            if(hit.transform.CompareTag("AI"))
            {
                EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
                enemy.TakeDammage(_equipementSystemScript.EquipementWeaponItem.AttackPoints);
            }
        }
    }
    #endregion

    #region AttackinFinished
    public void AttackFinished()
    {
        _isAttacking = false;
    }
    #endregion

    #region
    /*
     Pour attaquer on doit:
     Avoir une arme équiper
     Ne pas etre en train t'attaquer
    Ne pas avoir l'inventaire ouvert

     */
    bool CanAttack()
    {
        return _equipementSystemScript.EquipementWeaponItem != null && !_isAttacking && !_UiManagerScript.AtLestOnePanelOpend && !_interactBeheviour.IsBusy;
    }
    #endregion



}
