/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsable de la partie Ui/////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsible for the dynamic display of recipes//////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;


public class UiManager : MonoBehaviour
{

    [SerializeField] GameObject[] _UiPanel;
    [SerializeField] ThirdPersonOrbitCamBasic _playerCamScript;
    [SerializeField] bool _atLestOnePanelOpend;

    private float _defaultHorizontalAnimgSpeed;
    private float _defaultVerticalAnimgSpeed;

    public bool AtLestOnePanelOpend { get => _atLestOnePanelOpend; set => _atLestOnePanelOpend = value; }


    // Start is called before the first frame update
    void Start()
    {
        _defaultHorizontalAnimgSpeed = _playerCamScript.horizontalAimingSpeed;
        _defaultVerticalAnimgSpeed = _playerCamScript.verticalAimingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _atLestOnePanelOpend = _UiPanel.Any((panel) => panel == panel.activeSelf);

        if (_atLestOnePanelOpend)
        {
            _playerCamScript.horizontalAimingSpeed = 0;
            _playerCamScript.verticalAimingSpeed = 0;
        }
        else
        {
            _playerCamScript.horizontalAimingSpeed = _defaultHorizontalAnimgSpeed;
            _playerCamScript.verticalAimingSpeed = _defaultVerticalAnimgSpeed;
        }
    }
}
