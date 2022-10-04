using UnityEngine;

public class ConstructionMode : Mode
{
    [SerializeField] private LayerMask pickablesLayerMask;

    private Ray ray;
    private RaycastHit hit;
    private GameObject pickedObj;
    private Vector3 pickedObjScreenPos;

    protected override void OnClickDown()
    {
        base.OnClickDown();

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if((pickablesLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                //Debug.Log($"Picking: {hit.transform.name}");
                pickedObj = hit.collider.gameObject;
                pickedObjScreenPos = Camera.main.WorldToScreenPoint(pickedObj.transform.position);
            }
        }
    }

    protected override void OnDrag()
    {
        base.OnDrag();

        if (pickedObj)
            pickedObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pickedObjScreenPos.z));
    }

    protected override void OnClickUp()
    {
        base.OnClickUp();

        pickedObj = null;
    }
}
