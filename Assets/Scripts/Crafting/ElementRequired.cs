using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementRequired : MonoBehaviour
{
    [SerializeField] Image _elementImage;
    [SerializeField] Text _elementCountTxt;

    public Image ElementImage { get => _elementImage; set => _elementImage = value; }
    public Text ElementCountTxt { get => _elementCountTxt; set => _elementCountTxt = value; }
}
