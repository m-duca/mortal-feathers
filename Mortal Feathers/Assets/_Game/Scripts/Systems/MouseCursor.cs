using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    public static Vector2 position;

    #region Engine Functions
    // Start is called before the first frame update
    private void Start()
    {
       // Change Cursor Image
       Cursor.SetCursor(Resources.Load<Texture2D>("mouse_cursor"), Vector2.zero, CursorMode.Auto);
    }

    private void Update()
    {
        // Get Mouse position and convert to a world position
        position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log(position);
    }
    #endregion
}
