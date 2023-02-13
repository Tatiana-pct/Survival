////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsable del'affichge dynamique des recettes////////////////////////////////////////
//////////////////////////Script responsible for the dynamic display of recipes/////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    [SerializeField] Image _craftableImage;
    [SerializeField] GameObject _elementRequiredPrefab;
    [SerializeField] Transform _elementRequiredParent;
    [SerializeField] Button _craftButton;
    [SerializeField] Sprite _canBuildIcon;
    [SerializeField] Sprite _cantBuildIcon;

    private RecipeData _currentRecipe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Configure
    //Methode qui verifie si l'on peux crafter la recette ou non
    //Method that checks if we can craft the recipe or not
    public void Configure(RecipeData recipe)
    {
        _currentRecipe = recipe;

        _craftableImage.sprite = recipe.CraftableItem.Visual;

        bool canCraft = true;

        List<ItemsData> inventoryCopy = new List<ItemsData>(Inventory._instance.GetContent());
        for (int i = 0; i < recipe.RequiredItems.Length; i++)
        {
            ItemsData requiredItem = recipe.RequiredItems[i]; 
            
            if(inventoryCopy.Contains(requiredItem))
            {
                inventoryCopy.Remove(requiredItem);
            }
            else
            {
                canCraft = false;
            }

            GameObject requiredItemGameObject = Instantiate(_elementRequiredPrefab, _elementRequiredParent);
            requiredItemGameObject.transform.GetChild(0).GetComponent<Image>().sprite = recipe.RequiredItems[i].Visual;
        }
        //Gere l'affichage de Bouton
        //Manage Button display
        _craftButton.image.sprite = canCraft ? _canBuildIcon : _cantBuildIcon;
        _craftButton.enabled = canCraft;

        ResizeElementRequiredParent();
    }
    #endregion

    #region ResizeElementRequiredParent
    //Methode qui permet au buttonCraft de rester a sa place au bout de la recette
    //Method that allows the buttonCraft to stay in place at the end of the recipe
    private void ResizeElementRequiredParent()
    {
        Canvas.ForceUpdateCanvases();
        _elementRequiredParent.GetComponent<ContentSizeFitter>().enabled = false;
        _elementRequiredParent.GetComponent<ContentSizeFitter>().enabled = true;
    }
    #endregion


    #region Craftitem
    //Methode permettant de crafter la recette
    //Method to craft the recipe
    public void Craftitem()
    {
        Inventory._instance.AddItem(_currentRecipe.CraftableItem);
    }
    #endregion
}
