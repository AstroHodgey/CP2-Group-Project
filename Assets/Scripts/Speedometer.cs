using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;

public class Speedometer : MonoBehaviour {
    [SerializeField] private GameObject heatPip;
    private GameObject[] heatPips;

    private int pipCount = 0;

    public BikeMovement bikeMovement;
    
    void Start() {
        heatPips = new GameObject[26];
    }

    void Update()
    {
        if (pipCount < bikeMovement.speed) {
            pipCount++; heatPips[pipCount] = Instantiate(heatPip, this.transform);
        }

        if (pipCount - 1 > bikeMovement.speed) {
            Destroy(transform.GetChild(pipCount).gameObject); pipCount--;
        }

        if (pipCount is >= 20 and < 22 && bikeMovement.speed > 20) {
            pipCount++; heatPips[pipCount] = Instantiate(heatPip, this.transform);
            pipCount++; heatPips[pipCount] = Instantiate(heatPip, this.transform);
        }
    }
}
