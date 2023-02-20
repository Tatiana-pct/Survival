/////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////// Script responsable des vérification des connections ///////////////////
///////////////////////////// Script responsible for verifying connections///////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

public class CollisionDetectionEdge : MonoBehaviour
{
    [Header("Point References")]
    [SerializeField] float _raduis;
    [SerializeField] Vector3 _centerOffset;
    [SerializeField] PointDetectionEdge[] _detectionPoint;
    [SerializeField] MeshRenderer _meshRenderer;

    private Collider[] _hitColliders;

    public MeshRenderer MeshRenderer { get => _meshRenderer; set => _meshRenderer = value; }

    #region CheckConnection
    //Methode qui verifie la connection des quatre points de la structure constructible et/ou de la structure entiere
    //Method that checks the connection of the four points of the buildable structure and/or the whole structure
    public bool CheckConnection()
    {
        _hitColliders = Physics.OverlapSphere(transform.position + _centerOffset, _raduis);
        //Verification pour la stucture
        //Verification for the structure
        if (_hitColliders.Length > 0)
        {
            foreach (var collider in _hitColliders)
            {
                //Si l'element est connecter a lui meme
                //If the element is connected to itself
                if (collider.CompareTag(transform.tag))
                {
                    return false;
                }
                //Si l'élément est connecter au terrain
                //If the element is connected to the field
                else if (collider.CompareTag("Terrain"))
                {
                    return true;
                }
            }
        }
        //Vérification pour chaques points
        //Check for each points
        foreach (var point in _detectionPoint)
        {
            point.CheckOverlop();
            if(point.Connected)
            {
                return true;
            }
        }
        return false;

    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + _centerOffset, _raduis);
    }
}
