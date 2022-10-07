using UnityEngine;

public class ConstructionMode : Mode
{
    [SerializeField][Min(0)] private float maxConnectionDistance = 1;
    [Header("Dragging Limits")]
    [SerializeField] private float minDragLimitX = -15;
    [SerializeField] private float maxDragLimitX = 15;
    [SerializeField] private float minDragLimitY = 0;
    [SerializeField] private float maxDragLimitY = 10;

    private Ray ray;
    private RaycastHit hit;
    private Part pickedPart;
    private Vector3 pickedObjScreenPos, dragPos;

    private void OnDrawGizmos()
    {
        if (pickedPart)
            Gizmos.DrawWireSphere(pickedPart.transform.position, maxConnectionDistance);
    }

    protected override void OnClickDown()
    {
        base.OnClickDown();

        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //if((pickablesLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            if(hit.collider.gameObject.GetComponent<Part>() is Part part && !part.IsConnected())
            {
                pickedPart = part;
                pickedObjScreenPos = Camera.main.WorldToScreenPoint(pickedPart.transform.position);
            }
        }
    }

    protected override void OnDrag()
    {
        base.OnDrag();

        if (pickedPart)
        {
            dragPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pickedObjScreenPos.z));
            dragPos.x = Mathf.Clamp(dragPos.x, minDragLimitX, maxDragLimitX);
            dragPos.y = Mathf.Clamp(dragPos.y, minDragLimitY, maxDragLimitY);
            pickedPart.transform.position = dragPos;

            //Plane plane = new Plane(Vector3.up, new Vector3(0, 3, 0));
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (plane.Raycast(ray, out float distance))
            //    pickedObj.transform.position = ray.GetPoint(distance);
        }
    }

    protected override void OnClickUp()
    {
        base.OnClickUp();

        if (pickedPart)
        {
            //Socket check
            Collider[] hitColliders = Physics.OverlapSphere(pickedPart.transform.position, maxConnectionDistance);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.GetComponent<Socket>() is Socket socket)
                {
                    if (socket.Connect(pickedPart))
                        break;
                }
            }
        }  

        pickedPart = null;
    }
}
