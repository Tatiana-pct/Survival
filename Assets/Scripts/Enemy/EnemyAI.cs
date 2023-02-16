/////////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////Script responsable du l'AI de l'ennemy/////////////////////////////////////
//////////////////////////Script responsible for enemy AI       /////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _player;
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Animator _animator;
    [SerializeField] PlayerStats _playerStats;

    [Header("Enemy Stats")]
    [SerializeField] float _maxHealth;
    [SerializeField] float _currentHealth;
    [SerializeField] float _walkSpeed;
    [SerializeField] float _chaseSpeed;
    [SerializeField] float _detectionRaduis;
    [SerializeField] float _attackRaduis;
    [SerializeField] float _attackDelay;
    [SerializeField] float _damageDealt;
    [SerializeField] float _rotationSpeed;

    [Header("WaitingAI Parameters")]
    [SerializeField] float _WanderingTimeMin;
    [SerializeField] float _WanderingTimeMax;
    [SerializeField] float _WanderingDistanceMin;
    [SerializeField] float _WanderingDistanceMax;

    private bool _HasDestination;
    private bool _isAttacking;
    private bool _isDead;


    private void Awake()
    {
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _player = playerTransform;
        _playerStats = playerTransform.GetComponent<PlayerStats>();

        _currentHealth = _maxHealth;
    }

    void Update()
    {
        //Rayon de detection de l'ennemie
        //Enemy detection radius
        if (Vector3.Distance(_player.position, transform.position) < _detectionRaduis && !_playerStats.IsDead)
        {
            _agent.speed = _chaseSpeed;
            Quaternion rotation = Quaternion.LookRotation(_player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
            if (!_isAttacking)
            {

                if (Vector3.Distance(_player.position, transform.position) < _attackRaduis)
                {
                    StartCoroutine(AttackPlayer());
                }
                else
                {
                    _agent.SetDestination(_player.position);

                }
            }

        }
        else
        {
            _agent.speed = _walkSpeed;
            if (_agent.remainingDistance < 0.75f && !_HasDestination)
            {
                StartCoroutine(GetNewDestination());
            }
        }
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    #region GetNewDestination
    IEnumerator GetNewDestination()
    {
        _HasDestination = true;
        yield return new WaitForSeconds(Random.Range(_WanderingTimeMin, _WanderingTimeMax));

        Vector3 nextDestination = transform.position;
        nextDestination += Random.Range(_WanderingDistanceMin, _WanderingDistanceMin) * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextDestination, out hit, _WanderingDistanceMax, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
        }
        _HasDestination = false;

    }
    #endregion

    #region AttackPlayer
    //Couroutine permettant à l'ours d'attaquer le joueur
    //Couroutine allowing the bear to attack the player
    IEnumerator AttackPlayer()
    {
        _isAttacking = true;

        //bloque les mouvement de l'ours durant l'attaque
        //block the bear's movements during the attack
        _agent.isStopped = true;

        //Animation d'attaque
        //Animation of attack
        _animator.SetTrigger("Attack");
        _playerStats.TakeDamage(_damageDealt);

        yield return new WaitForSeconds(_attackDelay);

        if(_agent.enabled)
        {
        //Reactive le mouvement de l'ours
        //Reactivate bear movement
        _agent.isStopped = false;

        }
        _isAttacking = false;

    }
    #endregion
    //Methode responsable de la perte des point de vie l'AI
    //Method responsible for the loss of the AI's life points
    #region TakeDammage
    public void TakeDammage(float damages)
    {
        if (_isDead)
        {
            return;
        }
        _currentHealth -= damages;

        if (_currentHealth <= 0f)
        {
            _isDead = true;
            _animator.SetTrigger("Die");
            _agent.enabled = false;
            enabled = false;
        }
        else
        {
            _animator.SetTrigger("GetHit");

        }
    }

    #endregion 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRaduis);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRaduis);
    }
}
