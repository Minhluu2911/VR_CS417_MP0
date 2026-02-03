using UnityEngine;
using UnityEngine.InputSystem; // Cần thiết cho Input mới

public class GameController : MonoBehaviour
{
    [Header("Cài đặt Ánh sáng")]
    public Light roomLight;
    public InputActionProperty lightButton; // Nút đổi màu đèn (VD: Nút A hoặc X)

    [Header("Cài đặt Dịch chuyển")]
    public Transform player; // Kéo XR Origin vào đây
    public InputActionProperty teleportButton; // Nút dịch chuyển (VD: Nút B hoặc Y)
    public Vector3 insidePos = new Vector3(0, 1, 0); // Vị trí trong phòng
    public Vector3 outsidePos = new Vector3(0, 1, -30); // Vị trí ngoài trời (để ngắm skybox)
    private bool isOutside = false;

    [Header("Cài đặt Thoát game")]
    public InputActionProperty quitButton; // Nút thoát (VD: Nút Menu)

    void Update()
    {
        // 1. Xử lý đổi màu đèn (Khi nhấn nút)
        if (lightButton.action.WasPressedThisFrame())
        {
            Color randomColor = new Color(Random.value, Random.value, Random.value);
            roomLight.color = randomColor;
        }

        // 2. Xử lý dịch chuyển ra ngoài/vào trong
        if (teleportButton.action.WasPressedThisFrame())
        {
            if (isOutside)
                player.position = insidePos;
            else
                player.position = outsidePos;
            
            isOutside = !isOutside;
        }

        // 3. Xử lý thoát game
        if (quitButton.action.WasPressedThisFrame())
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}