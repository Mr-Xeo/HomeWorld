using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Stick : MonoBehaviour
{
    [SerializeField]
    private Color m_HoverColor;

    private Collider2D      m_Collider2D;
    private SpriteRenderer  m_SpriteRenderer;

    private Color m_OriginalColor;

    private float m_ScaleRatio;

    private const float c_MinScale  = 0.2f;
    private const float c_ScaleStep = 2f;

    private void Awake()
    {
        m_Collider2D        = GetComponent<Collider2D>();
        m_SpriteRenderer    = GetComponent<SpriteRenderer>();

        m_OriginalColor = m_SpriteRenderer.color;

        Vector3 scale = transform.localScale;
        m_ScaleRatio = scale.x / scale.y;
    }

    public void DisableStick()
    {
        m_Collider2D.enabled = false;
    }

    public void OnRaycasterIn()
    {
        m_SpriteRenderer.color = m_HoverColor;
    }

    public void OnRaycasterOut()
    {
        m_SpriteRenderer.color = m_OriginalColor;
    }

    public void OnRaycasterPress(Vector2 mouseDelta)
    {
        transform.Translate(mouseDelta, Space.World);
    }

    public void OnRotate(Vector2 mouseDelta)
    {
        transform.Rotate(0, 0, -mouseDelta.x, Space.World);
    }

    public void OnScale(Vector2 mouseDelta)
    {
        Vector3 scale = transform.localScale;
        scale.y = Mathf.Max(scale.y + mouseDelta.x * c_ScaleStep * Time.deltaTime, c_MinScale);
        scale.x = scale.y * m_ScaleRatio;

        transform.localScale = scale;
    }

    public void OnRaycasterUp()
    {
        
    }
}
