using UnityEngine;

public class Socket : MonoBehaviour
{
    public bool IsConnected { private set; get; }

    private GameObject connectedPart;

    public bool Connect(GameObject part)
    {
        if (!IsConnected)
        {
            part.transform.SetParent(transform);
            part.transform.localPosition = Vector3.zero;
            part.transform.localRotation = transform.localRotation;
            connectedPart = part;
            IsConnected = true;
        }
        
        return IsConnected;
    }

    public void UnConnect()
    {
        connectedPart.transform.SetParent(null);
        connectedPart = null;
        IsConnected = false;
    }
}
