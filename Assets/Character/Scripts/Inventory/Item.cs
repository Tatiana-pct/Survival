using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemData _item;

    public ItemData Itemdata { get => _item; set => _item = value; }
}
