using UnityEngine;

public class AddMeshColliders : MonoBehaviour
{
    void Start()
    {
        // Find all MeshRenderers (assumed to be walls) in the cave
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in meshRenderers)
        {
            // Check if the object doesn't already have a MeshCollider
            if (renderer.gameObject.GetComponent<MeshCollider>() == null)
            {
                // Add MeshCollider component to the object
                MeshCollider meshCollider = renderer.gameObject.AddComponent<MeshCollider>();
                meshCollider.convex = true; // Optionally, set this if needed for dynamic objects
            }
        }
    }
}

