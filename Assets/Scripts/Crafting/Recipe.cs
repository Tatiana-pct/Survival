////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsable del'affichge dynamique des recettes////////////////////////////////////////
//////////////////////////Script responsible for the dynamic display of recipes/////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static Inventory;

public class Recipe : MonoBehaviour
{
    [SerializeField] Image _craftableImage;
    [SerializeField] GameObject _elementRequiredPrefab;
    [SerializeField] Transform _elementRequiredParent;
    [SerializeField] Button _craftButton;
    [SerializeField] Sprite _canBuildIcon;
    [SerializeField] Sprite _cantBuildIcon;
    [SerializeField] Color _missingColor;
    [SerializeField] Color _avaibleColor;

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

        //Slot permettant l'affichage du tooltip lorsqu'on lui passe un item
        //Slot allowing the display of the tooltip when an item is passed to it
        _craftableImage.transform.parent.GetComponent<Slot>().Item = recipe.CraftableItem;

        bool canCraft = true;

        for (int i = 0; i < recipe.RequiredItems.Length; i++)
        {
            //r�cup�re tous les �l�ments n�c�ssaire pour la recette
            //get all the elements needed for the recipe
            GameObject requiredItemGameObject = Instantiate(_elementRequiredPrefab, _elementRequiredParent);
            Image requiredItemGameObjectImage = requiredItemGameObject.GetComponent<Image>();
            ItemsData requiredItem = recipe.RequiredItems[i]._itemsData;
            ElementRequired elementRequired = requiredItemGameObjectImage.GetComponent<ElementRequired>();

            //Slot permettant l'affichage du tooltip lorsqu'on lui passe un item
            //Slot allowing the display of the tooltip when an item is passed to it
            requiredItemGameObject.GetComponent<Slot>().Item = requiredItem;

            //si la copie d'inventaire contient l'�l�ment requis one le retire de l'inventaire et on passe au suivant
            //if the inventory copy contains the required item one removes it from the inventory and we move on to the next one
            ItemInInventory itemInInventoryCopy = Inventory._instance.GetContent().Where(elem => elem._itemsData == requiredItem).FirstOrDefault();

            if (itemInInventoryCopy != null && itemInInventoryCopy.count>= recipe.RequiredItems[i].count)
            {
                requiredItemGameObjectImage.color = _avaibleColor;
            }
            else
            {
                
                requiredItemGameObjectImage.color = _missingColor;
                canCraft = false;
            }

            //Configure le visuel de l'�l�ment requis
            //Configure the visual of the required element
            elementRequired.ElementImage.sprite = recipe.RequiredItems[i]._itemsData.Visual;
            elementRequired.ElementCountTxt.text = recipe.RequiredItems[i].count.ToString();
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


    #region CraftItem
    //Methode qui ajoute l'�lement crafter a l'inventaire
    //Method that adds the crafter item to the inventory
    public void CraftItem()
    {
        for (int i = 0; i < _currentRecipe.RequiredItems.Length; i++)
        {
            Inventory._instance.RemoveItem(_currentRecipe.RequiredItems[i]._itemsData, _currentRecipe.RequiredItems[i].count);
        }
        Inventory._instance.AddItem(_currentRecipe.CraftableItem);
    }
    #endregion
}
