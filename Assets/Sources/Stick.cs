using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Stick : MonoBehaviour
{
    [SerializeField]
    private Color m_HoverColor;

    private SpriteRenderer m_SpriteRenderer;

    private Color m_OriginalColor;

    private void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

        m_OriginalColor = m_SpriteRenderer.color;
    }

    public void OnMouseIn()
    {
        m_SpriteRenderer.color = m_HoverColor;
    }

    public void OnMouseOut()
    {
        m_SpriteRenderer.color = m_OriginalColor;
    }
}
