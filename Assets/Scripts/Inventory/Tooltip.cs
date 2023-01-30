//Ce script gere l'affichages des tooltips
//This script manages the display of tooltips

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tooltip : MonoBehaviour
{
    [SerializeField] private Text _headerField;
    [SerializeField] private Text _contentField;
    [SerializeField] private LayoutElement _layoutElement;
    [SerializeField] private int _maxCharacter;

    //[SerializeField]
   

    public void SetText(string content,string header = "")
    {
        if(header == "")
        {
            _headerField.gameObject.SetActive(false);
        }
        else
        {
            _headerField.gameObject.SetActive(true);
            _headerField.text = header;
        }

        _contentField.text = content;
        int headerLenght = _headerField.text.Length;
        int contentLenght = _contentField.text.Length;
        _layoutElement.enabled = ( headerLenght > _maxCharacter || contentLenght > _maxCharacter) ? true : false;
    }

   
}
