using UnityEngine;

public class MiningSpot : MonoBehaviour
{
    public int resourceAmount = 5; // Total amount of resource available

    public bool ExtractResource()
    {
        if (resourceAmount > 0)
        {
            resourceAmount--;
            Debug.Log("Mined 1 resource! Remaining: " + resourceAmount);
            return false; // Still has resources
        }
        else
        {
            Debug.Log("Mining spot depleted!");
            return true; // No resources left
        }
    }
}