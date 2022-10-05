using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoringMode : Mode
{
    private Ray ray;
    private RaycastHit hit;

    protected override void OnClickDown()
    {
        base.OnClickDown();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

        }
    }

    protected override void OnDrag()
    {
        base.OnDrag();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {

        }
    }
}
