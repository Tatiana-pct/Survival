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
    [SerializeField] Ressouces[] _harvestableItem;

    public Ressouces[] HarvestableItem { get => _harvestableItem; set => _harvestableItem = value; }
}

[System.Serializable]
public class Ressouces
{
    [SerializeField] ItemsData _itemData;
    [SerializeField] int _minAmoutSpwane =2;
    [SerializeField] int _maxAmoutSpwane =5;

    public ItemsData ItemData { get => _itemData; set => _itemData = value; }
    public int MinAmoutSpwane { get => _minAmoutSpwane; set => _minAmoutSpwane = value; }
    public int MaxAmoutSpwane { get => _maxAmoutSpwane; set => _maxAmoutSpwane = value; }
}
