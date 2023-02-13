//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsable des recettes de crafts/////////////////////////////////////////////////////
//////////////////////////Script responsible for crafting recipes/////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{

    [SerializeField] RecipeData[] _availableRecipes;   
    [SerializeField] GameObject _recipeUiPrefab;   
    [SerializeField] Transform _recipesParent;   

    void Start()
    {
        UpdateDisplayRecipes();
        
    }

    #region UpdateDisplayRecipes
    private void UpdateDisplayRecipes()
    {
        //Supprime les recettes listées
        foreach ( Transform child in _recipesParent)
        {
            Destroy(child.gameObject);
        }

        //recréé les recettes listées
        for (int i = 0; i < _availableRecipes.Length; i++)
        {
           GameObject recipe = Instantiate(_recipeUiPrefab, _recipesParent);
            recipe.GetComponent<Recipe>().Configure(_availableRecipes[i]);
        }
    }
    #endregion
}
