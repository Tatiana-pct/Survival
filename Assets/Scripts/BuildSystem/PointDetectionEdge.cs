/////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////// Script responsable des point de connection des constructibles //////////
//////////////////////////// Script responsible for constructible connection points//////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

public class PointDetectionEdge : MonoBehaviour
{
    [Header("Point References")]
    [SerializeField] bool _connected;
    [SerializeField] float _raduis = 0.6f;
    [SerializeField] Collider[] _hitColliders;



    public bool Connected { get => _connected; set => _connected = value; }
    public float Raduis { get => _raduis; set => _raduis = value; }
    public Collider[] HitColliders { get => _hitColliders; set => _hitColliders = value; }

    private void OnDisable()
    {
        _connected= false;
    }

    #region CheckOverlap
    // Methode qui verifie si un point et connecter ou non
    //Method that checks if a point is connected or not
    public void CheckOverlop()
    {
        Connected = false;

        _hitColliders = Physics.OverlapSphere(transform.position, _raduis);
        if(_hitColliders.Length > 0)
        {
            foreach (var collider in _hitColliders)
            {
                if(collider.CompareTag("Point") || collider.CompareTag("Terrain"))
                {
                    _connected = true;
                    return;
                }
            }
        }
        else
        {
            _connected = false;
        }

    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _raduis);
    }
}
