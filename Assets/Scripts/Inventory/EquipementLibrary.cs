using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipementLibrary : MonoBehaviour
{
    [SerializeField] List<EquipementLibraryItem> _content = new List<EquipementLibraryItem>();

    public List<EquipementLibraryItem> Content { get => _content; set => _content = value; }
}

    [System.Serializable]
    public class EquipementLibraryItem
{
    [SerializeField] ItemsData _itemsData;
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] GameObject[] _elementsToDisable;

    public ItemsData ItemsData { get => _itemsData; set => _itemsData = value; }
    public GameObject ItemPrefab { get => _itemPrefab; set => _itemPrefab = value; }
    public GameObject[] ElementsToDisable { get => _elementsToDisable; set => _elementsToDisable = value; }
}