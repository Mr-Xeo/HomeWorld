using UnityEngine;

public class Drawing : MonoBehaviour
{
    [SerializeField]
    private Sprite m_Sprite;
    public  Sprite Sprite { get { return m_Sprite; } }

    private Stick[] m_Sticks;

    private void Awake()
    {
        m_Sticks = GetComponentsInChildren<Stick>();
    }

    public void DisableDrawing()
    {
        for (int i = 0; i < m_Sticks.Length; i++)
        {
            m_Sticks[i].DisableStick();
        }
    }
}
