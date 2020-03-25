using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
public class MultiplayerManager : MonoBehaviour
{
	
	[SerializeField]
    private GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Listening must be enabled explicitly.
        ++InputUser.listenForUnpairedDeviceActivity;
 
        // Example of how to spawn a new player automatically when a button
        // is pressed on an unpaired device.
        InputUser.onUnpairedDeviceUsed +=
            (control, eventPtr) =>
            {
                // Ignore anything but button presses.
                if (!(control is ButtonControl))
                    return;
        
                // Spawn player and pair device. If the player's actions have control schemes
                // defined in them, PlayerInput will look for a compatible scheme automatically.
                //PlayerInput.Instantiate(playerPrefab, device: control.device);
            };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
