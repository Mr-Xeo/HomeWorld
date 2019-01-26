using UnityEngine;

using Zenject;

public class DrawerSlot : MonoBehaviour
{
    [SerializeField]
    private Color m_HoverColor;

    private CameraTransitions m_CameraTransitions;

    private SpriteRenderer m_SpriteRenderer;

    private Color m_OriginalColor;

    private void Awake()
    {
        m_SpriteRenderer    = GetComponent<SpriteRenderer>();
        m_OriginalColor     = m_SpriteRenderer.color;
    }

    [Inject]
    private void Init(CameraTransitions cameraTransitions)
    {
        m_CameraTransitions = cameraTransitions;
    }

    private void OnMouseEnter()
    {
        m_SpriteRenderer.color = m_HoverColor;
    }

    private void OnMouseExit()
    {
        m_SpriteRenderer.color = m_OriginalColor;
    }

    private void OnMouseUpAsButton()
    {
        m_SpriteRenderer.color = m_HoverColor;

        m_CameraTransitions.Transition();
    }
}
