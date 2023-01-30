//ce script ScriptableObject sert a la creation d'un item
//this ScriptableObject script is used to create an item

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Items_new item")]
public class ItemsData : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] Sprite _visual;
    [SerializeField] GameObject _prefab;

    public Sprite Visual { get => _visual; set => _visual = value; }
    public string Description { get => _description; set => _description = value; }
    public string Name { get => _name; set => _name = value; }
}
