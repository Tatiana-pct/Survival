/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////// Ce Script definis l'interaction avec les items destructible et collectible //////////////////////
/////////////////////// This Script defines the interaction with the destructible and collectible items /////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Harvestable : MonoBehaviour
{
    [Header("% Loot")]
    [SerializeField] Ressouces[] _harvestableItem;

    [Header("Options")]
    [SerializeField] Tool _tool;
    [SerializeField] bool _disableKinematicOnHarvest;
    [SerializeField] float _destroyDelay;

    public Ressouces[] HarvestableItem { get => _harvestableItem; set => _harvestableItem = value; }
    public bool DisableKinematicOnHarvest { get => _disableKinematicOnHarvest; set => _disableKinematicOnHarvest = value; }
    public float DestroyDelay { get => _destroyDelay; set => _destroyDelay = value; }
    public Tool Tool { get => _tool; set => _tool = value; }
}





[System.Serializable]
public class Ressouces
{
    [SerializeField] ItemsData _itemData;

    [Range(0,100)]
    [SerializeField] int _dropChance;
    

    public ItemsData ItemData { get => _itemData; set => _itemData = value; }
    public int DropChance { get => _dropChance; set => _dropChance = value; }
}

public enum Tool
{
    PickAxe,
    Axe
}
