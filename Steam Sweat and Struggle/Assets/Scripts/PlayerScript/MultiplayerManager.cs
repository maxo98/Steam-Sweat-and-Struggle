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
    private PlayerInput playerInput;
    private GameObject errorMsg;

    // Start is called before the first frame update
    void Start()
    {
        errorMsg = GameObject.FindGameObjectWithTag("errorMsg");
        errorMsg.SetActive(false);

        playerInputManager = GetComponent<PlayerInputManager>();

        
    }

    private void OnEnable()
    {
        // Listening must be enabled explicitly
        ++InputUser.listenForUnpairedDeviceActivity;

        //spawn a new player automatically when a button
        // is pressed on an unpaired device.
        InputUser.onUnpairedDeviceUsed +=
        (control, eventPtr) =>
        {

            if (!eventPtr.IsA<StateEvent>() && !eventPtr.IsA<DeltaStateEvent>())
                return;

            //ignore anything that isn't a gamepad
            if (control.device as Gamepad == null)
                return;

            // Spawn player and pair device. If the player's actions have control schemes
            // defined in them, PlayerInput will look for a compatible scheme automatically.
            if (playerInputManager.playerCount <= playerInputManager.maxPlayerCount)
            {
                playerInput = PlayerInput.Instantiate(prefab: playerPrefab, playerIndex: playerInputManager.playerCount, pairWithDevice: control.device);
                Debug.Log("controllers : " + playerInput.devices.Count);
                Debug.Log("new Player");
                CharacterSwitcher characterSwitch = playerInput.GetComponent<CharacterSwitcher>();
                Debug.Log("clone created : " + characterSwitch.gameObject);
                Debug.Log("menu : " + gameObject.ToString());
                CharacterName[] characters = characterSwitch.GetComponentsInChildren<CharacterName>();
                characterSwitch.SetParent(playerInputManager.playerCount, gameObject, characters, errorMsg);
            }
        };
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
