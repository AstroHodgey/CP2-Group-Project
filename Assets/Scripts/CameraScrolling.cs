using UnityEngine;

public class CameraScrolling : MonoBehaviour {
    [SerializeField] private Vector3 offset;
    [SerializeField] private float dampening;

    public Transform target;
    
    public Transform firstPlayerTransform;
    public Transform secondPlayerTransform;
    public Transform thirdPlayerTransform;
    public Transform fourthPlayerTransform;

    private Transform[] playerTransforms;

    private Vector3 vel = Vector3.zero;
    
    void Update() {

        float firstPlaceDistance = 0;
        float lastPlaceDistance = 0;
        
        
        for (int i = 0; i < playerTransforms.Length; i++) {
            float playerX = playerTransforms[i].position.x;
            if (playerX > firstPlaceDistance) {
                firstPlaceDistance = playerX;
            }

            if (i == 0) {
                lastPlaceDistance = playerX;
            } else if (playerX < lastPlaceDistance) {
                lastPlaceDistance = playerX;
            }
            
        }

        float distanceBetween = firstPlaceDistance - lastPlaceDistance;
        float midpoint = distanceBetween / 2;
        //target = (0, midpoint, 0);
        
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, dampening);
    }
}
