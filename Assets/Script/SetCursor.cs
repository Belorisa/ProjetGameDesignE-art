using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

using Vector2 = UnityEngine.Vector2;

public class SetCursor : MonoBehaviour
{

    public Texture2D cursor;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 cursorOff = new Vector2(0, 0);
        Cursor.SetCursor(cursor, cursorOff, CursorMode.Auto); //place le cursor et utlilise le cursorOff pour placer le clic (initialiser a 0 sois en haut a gauche)
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
