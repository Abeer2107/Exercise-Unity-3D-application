using UnityEngine;

public class ModesHandler : MonoBehaviour
{
    [SerializeField] private bool activateFirstModeByDefault = true;
    [Space(20)]
    [SerializeField] private Mode[] modes;

    private Mode currentMode;

    private void Start()
    {
        if (activateFirstModeByDefault)
            ActivateMode(0);
    }

    public void ActivateMode(int index)
    {
        if (index >= 0 && index < modes.Length)
        {
            if (!modes[index].IsActive)
            {
                //Deactivate the rest
                for (int i = 0; i < modes.Length; i++)
                {
                    if (i != index)
                        modes[i].Deactivate();
                }

                modes[index].Activate();
                currentMode = modes[index];
            }
        }
    }

    public void ActivateMode(Mode mode)
    {
        for (int i = 0; i < modes.Length; i++)
        {
            if (modes[i] == mode)
            {
                ActivateMode(i);
                break;
            }
        }
    }

    public void DeactivateMode(int index)
    {
        if (index >= 0 && index < modes.Length)
            modes[index].Deactivate();
    }

    public void DeactivateMode(Mode mode)
    {
        for (int i = 0; i < modes.Length; i++)
        {
            if (modes[i] == mode)
            {
                DeactivateMode(i);
                break;
            }
        }
    }

    public void ToggleMode(int index)
    {
        if (index >= 0 && index < modes.Length)
        {
            if (modes[index].IsActive) DeactivateMode(index);
            else ActivateMode(index);
        }
    }

    public void ToggleMode(Mode mode)
    {
        for (int i = 0; i < modes.Length; i++)
        {
            if (modes[i] == mode)
            {
                ToggleMode(i);
                break;
            }
        }
    }
}
