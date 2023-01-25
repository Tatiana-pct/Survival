using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Items_new item")]
public class ItemData : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] Sprite _virual;
    [SerializeField] GameObject _prefab;


}
