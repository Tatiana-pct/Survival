/////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsable des stats du joueur/////////////////////////////////////
//////////////////////////Script responsible for player stats   /////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("MoveBehaviour Script references")]
    [SerializeField]MoveBehaviour _playerMouvementScript;
    
    [Header("AIMBehaviour Script references")]
    [SerializeField]AimBehaviourBasic _PlayeeAimBehaviourBasic;

    [Header("Other elements references")]
    [SerializeField] Animator _animator;

    [Header("Health")]
    [SerializeField] float _maxHealth = 100f;
    [SerializeField] float _currentHealth;
    [SerializeField] Image _healthBarFill;

    [Header("HealthDescreas X2")]
    [SerializeField] float _healthDecreaseRateForHungerAndThrist;

    [Header("Hunger")]
    [SerializeField] float _maxHunger = 100f;
    [SerializeField] float _currentHunger;
    [SerializeField] Image _hungerBarFill;
    [SerializeField] float _hungerDecreaseRate;

    [Header("Thirst")]
    [SerializeField] float _maxThirst = 100f;
    [SerializeField] float _currentThirst;
    [SerializeField] Image _thirstBarFill;
    [SerializeField] float _thirstDecreaseRate;

    [Header("Armor Points")]
    [SerializeField] float _currentArmorPoints;

    private bool _isDead = false;

    public float CurrentArmorPoints { get => _currentArmorPoints; set => _currentArmorPoints = value; }
    public bool IsDead { get => _isDead; set => _isDead = value; }



    // Start is called before the first frame update
    void Awake()
    {
        _currentHealth = _maxHealth;
        _currentHunger = _maxHunger;
        _currentThirst = _maxThirst;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHungerAndThirstBarFill();
        UpdateHealthBarFill();

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(50f);
        }
    }

    #region TakeDamage
    //Methode qui gere la prise de dégâts du joueur
    //Method that manages the player's damage taking
    public void TakeDamage(float damage, bool overTime = false)
    {
        if (overTime)
        {
            //perte de la vie au cours du temps qui passe(faim/soif)
            //loss of life over time (hunger/thirst)
            _currentHealth -= damage * Time.deltaTime;
        }
        else
        {
            //perte de la vie via des dégâts classique
            // loss of life via classic damage
            _currentHealth -= (damage) * (1 - (_currentArmorPoints / 100));
        }

        if (_currentHealth <= 0 && !_isDead)
        {
            Die();
        }

        UpdateHealthBarFill();
    }
    #endregion

    #region Die
    void Die()
    {
        Debug.Log("Player died");
        _isDead = true;

        //On bloque le mouvement du joueur + mode inspection
        //We block the movement of the player + inspection mode
        _playerMouvementScript.CanMove = false;
        _PlayeeAimBehaviourBasic.enabled = false;

        //On bloque la diminution des barres de faim/soif 
        //We block the reduction of hunger/thirst bars
        _hungerDecreaseRate = 0;
        _thirstDecreaseRate = 0;
        _animator.SetTrigger("Die");
    }

    #endregion

    #region UpdateHealthBarFill
    //Methode qui met a jour le visuel de la bar de vie
    //Method that updates the visual of the life bar
    void UpdateHealthBarFill()
    {
        _healthBarFill.fillAmount = _currentHealth / _maxHealth;

    }
    #endregion

    #region UpdateHungerAndThirstBarFill
    //Methode qui gere l'evolution de la bar de faim et de soif du player
    //Method that manages the evolution of the player's hunger and thirst bar
    void UpdateHungerAndThirstBarFill()
    {

        //Dimunue la faim/ soif au fil du temps
        //Decreases hunger/thirst over time
        _currentHunger -= _hungerDecreaseRate * Time.deltaTime;
        _currentThirst -= _thirstDecreaseRate * Time.deltaTime;

        //On empeche de passer dans le negatif
        //We prevent going into the negative
        _currentHunger = _currentHunger < 0 ? 0 : _currentHunger;
        _currentThirst = _currentThirst < 0 ? 0 : _currentThirst;

        //Met a jour les visuels
        //Update the visuals
        _hungerBarFill.fillAmount = _currentHunger / _maxHunger;
        _thirstBarFill.fillAmount = _currentThirst / _maxThirst;

        //Si la faim / soif est a zero alors on retire des pts de vie (x2 si les deux barres sont a zero)
        //If hunger/thirst is zero then life pts are removed (x2 if both bars are at zero)
        if (_currentHunger <= 0 || _currentThirst <= 0)
        {
            TakeDamage((_currentHunger <= 0 && _currentThirst <= 0 ? _healthDecreaseRateForHungerAndThrist * 2 : _healthDecreaseRateForHungerAndThrist), true);
        }
    }
    #endregion

    #region ConsumeItem
    //Methode qui gere la consommation d'item
    //Method that handles item consumption
    public void ConsumeItem(float health, float hunger, float thirst)
    {
        _currentHealth += health;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        _currentHunger += hunger;
        if (_currentHunger > _maxHunger)
        {
            _currentHunger = _maxHunger;
        }

        _currentThirst += thirst;
        if (_currentThirst > _maxThirst)
        {
            _currentThirst = _maxThirst;
        }
        UpdateHealthBarFill();
    }
    #endregion


}
