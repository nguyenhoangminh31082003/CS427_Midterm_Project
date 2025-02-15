using System;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    [SerializeField] private float attackAnimationSpeed = 1.0f;
    public float triggerInterval = 2.0f;  // Time between triggers
    private float timer = 0.0f;           // Timer to keep track of interval
    // public bool isTriggered;              // Whether the trap is currently triggered
    private bool damaged;
    private bool isIn;
    private Animator _animator;
    private string _currentState;
    private GameManager gameManager;
    private MainCharacter mainCharacter;
    protected abstract string AttackState { get; }
    protected abstract string SleepState { get; }

    void Awake() {
        _animator = GetComponent<Animator>();
        _animator.speed = attackAnimationSpeed;        
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        mainCharacter = MainCharacter.Instance;
        isIn = false;
        damaged = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= triggerInterval)
        {
            ChangeAnimationState(AttackState);
            timer = 0.0f;
        }

        if (IsAnimationPlaying(_animator, AttackState))
        {
            if (isIn && !damaged)
            {
                damaged = true;
                Debug.Log("Deal Damage to player");
                // Call manager to deal damage
                gameManager.CollisionHandler(mainCharacter.transform.tag, mainCharacter.transform.name, this.tag, this.name);
            }
        }
        else
        {
            _currentState = SleepState;
            damaged = false;
        }

        
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) {
            isIn = true;
        }   
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) {
            isIn = false;
        }
    }
    private void ChangeAnimationState(string newState)
    {
        if (newState == _currentState)
        {
            return;
        }
        _animator.Play(newState);
        _currentState = newState;
    }

    private bool IsAnimationPlaying(Animator animator, string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
    }
}
