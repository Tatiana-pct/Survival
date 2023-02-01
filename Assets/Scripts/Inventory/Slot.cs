//ce script sert pour l'affichage des tooltip au passage de la souris
//this script is used for displaying tooltips on mouseover


using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] ItemsData _item;
    [SerializeField] Image _itemVisual;

    public ItemsData Item { get => _item; set => _item = value; }
    public Image ItemVisual { get => _itemVisual; set => _itemVisual = value; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_item != null)
        {
            TooltipSystem._instance.Show(_item.Description, _item.Name);

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem._instance.Hide();
    }

    public void ClickOnSlot()
    {
        Inventory._instance.OpenActionPanel(Item, transform.position);
    }

}
