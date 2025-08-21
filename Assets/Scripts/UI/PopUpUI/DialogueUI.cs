using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;



public class DialogueUI :MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;

    private void OnEnable()
    {
        playerInput.enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }
    private void OnDisable()
    {
        playerInput.enabled=true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
