using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckMineSpotInRange : Node
{
    private Transform _transform;
    private float _mineRange;

    private static int _mineLayerMask = 1 << 7;

    public CheckMineSpotInRange(Transform transform, float mineRange)
    {
        _transform = transform;
        _mineRange = mineRange;
    }

    public override NodeState Evaluate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(_transform.position, _mineRange, _mineLayerMask);
        
        //Debug.Log("Checking for mining spots... Found " + hitColliders.Length);

        if (hitColliders.Length > 0)
        {
            Transform miningSpot = hitColliders[0].transform;
            SetData("miningSpot", miningSpot);

            _transform.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Mine(miningSpot));

            Debug.Log("Mining spot found. Waiting to mine...");
            
            return NodeState.RUNNING;
        }

        Debug.Log("No mining spot found.");
        return NodeState.FAILURE; 
    }

    private IEnumerator Mine(Transform miningSpot)
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        if (miningSpot != null)
        {
            GameObject.Destroy(miningSpot.gameObject);
            Debug.Log("Mining spot destroyed.");

            // Reset the mining spot data
            SetData("miningSpot", null);
        }
        else
        {
            Debug.LogWarning("Mining spot was destroyed before mining process finished.");
        }

        yield return null;
    }

}

