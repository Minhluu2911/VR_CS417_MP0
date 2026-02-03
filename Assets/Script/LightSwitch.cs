using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// LightSwitch - Controls a point light's color via VR controller input.
/// Attach this script directly to the Point Light GameObject.
/// </summary>
public class LightSwitch : MonoBehaviour
{
    // Reference to the Light component (auto-fetched in Start)
    private Light myLight;

    // Input action for changing light color (assign in Inspector)
    // Drag your desired controller button action here
    public InputActionProperty switchButton;


    /// <summary>
    /// Start runs when the game initially starts.
    /// We get and save a reference to the Light component for later use.
    /// </summary>
    void Start()
    {
        // Get the Light component attached to this GameObject
        myLight = GetComponent<Light>();

        // Safety check - warn if no Light component found
        if (myLight == null)
        {
            Debug.LogError("LightSwitch: No Light component found on this GameObject!");
        }

        // Safety check - warn if no input action assigned
        if (switchButton.action == null)
        {
            Debug.LogError("LightSwitch: No input action assigned to switchButton!");
        }
    }

    /// <summary>
    /// OnEnable - Enable the input action when this script is enabled.
    /// </summary>
    void OnEnable()
    {
        // Make sure the action is enabled so it can receive input
        if (switchButton.action != null)
        {
            switchButton.action.Enable();
        }
    }

    /// <summary>
    /// OnDisable - Disable the input action when this script is disabled.
    /// </summary>
    void OnDisable()
    {
        // Disable the action when not needed
        if (switchButton.action != null)
        {
            switchButton.action.Disable();
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// We check for button press and change the light color.
    /// </summary>
    void Update()
    {
        // Check if the switch button was pressed this frame
        if (switchButton.action.WasPressedThisFrame())
        {
            ChangeLightColor();
        }
    }

    /// <summary>
    /// Changes the light to a random color.
    /// Uses Random.value to generate RGB values between 0 and 1.
    /// </summary>
    void ChangeLightColor()
    {
        // Safety check - make sure we have a light reference
        if (myLight == null) return;

        // Generate a random color using RGB values
        Color randomColor = new Color(Random.value, Random.value, Random.value);

        // Apply the random color to the light
        myLight.color = randomColor;

        // Optional: Log the color change for debugging
        Debug.Log("Light color changed to: " + randomColor);
    }
}
