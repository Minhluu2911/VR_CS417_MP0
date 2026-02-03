using UnityEngine;

public class OrbitPlanet : MonoBehaviour
{
    public float degreesPerSecond = 30f;

    private void Update()
    {
        transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f, Space.Self);
    }
}