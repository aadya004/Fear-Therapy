using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [Header("XR Origin (Player)")]
    public Transform xrOrigin;

    [Header("Teleport Points")]
    public Transform groundStart;
    public Transform level1Start;
    public Transform level2Start;
    public Transform level3Start;

    // Call this from UI buttons
    public void TeleportToGround()
    {
        TeleportTo(groundStart);
    }

    public void TeleportToLevel1()
    {
        TeleportTo(level1Start);
    }

    public void TeleportToLevel2()
    {
        TeleportTo(level2Start);
    }

    public void TeleportToLevel3()
    {
        TeleportTo(level3Start);
    }

    private void TeleportTo(Transform targetPoint)
    {
        if (xrOrigin != null && targetPoint != null)
        {
            xrOrigin.position = targetPoint.position;
            xrOrigin.rotation = targetPoint.rotation;
        }
    }
}
