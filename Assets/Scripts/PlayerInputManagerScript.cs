using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerScript : MonoBehaviour {
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private bool wasdJoined = false;
    private bool arrowsJoined = false;
    private bool gamepadJoined = false;

    // Update is called once per frame
    void Update() {
        if (Keyboard.current == null) return;

        if (!wasdJoined && Keyboard.current.spaceKey.wasPressedThisFrame) {
            var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "WASD",
                pairWithDevice: Keyboard.current);
            if (spawnPoints.Length > 0) {
                player.transform.position = spawnPoints[0].position;
            }

            wasdJoined = true;
        }

        if (!arrowsJoined && Keyboard.current.rightCtrlKey.wasPressedThisFrame) {
            var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "Arrows",
                pairWithDevice: Keyboard.current);
            if (spawnPoints.Length > 0) {
                player.transform.position = spawnPoints[0].position;
            }

            arrowsJoined = true;
        }

        foreach (var gamePad in Gamepad.all) {
            if (gamePad.buttonSouth.wasPressedThisFrame && !gamepadJoined) {
                PlayerInput.Instantiate(playerPrefab,
                    controlScheme: "Gamepad",
                    pairWithDevice: gamePad);
                
                gamepadJoined = true;
            }
        }
        
        
        
        
        
        
        
    }
}
