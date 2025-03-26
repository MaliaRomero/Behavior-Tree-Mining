using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RadSpot : Node
{
    private Transform _transform;
    private float _radRange;
    private bool isRadiating = false;
    private bool RadHappened = false;
    private Transform currentRadSpot = null;
    private float radiationCooldown = 3f;  // Cooldown after radiation ends
    private float cooldownTimer = 0f;

    private static int _radLayerMask = 1 << 8;

    public RadSpot(Transform transform, float radRange)
    {
        _transform = transform;
        _radRange = radRange;
    }

    public override NodeState Evaluate()
    {
        if (RadHappened)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
                Debug.Log("Cooldown complete. Returning to normal patrol.");
                RadHappened = false; // Reset flag
                currentRadSpot = null; // Allow detecting new radiation spots
                return NodeState.SUCCESS;
            }
            return NodeState.RUNNING; // Wait until cooldown is over
        }

        if (isRadiating)
        {
            return NodeState.RUNNING;
        }

        Collider[] hitColliders = Physics.OverlapSphere(_transform.position, _radRange, _radLayerMask);

        if (hitColliders.Length > 0)
        {
            Transform radSpot = hitColliders[0].transform;

            if (currentRadSpot == radSpot)
            {
                return NodeState.RUNNING; // Already processing this spot
            }

            currentRadSpot = radSpot;
            SetData("radSpot", radSpot);

            _transform.gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Radiation());

            Debug.Log("RADIATION FOUND.");
            isRadiating = true;
            return NodeState.RUNNING;
        }

        Debug.Log("No radiation spot found.");
        return NodeState.FAILURE;
    }

    private IEnumerator Radiation()
    {
        Debug.Log("Green!");
        SkinnedMeshRenderer skinnedMeshRenderer = _transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("No SkinnedMeshRenderer found on the guard!");
            yield break;
        }

        Color originalColor = skinnedMeshRenderer.material.color;
        skinnedMeshRenderer.material.color = Color.green;

        yield return new WaitForSeconds(5f);

        skinnedMeshRenderer.material.color = originalColor;
        Debug.Log("Radiation effect ended. Color reset.");

        if (currentRadSpot != null)
        {
            GameObject.Destroy(currentRadSpot.gameObject);
            Debug.Log("Rad spot destroyed.");

            SetData("radSpot", null);
        }

        isRadiating = false;
        RadHappened = true; 
        cooldownTimer = radiationCooldown; // Start cooldown period
    }
}