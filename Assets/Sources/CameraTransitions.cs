using UnityEngine;
using UnityEngine.UI;

using Cinemachine;

public class CameraTransitions : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_DrawerCamera;

    [SerializeField]
    private Image[] m_ScrollAreas;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_DrawerCamera.enabled = !m_DrawerCamera.enabled;

            for (int i = 0; i < m_ScrollAreas.Length; i++)
            {
                Image currentImage = m_ScrollAreas[i];
                currentImage.enabled = !currentImage.enabled;
            }
        }
    }
}
