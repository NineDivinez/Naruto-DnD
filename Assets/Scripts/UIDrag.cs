using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDrag : MonoBehaviour
{
    float offSetX;
    float offSetY;

    public void BeginDrag()
    {
        offSetX = transform.position.x - Input.mousePosition.x;
        offSetY = transform.position.y - Input.mousePosition.y;
    }

    public void OnMouseDrag()
    {
        transform.position = new Vector3(offSetX + Input.mousePosition.x, offSetY + Input.mousePosition.y);
    }
}
