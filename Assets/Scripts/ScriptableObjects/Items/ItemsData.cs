//ce script ScriptableObject sert a la creation d'un item
//this ScriptableObject script is used to create an item

using UnityEngine;
public enum ItemType
{
    Ressouce,
    Equipement,
    Consumable
}

public enum EquipementType
{
    Head,
    Chest,
    hands,
    legs,
    feets
}

[CreateAssetMenu(fileName = "Item", menuName = "Items_new item")]
public class ItemsData : ScriptableObject
{

    [Header("Data")]
    [SerializeField] string _name;
    [SerializeField] string _description;
    [SerializeField] Sprite _visual;
    [SerializeField] GameObject _prefab;
    [SerializeField] bool _stackable;
    [SerializeField] int _maxStack;

    [Header("Effects")]
    [SerializeField] float _healthEffect;
    [SerializeField] float _hungerEffect;
    [SerializeField] float _thristEffect;

   

    [Header("Type")]
    [SerializeField] ItemType _itemType;
    [SerializeField] EquipementType _equipementType;

    public Sprite Visual { get => _visual; set => _visual = value; }
    public string Description { get => _description; set => _description = value; }
    public string Name { get => _name; set => _name = value; }
    public ItemType ItemType { get => _itemType; set => _itemType = value; }
    public GameObject Prefab { get => _prefab; set => _prefab = value; }
    public EquipementType EquipementType { get => _equipementType; set => _equipementType = value; }
    public bool Stackable { get => _stackable; set => _stackable = value; }
    public int MaxStack { get => _maxStack; set => _maxStack = value; }
    public float HealthEffect { get => _healthEffect; set => _healthEffect = value; }
    public float HungerEffect { get => _hungerEffect; set => _hungerEffect = value; }
    public float ThristEffect { get => _thristEffect; set => _thristEffect = value; }
}
