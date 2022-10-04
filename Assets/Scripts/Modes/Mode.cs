using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Mode : MonoBehaviour
{
    public UnityEvent OnActivated;
    public UnityEvent OnDeactivated;

    public bool IsActive { get; protected set; }
    public bool IsDragging { get; protected set; }

    readonly float dragThreshold = 0.01f;
    private bool isDragging, passedThreshold;
    private Vector3 lastPressPos, startPressPos;
    protected Vector3 deltaPressPos;

    protected virtual void Update()
    {
        if (!IsActive) return;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isDragging = true;
            startPressPos = Input.mousePosition;
            lastPressPos = Input.mousePosition;

            OnClickDown();
        }

        if (isDragging)
        {
            deltaPressPos = Input.mousePosition - lastPressPos;
            deltaPressPos /= Screen.width;

            if (passedThreshold || Vector2.Distance(startPressPos, lastPressPos) / Screen.width >= dragThreshold)
            {
                passedThreshold = true;
                IsDragging = true;

                OnDrag();
            }

            lastPressPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            passedThreshold = false;

            OnClickUp();

            if (IsDragging)
                IsDragging = false;
        }

        if (Input.GetMouseButtonDown(1)) OnRightClick();
        if (Input.GetMouseButtonDown(2)) OnRollerClick();
    }

    protected virtual void OnActivate() { }
    protected virtual void OnDeactivate() { }
    protected virtual void OnDrag() { }
    protected virtual void OnClickDown() { }
    protected virtual void OnClickUp() { }
    protected virtual void OnRightClick() { }
    protected virtual void OnRollerClick() { }

    public void Activate()
    {
        IsActive = true;

        OnActivated.Invoke();

        OnActivate();
    }

    public void Deactivate()
    {
        IsActive = false;

        OnDeactivated.Invoke();

        OnDeactivate();
    }
}
