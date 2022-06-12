using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class dirtyManager : MonoBehaviour
{
    PlayerControls inputManager;

    void Awake()
    {
        inputManager = new PlayerControls();
    }

    private void OnEnable()
    {
        inputManager.MainMenu.Next.performed += LoadNextScene;
        inputManager.MainMenu.Enable();
    }

    public void LoadNextScene(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
