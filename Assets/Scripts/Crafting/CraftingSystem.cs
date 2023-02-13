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
    [SerializeField] KeyCode _openCraftPanelInput;   
    [SerializeField] GameObject _craftPanel;   

    void Start()
    {
        UpdateDisplayRecipes();
        
    }

    private void Update()
    {
        //Ouvre ou ferme le panel de craft si la player appuie sur le btn
        //Open or close the craft panel if the player presses the btn
        if (Input.GetKeyDown(_openCraftPanelInput))
        {
            _craftPanel.SetActive(!_craftPanel.activeSelf);
            UpdateDisplayRecipes();
        }
    }

    #region UpdateDisplayRecipes
    public void UpdateDisplayRecipes()
    {
        //Supprime les recettes list�es
        foreach ( Transform child in _recipesParent)
        {
            Destroy(child.gameObject);
        }

        //recr�� les recettes list�es
        for (int i = 0; i < _availableRecipes.Length; i++)
        {
           GameObject recipe = Instantiate(_recipeUiPrefab, _recipesParent);
            recipe.GetComponent<Recipe>().Configure(_availableRecipes[i]);
        }
    }
    #endregion
}
