using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] KeyManager.KeyItem _requiredKey;
    
    private const string idle = "chest";
    private const string open = "open_chest";
    private string _currentState;
    Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        _currentState = idle;
    }

    protected override void Interact()
    {
        if (KeyManager.Instance.CheckRequiredItem(_requiredKey)) {
            StartCoroutine(PlayAnimationAndChangeState());
        }
    }


    private IEnumerator PlayAnimationAndChangeState()
    {
        ChangeAnimationState(open);

        // Wait until the animation is complete
        while (IsAnimationPlaying(_animator, open))
        {
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        GetComponent<PickUpSpawner>().DropItems();
        Destroy(gameObject);
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
