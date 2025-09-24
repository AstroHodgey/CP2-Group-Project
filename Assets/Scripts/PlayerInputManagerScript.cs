using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManagerScript : MonoBehaviour {
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform[] spawnPoints;

    private bool wasdJoined = false;
    private bool arrowsJoined = false;
    
    private bool gamepadOneJoined = false;
    private bool gamepadTwoJoined = false;
    private bool gamepadThreeJoined = false;
    private bool gamepadFourJoined = false;

    private int maxPlayers = 4;
    private int numberOfplayers = 0;

    // Update is called once per frame
    void Update() {
        if (numberOfplayers == maxPlayers) return;

        
        if (!wasdJoined && Keyboard.current.spaceKey.wasPressedThisFrame) {
            var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "WASD",
                pairWithDevice: Keyboard.current);
            if (spawnPoints.Length > 0) {
                player.transform.position = spawnPoints[0].position;
            }

            wasdJoined = true;
            numberOfplayers++;
        }

        if (!arrowsJoined && Keyboard.current.rightCtrlKey.wasPressedThisFrame) {
            var player = PlayerInput.Instantiate(playerPrefab,
                controlScheme: "Arrows",
                pairWithDevice: Keyboard.current);
            if (spawnPoints.Length > 0) {
                player.transform.position = spawnPoints[0].position;
            }

            arrowsJoined = true;
            numberOfplayers++;

        }

        foreach (var gamePad in Gamepad.all) {
            
            
            if (gamePad.buttonSouth.wasPressedThisFrame && !gamepadOneJoined) {
                PlayerInput.Instantiate(playerPrefab,
                    controlScheme: "Gamepad",
                    pairWithDevice: Gamepad.all[0]);
                
                gamepadOneJoined = true;
                numberOfplayers++;
            }
            
            if (gamePad.buttonSouth.wasPressedThisFrame && gamepadOneJoined && !gamepadTwoJoined)
            {
                PlayerInput.Instantiate(playerPrefab,
                    controlScheme: "Gamepad",
                    pairWithDevice: Gamepad.all[1]);

                gamepadTwoJoined = true;
                numberOfplayers++;

            }
            
            if (gamePad.buttonSouth.wasPressedThisFrame && gamepadTwoJoined && !gamepadThreeJoined)
            {
                PlayerInput.Instantiate(playerPrefab,
                    controlScheme: "Gamepad",
                    pairWithDevice: Gamepad.all[2]);

                gamepadThreeJoined = true;
                numberOfplayers++;

            }
            
            if (gamePad.buttonSouth.wasPressedThisFrame && gamepadThreeJoined && !gamepadFourJoined)
            {
                PlayerInput.Instantiate(playerPrefab,
                    controlScheme: "Gamepad",
                    pairWithDevice: Gamepad.all[3]);

                gamepadFourJoined = true;
                numberOfplayers++;

            }



        }
        
        
        
        
        
        
        
    }



}
