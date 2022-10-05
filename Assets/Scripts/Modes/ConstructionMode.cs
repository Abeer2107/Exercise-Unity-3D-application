using UnityEngine;

public class ConstructionMode : Mode
{
    [SerializeField] private LayerMask pickablesLayerMask;
    [SerializeField][Min(0)] private float maxConnectionDistance = 1;
    [Header("Dragging Limits")]
    [SerializeField] private float minDragLimitX = -15;
    [SerializeField] private float maxDragLimitX = 15;
    [SerializeField] private float minDragLimitY = 0;
    [SerializeField] private float maxDragLimitY = 10;

    private Ray ray;
    private RaycastHit hit;
    private GameObject pickedObj;
    private Vector3 pickedObjScreenPos, dragPos;

    private void OnDrawGizmos()
    {
        if (pickedObj)
            Gizmos.DrawWireSphere(pickedObj.transform.position, maxConnectionDistance);
    }

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
        {
            dragPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pickedObjScreenPos.z));
            dragPos.x = Mathf.Clamp(dragPos.x, minDragLimitX, maxDragLimitX);
            dragPos.y = Mathf.Clamp(dragPos.y, minDragLimitY, maxDragLimitY);
            pickedObj.transform.position = dragPos;

            //Plane plane = new Plane(Vector3.up, new Vector3(0, 3, 0));
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (plane.Raycast(ray, out float distance))
            //    pickedObj.transform.position = ray.GetPoint(distance);
        }
    }

    protected override void OnClickUp()
    {
        base.OnClickUp();

        if (pickedObj)
        {
            //Socket check
            Collider[] hitColliders = Physics.OverlapSphere(pickedObj.transform.position, maxConnectionDistance);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.GetComponent<Socket>() is Socket socket)
                {
                    if (socket.Connect(pickedObj))
                    {
                        pickedObj.gameObject.layer = 0;
                        break;
                    }
                }
            }
        }  

        pickedObj = null;
    }
}
