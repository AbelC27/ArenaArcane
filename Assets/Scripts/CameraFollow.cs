using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 offset = new Vector3(0, 0, -10);

    void Update()
    {
        
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + offset;
        }
    }
}