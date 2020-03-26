using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.LowLevel;

public class MultiplayerManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
	[SerializeField]
    private GameObject playerPrefab;
    public PlayerInput PlayerInput { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        playerInputManager = GetComponent<PlayerInputManager>();


        // Listening must be enabled explicitly
        ++InputUser.listenForUnpairedDeviceActivity;

        // Example of how to spawn a new player automatically when a button
        // is pressed on an unpaired device.
        InputUser.onUnpairedDeviceUsed +=
        (control, eventPtr) =>
        {

            if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
                return;

            //ignore anything that isn't a gamepad
            if (control.device as Gamepad == null && control.device as Keyboard == null)
                return;

            // Spawn player and pair device. If the player's actions have control schemes
            // defined in them, PlayerInput will look for a compatible scheme automatically.
            if (playerInputManager.playerCount <= playerInputManager.maxPlayerCount)
            {
                PlayerInput = PlayerInput.Instantiate(prefab: playerPrefab, playerIndex: playerInputManager.playerCount, pairWithDevice: control.device);
                Debug.Log("new Player");
                CharacterSwitcher characterSwitch = PlayerInput.GetComponent<CharacterSwitcher>();
                Debug.Log("clone created : " + characterSwitch.gameObject);
                Debug.Log("menu : " + gameObject.ToString());
                characterSwitch.SetParent(playerInputManager.playerCount, gameObject);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
