//////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////// Script responsable du systeme de creations de grille ///////////////////
///////////////////////// Script responsible for the grid creation system      ///////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] float _sizeX = 7.2f;
    [SerializeField] float _sizeY = 6f;
    [SerializeField] float _sizeZ = 7.2f;


    #region GetNearestPointOnGrid
    //Methode qui renvoi le point constructible le plus proche de la ou le joueur va regarder
    //Method that returns the closest buildable point to where the player is going to look
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;
        int xCount = Mathf.RoundToInt(position.x / _sizeX);
        int yCount = Mathf.RoundToInt(position.y / _sizeY);
        int zCount = Mathf.RoundToInt(position.z / _sizeZ);

        Vector3 result = new Vector3 (xCount * _sizeX, yCount * _sizeY, zCount * _sizeZ);

        result += transform.position;
        return result;
    }
    #endregion

}
