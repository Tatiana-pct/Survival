////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsable del'affichge dynamique des recettes////////////////////////////////////////
//////////////////////////Script responsible for the dynamic display of recipes/////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
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
            //récupère tous les éléments nécéssaire pour la recette
            //get all the elements needed for the recipe
            GameObject requiredItemGameObject = Instantiate(_elementRequiredPrefab, _elementRequiredParent);
            Image requiredItemGameObjectImage = requiredItemGameObject.GetComponent<Image>();
            ItemsData requiredItem = recipe.RequiredItems[i]._itemsData;
            ElementRequired elementRequired = requiredItemGameObjectImage.GetComponent<ElementRequired>();

            //Slot permettant l'affichage du tooltip lorsqu'on lui passe un item
            //Slot allowing the display of the tooltip when an item is passed to it
            requiredItemGameObject.GetComponent<Slot>().Item = requiredItem;

            //si l'inventaire contient l'élément requis on le retire de l'inventaire et on passe au suivant
            //if the inventory contains the required element, remove it from the inventory and move on to the next one
            ItemInInventory[] itemInInventory = Inventory._instance.GetContent().Where(elem => elem._itemsData == requiredItem).ToArray();

            //variable temporaire
            int totalRequiredItemQuantityInInventory = 0;

            for (int y = 0; y < itemInInventory.Length; y++)
            {
                totalRequiredItemQuantityInInventory += itemInInventory[y].count;
            }

            if (totalRequiredItemQuantityInInventory >= recipe.RequiredItems[i].count)
            {
                requiredItemGameObjectImage.color = _avaibleColor;
            }
            else
            {

                requiredItemGameObjectImage.color = _missingColor;
                canCraft = false;
            }

            //Configure le visuel de l'élément requis
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
    //Methode qui ajoute l'élement crafter a l'inventaire
    //Method that adds the crafter item to the inventory
    public void CraftItem()
    {
        for (int i = 0; i < _currentRecipe.RequiredItems.Length; i++)
        {
            for (int y = 0; y < _currentRecipe.RequiredItems[i].count; y++)
            {
                Inventory._instance.RemoveItem(_currentRecipe.RequiredItems[i]._itemsData);

            }
        }
        Inventory._instance.AddItem(_currentRecipe.CraftableItem);
    }
    #endregion
}
