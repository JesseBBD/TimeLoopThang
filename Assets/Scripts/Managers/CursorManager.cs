using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursor_Normal;
    [SerializeField] Texture2D cursor_Pointer;
    [SerializeField] Texture2D cursor_Drag;
    [SerializeField] Texture2D cursor_Text;

    CursorMode cursorMode = CursorMode.Auto;

    Vector2 hotSpot_Pointer = new(10, 2);
    Vector2 hotSpot_Normal = new(8, 2);

    void Awake()
    {
        Cursor.SetCursor(cursor_Normal, hotSpot_Normal, cursorMode);
    }

    public void OnCursorEnter(GameObject sender)
    {
        if (sender.GetComponent<Button>().interactable)
        {
            Cursor.SetCursor(cursor_Pointer, hotSpot_Pointer, cursorMode);
        }
    }

    public void OnCursorEnter()
    {
        Cursor.SetCursor(cursor_Pointer, hotSpot_Pointer, cursorMode);
    }

    public void OnCursorEnterText()
    {
        Cursor.SetCursor(cursor_Text, hotSpot_Pointer, cursorMode);
    }

    public void OnCursorDrag()
    {
        Cursor.SetCursor(cursor_Drag, hotSpot_Pointer, cursorMode);
    }

    public void OnCursorExit()
    {
        Cursor.SetCursor(cursor_Normal, hotSpot_Normal, cursorMode);
    }
}
