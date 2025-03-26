using UnityEngine;

//From GitHub repo https://github.com/MinaPecheux/UnityTutorials-BehaviourTrees
public class EnemyManager : MonoBehaviour
{
    private int _healthpoints;

    private void Awake()
    {
        _healthpoints = 30;
    }

    public bool TakeHit()
    {
        _healthpoints -= 10;
        bool isDead = _healthpoints <= 0;
        if (isDead) _Die();
        return isDead;
    }

    private void _Die()
    {
        Destroy(gameObject);
    }
}
