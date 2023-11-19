using UnityEngine;

public class SpinObject : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 100f; // Adjust the spin speed in the Inspector

    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(0f, 0f, spinSpeed * Time.deltaTime);
    }
}
