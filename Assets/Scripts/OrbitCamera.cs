using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [Tooltip("Center of orbit rotation")]
    [SerializeField] private Transform target;
    [Tooltip("Offset from object of focus")]
    [SerializeField] private Vector3 targetOffset;
    [Space(20)]
    [Tooltip("Toggles allowing camera rotation")]
    [SerializeField] private bool canRotate = true;
    [Tooltip("Speed of camera rotation")]
    [SerializeField] private float rotationSpeed = 50f;
    [Tooltip("Increasing this value will stop the camera faster.")]
    [SerializeField] private float decelerationSpeed = 5f;
    [Tooltip("Increasing this value will make the camera slide faster before stopping.")]
    [SerializeField] private float decelerationSpeedMultipler = 50f;

    private float decelerateThreshold = 0.05f;
    private float curDecelerationSpeed;
    private float deltaX;
    private Vector3 pressPos, lastPressPos, releasePos;
    private Vector3 deltaPressPos;

    private void Start()
    {
        //Camera to look at the offsetted focus point
        transform.LookAt(target.transform.position + targetOffset);
    }

    private void Update()
    {
        if (canRotate)
        {
            if (Input.GetMouseButtonDown(1))
            {
                curDecelerationSpeed = 0f;
                pressPos = lastPressPos = Input.mousePosition;
            }

            if (Input.GetMouseButton(1))
            {
                deltaPressPos = Input.mousePosition - lastPressPos;
                deltaPressPos.x /= Screen.width;

                //Rotate Camera around focus point horizontally
                transform.RotateAround(target.transform.position, Vector3.up, deltaPressPos.x * rotationSpeed);

                lastPressPos = Input.mousePosition;

                if (Mathf.Abs(deltaPressPos.x) > 0f)
                    deltaX = deltaPressPos.x;
            }

            if (Input.GetMouseButtonUp(1))
            {
                releasePos = Input.mousePosition;

                if (Mathf.Abs((releasePos - pressPos).x) < decelerateThreshold)
                    deltaX = 0f;

                curDecelerationSpeed = rotationSpeed * decelerationSpeedMultipler * deltaX;
            }

            if (Mathf.Abs(curDecelerationSpeed) > 0.1f) //Smoothly decelerate to 0
            {
                curDecelerationSpeed = Mathf.Lerp(curDecelerationSpeed, 0, decelerationSpeed * Time.deltaTime);
                transform.RotateAround(target.transform.position, Vector3.up, curDecelerationSpeed * Time.deltaTime);
            }
        }
    }
}
