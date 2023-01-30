using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] ItemsData _item;

    public ItemsData Itemdata { get => _item; set => _item = value; }
}
