using UnityEngine;

using Zenject;

using MouseInput    = GameInstaller.MouseInput;
using MouseCursor   = GameInstaller.MouseCursor;

[RequireComponent(typeof(Camera))]
public class Raycaster : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_RaycastLayer;

    private Camera m_Camera;

    private Stick m_OldSelection;
    private Stick m_RotateSelection;
    private Stick m_PressedSelection;

    private MouseInput  m_MouseInput;
    private MouseCursor m_MouseCursor;

    private Vector2 m_OldMousePos;

    private void Awake()
    {
        m_Camera = GetComponent<Camera>();

        m_OldMousePos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
    }

    [Inject]
    private void Init(MouseInput mouseInput, MouseCursor mouseCursor)
    {
        m_MouseInput    = mouseInput;
        m_MouseCursor   = mouseCursor;
    }

    private void Update()
    {
        Stick selection =   m_PressedSelection != null ? m_PressedSelection : 
                            m_RotateSelection != null ? m_RotateSelection : GetHitOnMousePosition();
        
        if (selection != m_OldSelection)
        {
            if (m_OldSelection != null)
            {
                m_OldSelection.OnRaycasterOut();
            }

            if (selection != null)
            {
                selection.OnRaycasterIn();
            }
        }

        m_OldSelection = selection;

        ProcessMouseEvents(selection);
    }

    private void ProcessMouseEvents(Stick selection)
    {
        Vector2 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);

        if (selection != null)
        {
            if (Input.GetButtonDown(m_MouseInput.MouseClick))
            {
                m_PressedSelection = selection;
            }

            if (Input.GetButtonDown(m_MouseInput.MouseRightClick))
            {
                StartRotation(selection);
            }
        }

        if (m_PressedSelection != null)
        {
            Vector2 mouseDelta = mousePosition - m_OldMousePos;
            m_PressedSelection.OnRaycasterPress(mouseDelta);

            if (Input.GetButtonUp(m_MouseInput.MouseClick))
            {
                selection.OnRaycasterUp();
                m_PressedSelection = null;
            }
        }

        if (m_RotateSelection != null)
        {
            Vector2 mouseDelta = m_Camera.WorldToScreenPoint(mousePosition) - m_Camera.WorldToScreenPoint(m_OldMousePos);
            m_RotateSelection.OnRotate(mouseDelta);

            if (Input.GetButtonUp(m_MouseInput.MouseRightClick))
            {
                EndRotation();
            }
        }

        m_OldMousePos = mousePosition;
    }

    private void StartRotation(Stick selection)
    {
        m_RotateSelection = selection;

        Texture2D mouseCursor = m_MouseCursor.RotateCursor;
        Cursor.SetCursor(mouseCursor, new Vector2(mouseCursor.width * 0.5f, mouseCursor.height * 0.5f), CursorMode.Auto);
    }

    private void EndRotation()
    {
        m_RotateSelection = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
