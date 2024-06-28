using System;
using UnityEngine;

public abstract class Trap : MonoBehaviour
{
    public float triggerInterval = 2.0f;  // Time between triggers
    private float timer = 0.0f;           // Timer to keep track of interval
    public bool isTriggered;              // Whether the trap is currently triggered
    private Animator _animator;
    private string _currentState;

    protected abstract string AttackState { get; }
    protected abstract string SleepState { get; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isTriggered = false;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= triggerInterval)
        {
            ChangeAnimationState(AttackState);
            timer = 0.0f;
        }

        if (IsAnimationPlaying(_animator, AttackState))
        {
            isTriggered = true;
        }
        else
        {
            ChangeAnimationState(SleepState);
            isTriggered = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        Debug.Log(collision.gameObject.name);
        if (isTriggered && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Deal Damage from enter");
            // You can add damage logic or other effects here if needed
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("OnCollisionStay2D");
        Debug.Log(collision.gameObject.name);
        if (isTriggered && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Deal Damage from stay");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("OnCollisionExit2D");
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            // Additional logic can be added here if needed
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("OnTriggerEnter2D");
        Debug.Log(collider.gameObject.name);
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
