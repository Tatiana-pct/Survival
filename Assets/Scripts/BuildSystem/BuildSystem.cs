//////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////// Script Moteur du system de construction ////////////////////////////
///////////////////////////// Build System Engine Script              ////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////
using System.Linq;
using UnityEngine;
public enum StructureType
{
    Stairs,
    Wall,
    Floor
}

#region class Structure
[System.Serializable]
public class Structure
{
    [SerializeField] GameObject _placementPrefab;
    [SerializeField] GameObject _instantiatedPrefab;
    [SerializeField] StructureType _structureType;

    public GameObject PlacementPrefab { get => _placementPrefab; set => _placementPrefab = value; }
    public StructureType StructureType { get => _structureType; set => _structureType = value; }
    public GameObject InstantiatedPrefab { get => _instantiatedPrefab; set => _instantiatedPrefab = value; }
}
#endregion

public class BuildSystem : MonoBehaviour
{
    [Header("Script Grid references")]
    [SerializeField] Grid _grid;

    [Header("Structures References")]
    [SerializeField] Structure[] _structures;

    [Header("Color Materials References")]
    [SerializeField] Material _blueMat;
    [SerializeField] Material _redMat;

    [Header("Camera References")]
    [SerializeField] Transform _rotationRef;


    private StructureType _currentStructureType;
    private bool _canBuild;
    private Vector3 _finalePosition;
    private bool _inPlace;

    private void FixedUpdate()
    {
        //Permet de savoir SI on peut ou non contruire
        //Allows you to know whether or not you can build
        _canBuild = GetCurrentStructure().PlacementPrefab.GetComponentInChildren<CollisionDetectionEdge>().CheckConnection();

        //Permet de savoir OU on peut construire
        //Allows you to know where we can build
        _finalePosition = GetNearestPoint(transform.position);

        CheckPosition();
        RoundPlacementStructureRotation();
        UpdatePlacementStructureMaterial();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeStructureType(StructureType.Stairs);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeStructureType(StructureType.Wall);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeStructureType(StructureType.Floor);
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)&&_canBuild &&_inPlace)
        {
            Instantiate(GetCurrentStructure().InstantiatedPrefab,
                GetCurrentStructure().PlacementPrefab.transform.position,
                GetCurrentStructure().PlacementPrefab.transform.GetChild(0).transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateStructure();
        }
    }

    #region  ChangeStructureType
    //Methode qui gere le changement de structure
    //Method that manages the structure change
    void ChangeStructureType(StructureType newType)
    {
        _currentStructureType = newType;
        foreach (var structure in _structures)
        {
            structure.PlacementPrefab.SetActive(structure.StructureType == newType);
        }
    }
    #endregion

    #region GetStructureOfType
    //methode qui renvoie un Objet Structure
    //method that returns a Structure Object
    private Structure GetCurrentStructure()
    {
        return _structures.Where(elem => elem.StructureType == _currentStructureType).FirstOrDefault();
    }
    #endregion

    #region GetNearestPoint
    Vector3 GetNearestPoint(Vector3 referencePoint)
    {
        return _grid.GetNearestPointOnGrid(referencePoint);
    }
    #endregion

    #region CheckPosition
    //Methode qui permet de verifier si l'element et bien placé ou non et de le placer au bon endroit
    //Method to check if the element is well placed or not and to place it in the right place
    void CheckPosition()
    {
        _inPlace = GetCurrentStructure().PlacementPrefab.transform.position == _finalePosition;

        if (!_inPlace)
        {
            setPosition(_finalePosition);
        }

    }
    #endregion setPosition

    #region setPosition
    //Methode qui permet de deplace l'element actuel vers sa position constructible la plus proche
    //Method that moves the current element to its nearest buildable position
    void setPosition(Vector3 targetPosition)
    {
        Transform PlacementPrefabtransform = GetCurrentStructure().PlacementPrefab.transform;
        Vector3 positionVelocity = Vector3.zero;
        Vector3 newTargetPosition = Vector3.SmoothDamp(PlacementPrefabtransform.position, targetPosition, ref positionVelocity, 0, 15000);
        PlacementPrefabtransform.position = newTargetPosition;
    }
    #endregion

    #region  UpdatePlacementStructureMaterial
    //Methode qui met à jour la couleur du material si l'endroit est constructible ou non
    //Method that updates the color of the material if the place is buildable or not
    void UpdatePlacementStructureMaterial()
    {
        MeshRenderer placementPrefabrenderer = GetCurrentStructure().PlacementPrefab.GetComponentInChildren<CollisionDetectionEdge>().MeshRenderer;
        if (_inPlace && _canBuild)
        {
            placementPrefabrenderer.material = _blueMat;

        }
        else
        {
            placementPrefabrenderer.material = _redMat;
        }
    }
    #endregion

    #region RotateStructure
    //Methode qui permet de pivoter des stucture constructible
    //Method that allows to rotate constructible structures
    void RotateStructure()
    {
        if (_currentStructureType != StructureType.Wall)
        {
            GetCurrentStructure().PlacementPrefab.transform.GetChild(0).transform.Rotate(0, 90, 0);
        }
    }
    #endregion

    #region RoundPlacementStructureRotation
    //Methode qui permet de verifier si la rotation de l'objet constructible est bonne
    //Method which allows to check if the rotation of the constructible object is correct
    void RoundPlacementStructureRotation()
    {
        //Variable local stoquant l'angle de la Camera
        //Local variable storing the Camera angle
        float Yangle = _rotationRef.localEulerAngles.y;

        int roundedRotation =0;

        if(Yangle > -45 && Yangle <= 45)
        {
            roundedRotation = 0;
        }
        else if(Yangle > 45 && Yangle <= 135)
        {
            roundedRotation = 90;
        }
        else if (Yangle > 135 && Yangle <= 225)
        {
            roundedRotation = 180;
        }
        else if (Yangle > 225 && Yangle <= 315)
        {
            roundedRotation = 270;
        }

        GetCurrentStructure().PlacementPrefab.transform.rotation = Quaternion.Euler(0, roundedRotation, 0);


    }
    #endregion

}

