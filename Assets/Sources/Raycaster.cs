using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Raycaster : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_RaycastLayer;

    private Camera m_Camera;

    private Stick m_OldSelection;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }

    private void Update()
    {
        Stick selection = GetHitOnMousePosition();
        if (selection != m_OldSelection)
        {
            if (m_OldSelection != null)
            {
                m_OldSelection.OnMouseOut();
            }

            if (selection != null)
            {
                selection.OnMouseIn();
            }
        }

        m_OldSelection = selection;
    }

    private Stick GetHitOnMousePosition()
    {
        Vector2 mousePosition   = Input.mousePosition;
        Ray     ray             = m_Camera.ScreenPointToRay(mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, m_Camera.farClipPlane, m_RaycastLayer))
        {
            return hit.collider.GetComponent<Stick>();
        }

        return null;
    }
}
