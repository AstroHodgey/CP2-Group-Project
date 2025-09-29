using UnityEngine;

public class CameraScrolling : MonoBehaviour {
    [SerializeField] private Vector3 offset;
    [SerializeField] private float dampening;

    public Transform target;

    private Vector3 vel = Vector3.zero;
    
    void Update() {
        
        Vector3 targetPosition = target.position + offset;
        
        if (target.position.x < 150) {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, dampening);
        }
        
    }
}
