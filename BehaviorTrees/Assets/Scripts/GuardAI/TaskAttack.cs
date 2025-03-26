/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _animator;

    private Transform _lastTarget;
    private EnemyManager _enemyManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        // Ensure the target is not null
        if (target == null)
        {
            Debug.LogError("Target is null in TaskAttack!");
            return NodeState.FAILURE; // Exit early if no target
        }

        /*if (target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            _lastTarget = target;
        }//
                // Check if we need to assign the enemy manager and the target is different from the last one
        if (_enemyManager == null || target != _lastTarget)
        {
            _enemyManager = target.GetComponent<EnemyManager>();
            if (_enemyManager == null)
            {
                Debug.LogError("EnemyManager is missing on the target!");
                return NodeState.FAILURE; // Exit early if no EnemyManager is found
            }
            _lastTarget = target; // Update lastTarget reference
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _enemyManager.TakeHit(); // Checks if last hit in enemy script
            if (enemyIsDead)
            {
                Debug.Log("enemy dead");
                ClearData("target"); // if enemy dead, clear target to go back to patrolling
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Walking", true);
            }
            else
            {
                _attackCounter = 0f;
            }
        }

        state = NodeState.RUNNING;
        return state;
    }

}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttack : Node
{
    private Animator _animator;
    private Transform _lastTarget;
    private EnemyManager _enemyManager;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target == null)
        {
            Debug.LogError("Target is null in TaskAttack!");
            return NodeState.FAILURE; // Exit early if no target
        }

        _enemyManager = target.GetComponent<EnemyManager>();
        if (_enemyManager == null)
        {
            Debug.LogError("EnemyManager is missing on the target! Target is not an enemy.");
            return NodeState.FAILURE; // Exit early if the target is not an enemy
        }

        if (target != _lastTarget)
        {
            _lastTarget = target;
        }

        // Increment the attack counter based on the time passed
        _attackCounter += Time.deltaTime;

        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _enemyManager.TakeHit();
            if (enemyIsDead)
            {
                Debug.Log("Enemy is dead.");
                ClearData("target"); // Clear target when the enemy is dead
                _animator.SetBool("Attacking", false);
                _animator.SetBool("Walking", true);
            }
            else
            {
                _attackCounter = 0f; 
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}