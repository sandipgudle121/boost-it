using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplicaionScript : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed)
            Application.Quit();
            Debug.Log("Quit Application.");
    }
}
