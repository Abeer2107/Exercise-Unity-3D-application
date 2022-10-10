using UnityEngine;

public class Socket : MonoBehaviour
{
    public bool IsConnected { private set; get; }

    private Part connectedPart;

    public bool Connect(Part part)
    {
        if (!IsConnected)
        {
            part.transform.SetParent(transform);
            part.transform.localPosition = Vector3.zero; //Assuming pivot is connection point
            part.transform.localRotation = transform.localRotation;
            part.SetConnected(true);
            connectedPart = part;
            IsConnected = true;
        }
        
        return IsConnected;
    }

    public void Disconnect()
    {
        connectedPart.transform.SetParent(null);
        connectedPart.SetConnected(false);
        connectedPart = null;
        IsConnected = false;
    }
}
