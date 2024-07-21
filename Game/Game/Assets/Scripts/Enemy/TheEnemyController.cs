using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheEnemyController : MonoBehaviour
{
    private enum State {
        Roaming,
        Attacking
    }
    private State state;
    private TheMovementBase movement;

    private void Awake() {
        movement = GetComponent<TheMovementBase>();
    }

    private void Start() {
        state = State.Roaming;
    }

    private void Update() {
        if (DialogueManager.Instance != null)
        {
            if (DialogueManager.Instance.isDialogueActive) { 
                return; 
            }
        }
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            default:
            case State.Roaming:
                Roaming();
            break;

            case State.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming() {
        movement.Roaming();
    }

    private void Attacking() {
        movement.Attacking();
    }

    private IEnumerator RoamingRoutine() {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            movement.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public void SwitchToRoaming() { state = State.Roaming; }
    public void SwitchToAttacking() { state = State.Attacking; }
}
