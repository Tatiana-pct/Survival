using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Items_new item")]
public class ItemsData : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] Sprite _visual;
    [SerializeField] GameObject _prefab;

    public Sprite Visual { get => _visual; set => _visual = value; }
}
