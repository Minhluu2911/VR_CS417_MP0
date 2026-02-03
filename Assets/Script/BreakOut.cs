using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

/// <summary>
/// BreakOutToggle - Toggles between inside room view and outside viewpoint.
/// Attach to the XR Origin GameObject.
/// </summary>
public class BreakOutToggle : MonoBehaviour
{
    // Input action to trigger the toggle (assign in Inspector)
    public InputActionReference toggleAction;
    
    // The outside viewpoint transform (assign in Inspector)
    public Transform outsideViewpoint;

    // Reference to XR Origin component
    XROrigin origin;
    
    // Saved inside position and rotation
    Vector3 insidePos;
    Quaternion insideRot;
    
    // Track current state
    bool isOutside;

    void Awake()
    {
        origin = GetComponent<XROrigin>();
    }

    void OnEnable()
    {
        if (toggleAction != null)
        {
            toggleAction.action.Enable();
            toggleAction.action.performed += Toggle;
        }
    }

    void OnDisable()
    {
        if (toggleAction != null)
            toggleAction.action.performed -= Toggle;
    }

    /// <summary>
    /// Toggle between inside and outside viewpoints.
    /// Saves current position before going outside so we can return.
    /// </summary>
    void Toggle(InputAction.CallbackContext ctx)
    {
        // If going outside, save current position BEFORE teleporting
        if (!isOutside)
        {
            // Save current position and rotation before leaving
            insidePos = transform.position;
            insideRot = transform.rotation;
            Debug.Log($"BreakOut: Saved inside position: {insidePos}");
        }

        // Toggle state
        isOutside = !isOutside;

        if (isOutside && outsideViewpoint != null)
        {
            // Teleport to outside viewpoint
            TeleportTo(outsideViewpoint.position, outsideViewpoint.rotation);
            Debug.Log($"BreakOut: Teleported OUTSIDE to {outsideViewpoint.position}");
        }
        else
        {
            // Return to saved inside position
            TeleportTo(insidePos, insideRot);
            Debug.Log($"BreakOut: Teleported INSIDE to {insidePos}");
        }
    }

    /// <summary>
    /// Teleports the XR Origin to a target position and rotation.
    /// Uses direct transform positioning to avoid camera offset issues
    /// that can occur with MoveCameraToWorldLocation.
    /// </summary>
    void TeleportTo(Vector3 targetPos, Quaternion targetRot)
    {
        transform.position = targetPos;
        transform.rotation = targetRot;
    }
}
