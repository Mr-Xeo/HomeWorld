using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Cinemachine;

public class CameraTransitions : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_DrawerCamera;

    [SerializeField]
    private DrawerHandler m_DrawerHandler;

    public void Transition()
    {
        m_DrawerCamera.enabled = !m_DrawerCamera.enabled;

        m_DrawerHandler.ToggleDrawer();
    }

    public void InitDrawerSlot(DrawerSlot drawerSlot)
    {
        m_DrawerHandler.InitSelectionPanel(drawerSlot);
    }

    public void OnDrawingEnded()
    {
        m_DrawerHandler.OnDrawingEnded();

        Transition();
    }

    public void OnDrawingSelected()
    {
        m_DrawerHandler.OnDrawingSelected();

        Transition();
    }
}
