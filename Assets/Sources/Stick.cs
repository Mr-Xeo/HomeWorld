using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Stick : MonoBehaviour
{
    [SerializeField]
    private Color m_HoverColor;

    private Collider2D m_Collider2D;

    private SpriteRenderer      m_SpriteRenderer;
    private SpriteRenderer[]    m_SpriteRenderers;

    private Color m_OriginalColor;

    private float m_ScaleRatio;

    private const float c_MinScale  = 0.2f;
    private const float c_ScaleStep = 2f;

    private bool m_IsKeep;

    private void Awake()
    {
        m_Collider2D = GetComponent<Collider2D>();

        m_SpriteRenderer    = GetComponent<SpriteRenderer>();
        m_SpriteRenderers   = GetComponentsInChildren<SpriteRenderer>();

        m_OriginalColor = m_SpriteRenderer.color;

        Vector3 scale = transform.localScale;
        m_ScaleRatio = scale.x / scale.y;
    }

    public void DisableStick()
    {
        //only keep sticks marked as keep
        if (!m_IsKeep)
        {
            DisableRenderers();
        }

        m_Collider2D.enabled = false;
    }

    private void DisableRenderers()
    {
        m_SpriteRenderer.enabled = false;

        for (int i = 0; i < m_SpriteRenderers.Length; i++)
        {
            m_SpriteRenderers[i].enabled = false;
        }

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

    private void OnTriggerEnter2D(Collider2D col)
    {
        m_IsKeep = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        m_IsKeep = false;
    }
}
