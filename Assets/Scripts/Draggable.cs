using System.Collections;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool isDragging = false;
    public Vector2 offset;
    public Vector2 initialPosition;
    public bool isUsed = false;
    public bool isClicked = false;

    private IEnumerator StartDragCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        isDragging = true;
    }
    private void OnMouseDown()
    {
        isDragging = false;
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        initialPosition = transform.position;
        isClicked = true;
    }

    private void OnMouseUp()
    {
        if (!isDragging && (Vector2)transform.position == initialPosition)
        {
            isUsed = !isUsed;
        }

        isClicked = false;
        isDragging = false;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition + offset;

        // isDragging = true;
    }
}