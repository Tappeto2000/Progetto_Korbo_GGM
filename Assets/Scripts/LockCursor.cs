using UnityEngine;

public class LockCursor : MonoBehaviour
{
    void Start()
    {
        // Blocca il cursore al centro dello schermo
        Cursor.lockState = CursorLockMode.Locked;
        // Nascondi il cursore
        Cursor.visible = false; 
    }

    void Update()
    {
        // Rendi possibile sbloccare il cursore premendo il tasto Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Rilock il cursore se premuto il tasto del mouse
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
