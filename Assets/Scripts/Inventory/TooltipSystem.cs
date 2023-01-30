//ce script sert pour l'affichage des tooltip
//this script is used for displaying tooltips

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem _instance;

    [SerializeField]private Tooltip _tooltip;

   

    private void Awake()
    {
        _instance = this;
    }

    public void Show(string content, string header = "")
    {
        _tooltip.SetText(content, header);
        _tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _tooltip.gameObject.SetActive(false);
    }
}
